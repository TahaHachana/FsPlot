module FsPlot.Quote

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Reflection
open System
open DataSeries
open Options

[<ReflectedDefinition>]
type Boxer = static member Box (x:obj) = box x

let boxInfo = typeof<Boxer>.GetMethod("Box")

let strOptionInfos = FSharpType.GetUnionCases(typeof<string option>)
let chartTypeInfos = FSharpType.GetUnionCases(typeof<ChartType>)
let stackingInfos = Reflection.FSharpType.GetUnionCases(typeof<Stacking>)
let pieOptionsInfos = FSharpType.GetUnionCases(typeof<PieOptions option>)

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
    | Pie -> chartTypeExpr 10
    | Radar -> chartTypeExpr 11
    | Scatter -> chartTypeExpr 12
    | Spline -> chartTypeExpr 13

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
        [
            for x in series do
                yield areaSeriesExpr x
        ])

let quoteStacking (stacking:Stacking) =
    match stacking with
    | Disabled -> Expr.NewUnionCase(stackingInfos.[0], [])
    | Normal -> Expr.NewUnionCase(stackingInfos.[1], [])
    | Percent -> Expr.NewUnionCase(stackingInfos.[2], [])

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