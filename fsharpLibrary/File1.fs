module Translations.From

open System
open System.Xml
open System.Collections.Generic

type List = System.Collections.Generic.List<string>

let getNames (name:XmlNode) = name.InnerText.ToString()

let Xml(input:string) =
    let getDoc =
        let mutable doc = new XmlDocument()
        try
            doc.Load(input) |> ignore
        with
            | :? System.IO.FileNotFoundException as ex ->
                printfn "%s" ex.Message
                doc <- null
        doc
    let document =
        if getDoc <> null then
            getDoc
        else raise (new System.NullReferenceException())
    let names = new List()
    let ages = new List()

    let nodeSelect (name:string, out:List) =
        document.SelectNodes(name)
            |> Seq.cast<XmlNode>
            |> Seq.map (fun n -> n.ChildNodes)
            |> Seq.map (Seq.cast<XmlNode>)
            |> Seq.concat
            |> Seq.map (getNames)
            |> Seq.iter (fun e -> out.Add(e) |> ignore)

    nodeSelect("//name", names)
    nodeSelect("//age", ages)

    (names, ages)

