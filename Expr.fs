module internal FsPlot.Expr

open System
open Microsoft.FSharp.Quotations
open DataSeries

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
        ])

let areaSeriesExpr (x:Series) =
    Expr.NewRecord(
        typeof<Series>,
        [
            Expr.Value x.Name
            quoteChartType x.Type
            objArrExpr x.Values x.XType
            Expr.Value x.XType
            Expr.Value x.YType
        ])

let quoteSeriesArr (series:Series []) =
    Expr.NewArray(
        typeof<Series>,
        [
            for x in series do
                yield areaSeriesExpr x
        ])