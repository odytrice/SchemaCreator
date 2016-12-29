module DGML

open System
open System.Text
open System.Xml

type DgmlHelper(outFile : string) = 
    let xtw = new XmlTextWriter(outFile, Encoding.UTF8)
    
    let init() = 
        xtw.Formatting <- Formatting.Indented
        xtw.WriteStartDocument()
        xtw.WriteStartElement("DirectedGraph", "http://schemas.microsoft.com/vs/2009/dgml")
        xtw.WriteAttributeString("GraphDirection", "LeftToRight")
    
    do init()
    
    member this.WriteNode(id : string, label : string) = 
        xtw.WriteStartElement("Node")
        xtw.WriteAttributeString("Id", id)
        xtw.WriteAttributeString("Label", label)
        xtw.WriteEndElement()
    
    member this.WriteNode(id : string, label : string, reference : string) = 
        xtw.WriteStartElement("Node")
        xtw.WriteAttributeString("Id", id)
        xtw.WriteAttributeString("Label", label)
        if not (String.IsNullOrEmpty(reference)) then xtw.WriteAttributeString("Reference", reference)
        xtw.WriteEndElement()
    
    member this.WriteLink(source : string, target : string, label : string) = 
        xtw.WriteStartElement("Link")
        xtw.WriteAttributeString("Source", source)
        xtw.WriteAttributeString("Target", target)
        if not (String.IsNullOrEmpty(label)) then xtw.WriteAttributeString("Label", label)
        xtw.WriteEndElement()
    
    member this.BeginElement(element : string) = xtw.WriteStartElement(element)
    member this.EndElement() = xtw.WriteEndElement()
    member this.Close() = 
        xtw.WriteEndElement()
        xtw.Close()
