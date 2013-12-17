module internal FsPlot.Expr

open System
open Microsoft.FSharp.Quotations
open DataSeries

//let quoteTupleSeq (col:seq<#key*#value>) =
//    Expr.NewArray(
//        typeof<System.Tuple<string,value>>,
//        [
//            for (k, v) in col do
//                yield Expr.NewTuple(
//                    [
//                        Expr.Value(k.ToString())
//                        Expr.Value (v :> value)
//                    ])
//        ])

let quoteStringArr (arr:string []) =
    Expr.NewArray(
        typeof<string>,
        [
            for str in arr do
                yield Expr.Value str
        ])

let strOptionInfos = Reflection.FSharpType.GetUnionCases(typeof<string option>)

let quoteStrOption (strOption:string option) =
    match strOption with
    | None -> Expr.NewUnionCase(strOptionInfos.[0], [])
    | Some value -> Expr.NewUnionCase(strOptionInfos.[1], [Expr.Value value])

let quoteBool (b:bool) = Expr.Value b

let chartTypeInfos = Reflection.FSharpType.GetUnionCases(typeof<ChartType>)

let chartTypeExpr x = Expr.NewUnionCase(chartTypeInfos.[0], [])

let quoteChartType (chartType:ChartType) =
    match chartType with
    | Area -> chartTypeExpr 0
    | Bar -> chartTypeExpr 1
    | Bubble -> chartTypeExpr 2
    | Column -> chartTypeExpr 3
    | Combination -> chartTypeExpr 4
    | Line -> chartTypeExpr 5
    | Pie -> chartTypeExpr 6
    | Scatter -> chartTypeExpr 7

let boxArrExpr (arr:obj []) =
    Expr.Call(
        boxInfo,
        [
            Expr.NewArray(
                typeof<obj>,
                [
                    Expr.Value arr.[0]
                    Expr.Value arr.[1]
                ])
        ])

let objArrExpr (arr:obj []) xType =
    Expr.NewArray(
        typeof<obj>,
        [
            for x in arr do
                match xType with
                | TypeCode.Empty -> yield Expr.Call(boxInfo, [Expr.Value x])
                | _ ->
                    let arr = unbox<obj []> x
                    yield boxArrExpr arr
//                        Expr.Call(
//                            boxInfo,
//                            [
//                                Expr.NewArray(
//                                    typeof<obj>,
//                                    [
//                                        Expr.Value arr.[0]
//                                        Expr.Value arr.[1]
//                                    ])
//                            ])
        ])

let areaSeriesExpr (x:Series) =
    Expr.NewRecord(
        typeof<Series>,
        [
            Expr.Value x.Name
            quoteChartType x.Type
            objArrExpr x.Values x.XType
//            Expr.NewArray(
//                typeof<obj>,
//                [
//                    for y in x.Values do
//                            match x.XType with
//                            | TypeCode.Empty -> yield Expr.Call(boxInfo, [Expr.Value y])
//                            | _ ->
//                                let arr = unbox<obj []> y
//                                yield Expr.Call(
//                                    boxInfo,
//                                    [
//                                        Expr.NewArray(
//                                            typeof<obj>,
//                                            [
//                                                Expr.Value arr.[0]
//                                                Expr.Value arr.[1]
//                                            ])
//                                    ])
//                ])
            Expr.Value x.XType
            Expr.Value x.YType
        ])

let quoteSeriesArr (series:Series []) =
    Expr.NewArray(
        typeof<Series>,
        [
            for x in series do
                yield areaSeriesExpr x
//                    Expr.NewRecord(
//                        typeof<Series>,
//                        [
//                            Expr.Value x.Name
//                            Expr.NewArray(
//                                typeof<obj>,
//                                [
//                                    for y in x.Values do
//                                            match x.XType with
//                                            | TypeCode.Empty -> yield Expr.Call(boxInfo, [Expr.Value y])
//                                            | _ ->
//                                                let arr = unbox<obj []> y
//                                                yield Expr.Call(
//                                                    boxInfo,
//                                                    [
//                                                        Expr.NewArray(
//                                                            typeof<obj>,
//                                                            [
//                                                                Expr.Value arr.[0]
//                                                                Expr.Value arr.[1]
//                                                            ])
//                                                    ])
//                                ])
//                            quoteChartType x.Type
//                            Expr.Value x.XType
//                            Expr.Value x.YType
//                        ])
        ])

//let test =
//    ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]
//    |> Series.New "Browser Share" Pie
//
//let ex = quoteDataSeriesArr [|test|]

//
//
//let quoteSeriesArr (series:Series []) =
//    Expr.NewArray(
//        typeof<Series>,
//        [
//            for x in series do
//                yield
//                    Expr.NewRecord(
//                        typeof<Series>,
//                        [
//                            Expr.Value x.Name
//                            Expr.NewArray(
//                                typeof<obj>,
//                                [
//                                    for y in x.Values do
//                                        match x.Size with
//                                        | TypeCode.Empty ->
//                                            match x.XType with
//                                            | TypeCode.Empty -> yield Expr.Call(boxInfo, [Expr.Value e])
//                                            | _ ->
//                                                let arr = unbox<obj []> e
//                                                yield Expr.Call(
//                                                    boxInfo,
//                                                    [Expr.NewArray(
//                                                        typeof<obj>,
//                                                        [
//                                                            Expr.Value arr.[0]
//                                                            Expr.Value arr.[1]
//                                                        ])])
//                                        | _ ->
//                                            match ds.XType with
//                                            | TypeCode.Empty -> 
//                                                let arr = unbox<obj []> e
//                                                yield Expr.Call(
//                                                    boxInfo,
//                                                    [Expr.NewArray(
//                                                        typeof<obj>,
//                                                        [
//                                                            Expr.Value arr.[0]
//                                                            Expr.Value arr.[1]
//                                                        ])])
//                                            | _ ->
//                                                let arr = unbox<obj []> e
//                                                yield Expr.Call(
//                                                    boxInfo,
//                                                    [Expr.NewArray(
//                                                        typeof<obj>,
//                                                        [
//                                                            Expr.Value arr.[0]
//                                                            Expr.Value arr.[1]
//                                                            Expr.Value arr.[2]
//                                                        ])]
//                                                )
//                                ])
//                            quoteChartType ds.Type
//                            Expr.Value ds.XType
//                            Expr.Value ds.YType
//                            Expr.Value ds.Size
//                        ])
//        ])
