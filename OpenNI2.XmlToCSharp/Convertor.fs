module OpenNI2.XmlToCSharp.Convertor

open System
open System.CodeDom.Compiler
open System.Collections.Generic
open System.IO
open System.Linq
open System.Text.RegularExpressions
open System.Xml.Linq
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

let getAttr (element: XElement) name = 
    match element.Attribute(name) with
    | null -> null
    | attribute -> attribute.Value

let xnId = XName.Get "id"
let getId (element: XElement) = getAttr element xnId

let xnName = XName.Get "name"
let getName (element: XElement) = getAttr element xnName

let xnFile = XName.Get "file"
let getFile (element: XElement) = getAttr element xnFile

let xnType = XName.Get "type"
let getType (element: XElement) = getAttr element xnType

let xnInit = XName.Get "init"
let getInit (element: XElement) = getAttr element xnInit

let xnExtern = XName.Get "extern" 
let getExtern (element: XElement) = getAttr element xnExtern

let xnReturns = XName.Get "returns"
let getReturns (element: XElement) = getAttr element xnReturns

let xnMembers = XName.Get "members"
let getMembers (element: XElement) = getAttr element xnMembers

let xnArtificial = XName.Get "artificial"
let getArtificial (element: XElement) = getAttr element xnArtificial

type Converter(document : XDocument, writer : IndentedTextWriter, dllName : string) = 

    let beginBlock() =
        writer.WriteLine ("{")
        writer.Indent <- writer.Indent + 1
 
    let endBlock() =
        writer.Indent <- writer.Indent - 1
        writer.WriteLine ("}")

    let root = document.Root
    let outputed = new HashSet<string>()

    let localFileIds = 
        root.Elements(XName.Get "File") |> Seq.filter (fun x -> Path.IsPathRooted(getName x) |> not) |> Seq.map getId
    
    let localElements = 
        seq { 
            let localFileIds = new HashSet<string>(localFileIds)
            for element in root.Elements() do
                match getFile element with
                | null -> ()
                | value -> 
                    match localFileIds.Contains value with
                    | false -> ()
                    | true -> yield element
        }

    let map = root.Elements().ToDictionary(fun x -> getId x)
    let lookUpById id = map.[id]

    let rec isFundamental (element : XElement) =
        let typeElement = map.[getType element]
        match typeElement.Name.LocalName with
        | "FundamentalType" -> true
        | "CvQualifiedType" -> isFundamental typeElement
        | _ -> false

    let getFunctionType (element : XElement) =
        let t = lookUpById (getType element)
        match t.Name.LocalName with
        | "PointerType" -> 
            let t = lookUpById (getType t)
            match t.Name.LocalName with
            | "FunctionType" -> Some(t)
            | _ -> None
        | _ -> None

    let rec lookupType id =
        let element = lookUpById id
        match element.Name.LocalName with
        | "FundamentalType" -> 
            match getName element with
            | "signed char" -> "sbyte"
            | "unsigned char" -> "byte"
            | "short int" -> "short"
            | "short unsigned int" -> "ushort"
            | "int" | "long int" -> "int"
            | "unsigned int" | "long unsigned int" -> "uint"
            | "long long int" -> "long"
            | "long long unsigned int" -> "ulong"
            | "float" -> "float"
            | "double" -> "double"
            | "long double" -> "decimal"
            | "char" -> "byte"
            | "void" -> "void"
            | value -> value
        | "CvQualifiedType" ->
            lookupType (getType element) 
        | "PointerType" -> 
            sprintf "%s*" (lookupType (getType element))
        | "ArrayType" -> 
            let typeName = lookupType (getType element)
            let max = getAttr element (XName.Get "max")
            let m = Regex.Match(max, @"\d+")
            if m.Success then
                sprintf "fixed %s [%i]" typeName ((int m.Value) + 1)
            else
                sprintf "%s[]" typeName
        | "Typedef" ->
            match isFundamental element with 
            | true -> lookupType (getType element)
            | false -> 
                match getFunctionType element with
                | Some(x) -> getName element
                | _ -> lookupType (getType element)
        | _ ->
            getName element
    
    let rec evalElementTypeName (element : XElement) =
        lookupType (getType element)

    let outputLiteral (element : XElement) = 
        let enumValues = element.Elements(XName.Get "EnumValue")
        for enumValue in enumValues do
            let name = getName enumValue
            let value = getInit enumValue
            writer.WriteLine (sprintf "internal const int %s = %s;" name value)
        writer.WriteLine ()
    
    let outputEnum (element : XElement) = 
        let typeName = getName element
        writer.WriteLine(sprintf "internal enum %s" typeName)
        beginBlock()
        let enumValues = element.Elements(XName.Get "EnumValue")
        for enumValue in enumValues do
            let name = getName enumValue
            let value = getInit enumValue
            writer.WriteLine (sprintf "%s = %s," name value)
        endBlock()
        writer.WriteLine ()
    
    let outputTypedef (element : XElement) = 
        let name = getName element

        match getFunctionType element with
        | Some(dt) -> 
            let arguments = dt.Elements(XName.Get "Argument") |> Seq.mapi (fun i x -> sprintf "%s p%i"(lookupType (getType x)) i) |> String.concat ", "
            let returnType = lookupType (getReturns dt)
            writer.WriteLine "[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]"
            writer.WriteLine (sprintf "internal unsafe delegate %s %s (%s);" returnType name arguments)
            writer.WriteLine ()
        | None -> 
            ()

    let lookupMembers (members : string) =
        let ids = members.Split([|' '|], System.StringSplitOptions.RemoveEmptyEntries)
        ids |> Seq.map lookUpById

    let getStructFields (element : XElement) =
        seq {
            match getMembers element with 
            | null -> ()
            | members ->
                for _member in lookupMembers members do
                    let name = getName _member
                    match _member.Name.LocalName with
                    | "Field" -> yield _member
                    | _ -> printfn "Skiping member %s in %O %s." name _member.Name (getName element)
        }
        
    let outputStruct (element : XElement) = 
        let typeName = getName element

        let fixTypeAndName typeName name =
            let m = Regex.Match(typeName, @"(fixed \w+) (\[\d+\])")
            if m.Success then
                sprintf "internal %s %s %s;" m.Groups.[1].Value name m.Groups.[2].Value
            else 
                sprintf "internal %s %s;" typeName name

        let fields = getStructFields element |> Seq.map (fun x -> fixTypeAndName (evalElementTypeName x) (getName x))

        writer.WriteLine "[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]"
        writer.WriteLine (sprintf "internal unsafe struct %s" typeName)
        beginBlock()
        fields |> Seq.iter writer.WriteLine
        endBlock()
        writer.WriteLine ()
    
    let outputFunc (element : XElement) =
        let name = getName element
        match getExtern element with
        | "1" ->
            let arguments = element.Elements(XName.Get "Argument") |> Seq.map (fun x -> sprintf "%s %s" (lookupType (getType x)) (getName x)) |> String.concat ", "
            let returnType = lookupType (getReturns element)
            writer.WriteLine (sprintf "[DllImport(@\"%s\", CallingConvention = CallingConvention.Cdecl, CharSet=CharSet.Ansi)]" dllName)
            writer.WriteLine (sprintf "internal static extern %s %s(%s);" returnType name arguments)
            writer.WriteLine ()
        | _ -> printfn "Skipping function: %s" name

    let rec getDependentTypesById id =
        seq {
            let element = lookUpById id
            match element.Name.LocalName with
            | "FundamentalType" -> ()
            | "CvQualifiedType" | "PointerType" | "ArrayType" ->
                yield! getDependentTypesById (getType element)
            | "Typedef" ->
                match isFundamental element with 
                | true -> ()
                | false -> yield element
            | _ ->
                yield element
        }

    let getFunctionDependencies (element : XElement) =
        seq {
            let arguments = element.Elements(XName.Get "Argument")
            yield! arguments |> Seq.collect (fun x -> getDependentTypesById(getType x))
            yield! getDependentTypesById (getReturns element)
        }

    let getStructDependencies (element : XElement) =
        let fields = getStructFields element
        fields |> Seq.collect (fun x -> getDependentTypesById(getType x))

    let getTypedefDependencies (element : XElement) =
        getDependentTypesById(getType element)

    let knownTypeId = new HashSet<string>()

    let rec outputType (element : XElement) =
        let id = getId element
        match knownTypeId.Contains id with 
        | true -> ()
        | false ->
            knownTypeId.Add id |> ignore
            match element.Name.LocalName with
            | "Function" -> 
                getFunctionDependencies element |> Seq.iter outputType
                outputFunc element
            | "Struct" -> 
                getStructDependencies element |> Seq.iter outputType
                outputStruct element
            | "Typedef" -> 
                getTypedefDependencies element |> Seq.iter outputType
                outputTypedef element
            | "FunctionType" -> 
                getFunctionDependencies element |> Seq.iter outputType
            | "Enumeration" -> outputEnum element
            | name -> printfn "Skipping element %s with id = %s" name id

    member x.Convert() = 
        writer.WriteLine "using System.Runtime.InteropServices;"
        writer.WriteLine ()

        writer.WriteLine "namespace OpenNI2"
        beginBlock()

        let localElementsFilterByName name = 
            let xnName = XName.Get name
            localElements |> Seq.filter (fun x -> x.Name = xnName)

        let enums = localElementsFilterByName "Enumeration" |> Seq.filter (fun x -> (getArtificial x) = null)
        enums |> Seq.iter outputType

        let structs = localElementsFilterByName "Struct"
        structs |> Seq.iter outputType
    
        let typedefs = localElementsFilterByName "Typedef"
        typedefs |> Seq.iter outputType

        writer.WriteLine "internal static unsafe class OniCAPI"
        beginBlock()

        let literals = localElementsFilterByName "Enumeration" |> Seq.filter (fun x -> (getArtificial x) <> null)
        literals |> Seq.iter outputLiteral

        let functions = localElementsFilterByName "Function"
        functions |> Seq.iter outputType
         
        endBlock()
        endBlock()