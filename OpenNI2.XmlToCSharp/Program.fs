open OpenNI2.XmlToCSharp.Convertor

open System.CodeDom.Compiler
open System.IO
open System.Xml.Linq

let DllName = "OpenNI2"

let xmlOniCAPIPath = Path.GetFullPath "../../../Data/OniCAPI.xml"
let outputOniCAPIPath = Path.GetFullPath "../../../OpenNI2ManagedWrapper/OniCAPI.generated.cs"    

let xmlPS1080Path = Path.GetFullPath "../../../Data/PS1080.xml"
let outputPS1080Path = Path.GetFullPath "../../../OpenNI2ManagedWrapper/PS1080.generated.cs"    

[<EntryPoint>]
let main argv = 
    let convert inPath outPath =
        printfn "converting: %s -> %s" inPath outPath
        let document = XDocument.Load inPath
        use writer = new IndentedTextWriter (new StreamWriter (File.Open (outPath, FileMode.Create)))
        let converter = Converter (document, writer, DllName)
        converter.Convert ()
        writer.Flush ()
    
    convert xmlOniCAPIPath outputOniCAPIPath

    convert xmlPS1080Path outputPS1080Path

    0 // return an integer exit code
