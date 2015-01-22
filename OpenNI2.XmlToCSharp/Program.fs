open OpenNI2.XmlToCSharp.Convertor

open System.CodeDom.Compiler
open System.IO
open System.Xml.Linq

let DllName = "OpenNI2"
let xmlPath = Path.GetFullPath "../../../Data/OniCAPI.xml"
let fsPath = Path.GetFullPath "../../../OpenNI2ManagedWrapper/OniCAPI.generated.cs"    

[<EntryPoint>]
let main argv = 
    printfn "input: %s" xmlPath
    let document = XDocument.Load xmlPath
    printfn "output: %s" fsPath
    use writer = new IndentedTextWriter (new StreamWriter (File.Open (fsPath, FileMode.Create)))
    let converter = Converter (document, writer, DllName)
    converter.Convert ()
    writer.Flush ()
    0 // return an integer exit code
