module FsPlot.Quote

open FsPlot.Config
open FsPlot.Data
open FsPlot.Highcharts.Options
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Reflection
open System

[<ReflectedDefinition>]
type Boxer = static member Box (x:obj) = box x

let boxInfo = typeof<Boxer>.GetMethod("Box")
let strOptionInfos = FSharpType.GetUnionCases typeof<string option>
let chartTypeInfos = FSharpType.GetUnionCases typeof<ChartType>
let stackingInfos = FSharpType.GetUnionCases typeof<Stacking>
let axisTypeInfos = FSharpType.GetUnionCases typeof<AxisType>
let pieOptionsInfos = FSharpType.GetUnionCases typeof<PieOptions option>
let chartTypeExpr x = Expr.NewUnionCase(chartTypeInfos.[x], [])

let quoteStringArr (arr:string []) =
    Expr.NewArray(
        typeof<string>,
        [for str in arr -> Expr.Value str])

let quoteStrOption (strOption:string option) =
    match strOption with
    | None -> Expr.NewUnionCase(strOptionInfos.[0], [])
    | Some value -> Expr.NewUnionCase(strOptionInfos.[1], [Expr.Value value])

let quoteBool (b:bool) = Expr.Value b

let quoteChartType (chartType:ChartType) =
    match chartType with
    | Area -> chartTypeExpr 0
    | Areaspline -> chartTypeExpr 1
    | Arearange -> chartTypeExpr 2
    | Bar -> chartTypeExpr 3
    | Bubble -> chartTypeExpr 4
    | Column -> chartTypeExpr 5
    | Combination -> chartTypeExpr 6
    | Donut -> chartTypeExpr 7
    | Funnel -> chartTypeExpr 8
    | Line -> chartTypeExpr 9
    | PercentArea -> chartTypeExpr 10
    | PercentBar -> chartTypeExpr 11
    | PercentColumn -> chartTypeExpr 12
    | Pie -> chartTypeExpr 13
    | Radar -> chartTypeExpr 14
    | Scatter -> chartTypeExpr 15
    | Spline -> chartTypeExpr 16
    | StackedArea -> chartTypeExpr 17
    | StackedBar -> chartTypeExpr 18
    | StackedColumn -> chartTypeExpr 19

let boxArrExpr (arr:obj []) =
    Expr.Call(
        boxInfo,
        [
            Expr.NewArray(
                typeof<obj>,
                Array.map (fun x -> Expr.Value x) arr
                |> Array.toList)
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
        [for x in series -> areaSeriesExpr x])

let quoteStacking (stacking:Stacking) =
    match stacking with
    | Disabled -> Expr.NewUnionCase(stackingInfos.[0], [])
    | Normal -> Expr.NewUnionCase(stackingInfos.[1], [])
    | Percent -> Expr.NewUnionCase(stackingInfos.[2], [])

let quoteAxisType (axisType:AxisType) =
    match axisType with
    | Category -> Expr.NewUnionCase(axisTypeInfos.[0], [])
    | DateTime -> Expr.NewUnionCase(axisTypeInfos.[1], [])
    | Linear -> Expr.NewUnionCase(axisTypeInfos.[2], [])

let pieOptionsExpr (x:PieOptions) =
    Expr.NewRecord(
        typeof<PieOptions>,
        [
            Expr.NewArray(
                typeof<int>,
                [for i in x.Center -> Expr.Value i])
            Expr.Call(boxInfo, [Expr.Value x.Size])
        ])

let quotePieOptions (x:PieOptions option) =
    match x with
    | None -> Expr.NewUnionCase(pieOptionsInfos.[0], [])
    | Some value -> Expr.NewUnionCase(pieOptionsInfos.[1], [pieOptionsExpr value])

let quoteChartConfig (x:ChartConfig) =
    Expr.NewRecord(
        typeof<ChartConfig>,
        [
            quoteStringArr x.Categories
            quoteSeriesArr x.Data
            quoteBool x.Legend
            quoteStrOption x.Subtitle
            quoteStrOption x.Title
            quoteStrOption x.Tooltip
            quoteChartType x.Type
            quoteAxisType x.XAxis
            quoteStrOption x.XTitle
            quoteStrOption x.YTitle
        ])
