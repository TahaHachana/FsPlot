module FsPlot.Expr

open Microsoft.FSharp.Quotations

type value = System.IConvertible

let quoteTupleSeq (col:seq<string*#value>) =
    Expr.NewArray(
        typeof<System.Tuple<string,value>>,
        [
            for (k, v) in col do
                yield Expr.NewTuple(
                    [
                        Expr.Value(k)
                        Expr.Value (v :> value)
                    ])
        ])

let quoteSeq (col:seq<#value>) =
    Expr.NewArray(
        typeof<value>,
        [
            for v in col do
                yield Expr.Value (v :> value)
        ])

let quoteStrOption (strOption:string option) =
    let infos = Reflection.FSharpType.GetUnionCases(typeof<string option>)
    match strOption with
    | None -> Expr.NewUnionCase(infos.[0], [])
    | Some value -> Expr.NewUnionCase(infos.[1], [Expr.Value value])

let quoteBool (b:bool) = Expr.Value b