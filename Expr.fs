module internal FsPlot.Expr

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

//let quoteSeq (col:seq<#value>) =
//    Expr.NewArray(
//        typeof<value>,
//        [
//            for v in col do
//                yield Expr.Value (v :> value)
//        ])

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
    | Column -> Expr.NewUnionCase(infos.[2], [])
    | Line -> Expr.NewUnionCase(infos.[3], [])
    | Pie -> Expr.NewUnionCase(infos.[4], [])

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
                                typeof<System.Tuple<key,value>>,
                                [
                                    for (k, v) in ds.Values do
                                        yield Expr.NewTuple(
                                            [
                                                Expr.Value(k)
                                                Expr.Value (v)
                                            ])
                                ])
                            quoteChartType ds.Type
                            Expr.Value ds.XType
                            Expr.Value ds.YType
                        ])
        ])

//let test =
//    ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]
//    |> Series.New "Browser Share" Pie
//
//let ex = quoteDataSeriesArr [|test|]



