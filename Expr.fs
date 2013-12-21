module FsPlot.Expr

open System
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Reflection
open Options
open DataSeries

let quoteStringArr (arr:string []) =
    Expr.NewArray(
        typeof<string>,
        [for str in arr -> Expr.Value str])

let strOptionInfos = FSharpType.GetUnionCases(typeof<string option>)

let quoteStrOption (strOption:string option) =
    match strOption with
    | None -> Expr.NewUnionCase(strOptionInfos.[0], [])
    | Some value -> Expr.NewUnionCase(strOptionInfos.[1], [Expr.Value value])

let quoteBool (b:bool) = Expr.Value b

let chartTypeInfos = FSharpType.GetUnionCases(typeof<ChartType>)

let chartTypeExpr x = Expr.NewUnionCase(chartTypeInfos.[x], [])

let quoteChartType (chartType:ChartType) =
    match chartType with
    | Area -> chartTypeExpr 0
    | Areaspline -> chartTypeExpr 1
    | Arearange -> chartTypeExpr 2
    | Bar -> chartTypeExpr 3
    | Bubble -> chartTypeExpr 4
    | Column -> chartTypeExpr 5
    | Combination -> chartTypeExpr 6
    | Line -> chartTypeExpr 7
    | Pie -> chartTypeExpr 8
    | Scatter -> chartTypeExpr 9

let boxArrExpr (arr:obj []) =
    Expr.Call(
        boxInfo,
        [
            Expr.NewArray(
                typeof<obj>,
                Array.map (fun x -> Expr.Value x) arr
                |> Array.toList)
//                [
//                    Expr.Value arr.[0]
//                    Expr.Value arr.[1]
//                ])
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
        ])

let quoteSeriesArr (series:Series []) =
    Expr.NewArray(
        typeof<Series>,
        [
            for x in series do
                yield areaSeriesExpr x
        ])

let stackingInfos = Reflection.FSharpType.GetUnionCases(typeof<Stacking>)

let quoteStacking (stacking:Stacking) =
    match stacking with
    | Disabled -> Expr.NewUnionCase(stackingInfos.[0], [])
    | Normal -> Expr.NewUnionCase(stackingInfos.[1], [])
    | Percent -> Expr.NewUnionCase(stackingInfos.[2], [])
