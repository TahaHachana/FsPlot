module internal FsPlot.Expr

open System
open Microsoft.FSharp.Quotations
open DataSeries

let quoteTupleSeq (col:seq<#key*#value>) =
    Expr.NewArray(
        typeof<System.Tuple<string,value>>,
        [
            for (k, v) in col do
                yield Expr.NewTuple(
                    [
                        Expr.Value(k.ToString())
                        Expr.Value (v :> value)
                    ])
        ])

let quoteStringArr (arr:string []) =
    Expr.NewArray(
        typeof<string>,
        [
            for str in arr do
                yield Expr.Value str
        ])

let quoteStrOption (strOption:string option) =
    let infos = Reflection.FSharpType.GetUnionCases(typeof<string option>)
    match strOption with
    | None -> Expr.NewUnionCase(infos.[0], [])
    | Some value -> Expr.NewUnionCase(infos.[1], [Expr.Value value])

let quoteBool (b:bool) = Expr.Value b

let quoteChartType (chartType:ChartType) =
    let infos = Reflection.FSharpType.GetUnionCases(typeof<ChartType>)
    match chartType with
    | Area -> Expr.NewUnionCase(infos.[0], [])
    | Bar -> Expr.NewUnionCase(infos.[1], [])
    | Bubble -> Expr.NewUnionCase(infos.[2], [])
    | Column -> Expr.NewUnionCase(infos.[3], [])
    | Combination -> Expr.NewUnionCase(infos.[4], [])
    | Line -> Expr.NewUnionCase(infos.[5], [])
    | Pie -> Expr.NewUnionCase(infos.[6], [])
    | Scatter -> Expr.NewUnionCase(infos.[7], [])

let quoteDataSeriesArr (dataSeries:Series []) =
    Expr.NewArray(
        typeof<Series>,
        [
            for ds in dataSeries do
                yield
                    Expr.NewRecord(
                        typeof<Series>,
                        [
                            Expr.Value ds.Name
                            Expr.NewArray(
                                typeof<obj>,
                                [
                                    for e in ds.Values do
                                        match ds.Size with
                                        | TypeCode.Empty ->
                                            match ds.XType with
                                            | TypeCode.Empty -> yield Expr.Call(boxInfo, [Expr.Value e])
                                            | _ ->
                                                let arr = unbox<obj []> e
                                                yield Expr.Call(
                                                    boxInfo,
                                                    [Expr.NewArray(
                                                        typeof<obj>,
                                                        [
                                                            Expr.Value arr.[0]
                                                            Expr.Value arr.[1]
                                                        ])])
                                        | _ ->
                                            match ds.XType with
                                            | TypeCode.Empty -> 
                                                let arr = unbox<obj []> e
                                                yield Expr.Call(
                                                    boxInfo,
                                                    [Expr.NewArray(
                                                        typeof<obj>,
                                                        [
                                                            Expr.Value arr.[0]
                                                            Expr.Value arr.[1]
                                                        ])])
                                            | _ ->
                                                let arr = unbox<obj []> e
                                                yield Expr.Call(
                                                    boxInfo,
                                                    [Expr.NewArray(
                                                        typeof<obj>,
                                                        [
                                                            Expr.Value arr.[0]
                                                            Expr.Value arr.[1]
                                                            Expr.Value arr.[2]
                                                        ])]
                                                )
                                ])
                            quoteChartType ds.Type
                            Expr.Value ds.XType
                            Expr.Value ds.YType
                            Expr.Value ds.Size
                        ])
        ])

//let test =
//    ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]
//    |> Series.New "Browser Share" Pie
//
//let ex = quoteDataSeriesArr [|test|]



