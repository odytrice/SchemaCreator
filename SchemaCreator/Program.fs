﻿// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv =
    Generator.Generate() |> ignore 
    printfn "%A" argv
    0 // return an integer exit code
