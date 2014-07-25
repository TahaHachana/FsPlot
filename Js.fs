module internal FsPlot.Js

open FsPlot.Config
open FsPlot.Data
open FsPlot.Highcharts
open FsPlot.Highcharts.Options

let highcharts (config:ChartConfig) =
    match config.Type with
    | Area -> Js.area config Disabled false
    | Areaspline -> Js.areaspline config Disabled false
    | Arearange -> Js.arearange config
    | Bar -> Js.bar config Disabled
    | Bubble -> Js.bubble config
    | Column -> Js.column config Disabled
    | Combination -> Js.combine config None
    | Donut -> Js.donut config
    | Funnel -> Js.funnel config
    | Line -> Js.line config
    | PercentArea -> Js.percentArea config false
    | PercentBar -> Js.percentBar config
    | PercentColumn -> Js.percentColumn config
    | Pie -> Js.pie config
    | Radar -> Js.radar config
    | Scatter -> Js.scatter config
    | Spline -> Js.spline config
    | StackedArea -> Js.stackedArea config false
    | StackedBar -> Js.stackedBar config
    | StackedColumn -> Js.stackedColumn config

let dynamicHighcharts address guid shift (config:ChartConfig) =
    match config.Type with
    | Area -> Js.dynamicArea address guid shift config
    | Areaspline -> Js.dynamicAreaspline address guid shift config
    | Arearange-> Js.dynamicArearange address guid shift config
    | Bar -> Js.dynamicBar address guid shift config
    | Bubble -> Js.dynamicBubble address guid shift config
    | Column -> Js.dynamicColumn address guid shift config
    | Donut -> Js.dynamicDonut address guid shift config
    | Funnel -> Js.dynamicFunnel address guid shift config
    | Line -> Js.dynamicLine address guid shift config
    | Pie -> Js.dynamicPie address guid shift config
    | Radar -> Js.dynamicRadar address guid shift config
    | Scatter -> Js.dynamicScatter address guid shift config
    | _ -> Js.dynamicSpline address guid shift config

let google (config:ChartConfig) =
    match config.Type with
    | _ -> Google.Js.bar config
