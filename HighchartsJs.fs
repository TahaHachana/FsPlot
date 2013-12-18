module FsPlot.HighchartsJs

#if INTERACTIVE
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#endif

open Microsoft.FSharp.Quotations
open System
open FunScript
open Options
open DataSeries
open Expr

[<ReflectedDefinition>]
module internal Utils =

    let jq(selector:string) = Globals.Dollar.Invoke selector

    let areaChartOptions renderTo chartType inverted (options:HighchartsOptions) =
        let chartOptions = createEmpty<HighchartsChartOptions>()
        chartOptions.renderTo <- renderTo
        chartOptions._type <- chartType
        chartOptions.inverted <- inverted
        options.chart <- chartOptions

    let setChartOptions renderTo chartType (options:HighchartsOptions) =
        let chartOptions = createEmpty<HighchartsChartOptions>()
        chartOptions.renderTo <- renderTo
        chartOptions._type <- chartType
        options.chart <- chartOptions

    let setXAxisOptions xType (options:HighchartsOptions) (categories:string []) xTitle =
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let xAxisType =
            match categories.Length with
            | 0 ->
                match xType with
                | TypeCode.DateTime -> "datetime"
                | TypeCode.String -> "category"
                | _ -> "linear"
            | _ ->
                axisOptions.categories <- categories
                "category"
        axisOptions._type <- xAxisType
        let axisTitle = createEmpty<HighchartsAxisTitle>()
        axisTitle.text <- defaultArg xTitle ""
        axisOptions.title <- axisTitle
        options.xAxis <- axisOptions

    let setYAxisOptions (options:HighchartsOptions) yTitle =
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let axisTitle = createEmpty<HighchartsAxisTitle>()
        axisTitle.text <- defaultArg yTitle ""
        axisOptions.title <- axisTitle
        options.yAxis <- axisOptions

    let setTitleOptions chartTitle (options:HighchartsOptions) =
        let titleOptions = createEmpty<HighchartsTitleOptions>()
        titleOptions.text <- defaultArg chartTitle ""
        options.title <- titleOptions

    let setSeriesChartType chartType (options:HighchartsSeriesOptions) =
        let chartTypeStr = 
            match chartType with
            | Area -> "area"
            | Areaspline -> "areaspline"
            | Bar -> "bar"
            | Bubble -> "bubble"
            | Combination -> ""
            | Column -> "column"
            | Line -> "line"
            | Pie -> "pie"
            | Scatter -> "scatter"
        options._type <- chartTypeStr

    let setSeriesOptions (series:Series []) (options:HighchartsOptions) =
        let seriesOptions =
            [|
                for x in series do
                    let options = createEmpty<HighchartsSeriesOptions>()
                    options.data <- x.Values
                    options.name <- x.Name
                    setSeriesChartType x.Type options
                    yield options
            |]
        options.series <- seriesOptions

    let setTooltipOptions pointFormat (options:HighchartsOptions) =
        match pointFormat with
        | None -> ()
        | Some value ->
            let tooltipOptions = createEmpty<HighchartsTooltipOptions>()
            tooltipOptions.pointFormat <- value
            options.tooltip <- tooltipOptions

    let setAreaMarker (areaChart:HighchartsAreaChart) =
        let marker = createEmpty<HighchartsMarker>()
        marker.enabled <- false
        marker.radius <- 2.
        let state = createEmpty<HighchartsMarkerState>()
        state.enabled <- true
        let states = createEmpty<AnonymousType1905>()
        states.hover <- state
        marker.states <- states
        areaChart.marker <- marker

    let setSubtitle subtitle (options:HighchartsOptions) =
        match subtitle with
        | None -> ()
        | Some value ->
            let subtitleOptions = createEmpty<HighchartsSubtitleOptions>()
            subtitleOptions.text <- value
            options.subtitle <- subtitleOptions
    
    let areaStacking stacking (areaChart:HighchartsAreaChart) =
        match stacking with
        | Disabled -> ()
        | Normal -> areaChart.stacking <- "normal"
        | Percent -> areaChart.stacking <- "percent"

open Utils

let private quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let seriesExpr = quoteSeriesArr series
    let chartTitleExpr = quoteStrOption chartTitle
    let legendExpr = quoteBool legend
    let categoriesExpr = quoteStringArr categories
    let xTitleExpr = quoteStrOption xTitle
    let yTitleExpr = quoteStrOption yTitle
    let pointFormatExpr = quoteStrOption pointFormat
    let subtitleExpr = quoteStrOption subtitle
    let stackingExpr = quoteStacking stacking
    seriesExpr, chartTitleExpr, legendExpr, categoriesExpr, xTitleExpr, yTitleExpr, pointFormatExpr, subtitleExpr, stackingExpr

let private quoteArgs' series chartTitle legend categories xTitle yTitle pointFormat subtitle =
    let seriesExpr = quoteSeriesArr series
    let chartTitleExpr = quoteStrOption chartTitle
    let legendExpr = quoteBool legend
    let categoriesExpr = quoteStringArr categories
    let xTitleExpr = quoteStrOption xTitle
    let yTitleExpr = quoteStrOption yTitle
    let pointFormatExpr = quoteStrOption pointFormat
    let subtitleExpr = quoteStrOption subtitle
    seriesExpr, chartTitleExpr, legendExpr, categoriesExpr, xTitleExpr, yTitleExpr, pointFormatExpr, subtitleExpr

[<ReflectedDefinition>]
let private areaChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle pointFormat subtitle stacking inverted =
    let options = createEmpty<HighchartsOptions>()
    areaChartOptions "chart" "area" inverted options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let areaChart = createEmpty<HighchartsAreaChart>()
    areaChart.showInLegend <- legend
    setAreaMarker areaChart
    areaStacking stacking areaChart
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.area <- areaChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let area series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking inverted =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8 =
        quoteArgs' series chartTitle legend categories xTitle yTitle pointFormat subtitle
    let stackingExpr = quoteStacking stacking
    let invertedExpr = quoteBool inverted
    Compiler.Compiler.Compile(
        <@ areaChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%stackingExpr %%invertedExpr @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private areasplineChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle pointFormat subtitle stacking inverted =
    let options = createEmpty<HighchartsOptions>()
    areaChartOptions "chart" "area" inverted options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let areaChart = createEmpty<HighchartsAreaChart>()
    areaChart.showInLegend <- legend
    setAreaMarker areaChart
    areaStacking stacking areaChart
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.area <- areaChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let areaspline series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking inverted =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8 =
        quoteArgs' series chartTitle legend categories xTitle yTitle pointFormat subtitle
    let stackingExpr = quoteStacking stacking
    let invertedExpr = quoteBool inverted
    Compiler.Compiler.Compile(
        <@ areasplineChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%stackingExpr %%invertedExpr @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private barChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle (pointFormat:string option) subtitle stacking =
    let options = createEmpty<HighchartsOptions>()
    setChartOptions "chart" "bar" options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let barChart = createEmpty<HighchartsBarChart>()
    barChart.showInLegend <- legend
    match stacking with
    | Disabled -> ()
    | Normal -> barChart.stacking <- "normal"
    | Percent -> barChart.stacking <- "percent"
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.bar <- barChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let bar series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8, expr9 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking
    Compiler.Compiler.Compile(
        <@ barChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%expr9 @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private bubbleChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle (pointFormat:string option) subtitle (stacking:Stacking) =
    let options = createEmpty<HighchartsOptions>()
    // chart options
    setChartOptions "chart" "bubble" options
    // x axis options
    setXAxisOptions series.[0].XType options categories xTitle
    // y axis options
    setYAxisOptions  options yTitle
    // title options
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setTooltipOptions pointFormat options    
    // series options
    setSeriesOptions series options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let bubble series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8, expr9 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking
    Compiler.Compiler.Compile(
        <@ bubbleChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%expr9 @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private columnChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle (pointFormat:string option) subtitle stacking =
    let options = createEmpty<HighchartsOptions>()
    setChartOptions "chart" "column" options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let barChart = createEmpty<HighchartsBarChart>()
    barChart.showInLegend <- legend
    match stacking with
    | Disabled -> ()
    | Normal -> barChart.stacking <- "normal"
    | Percent -> barChart.stacking <- "percent"
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.bar <- barChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let column series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8, expr9 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking
    Compiler.Compiler.Compile(
        <@ columnChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%expr9 @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private combChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle (pointFormat:string option) (subtitle:string option) (stacking:Stacking) =
        let options = createEmpty<HighchartsOptions>()
        // x axis options
        setXAxisOptions series.[0].XType options categories xTitle
        // y axis options
        setYAxisOptions options yTitle
        // title options
        setTitleOptions chartTitle options
        // series options
        setSeriesOptions series options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

let comb series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8, expr9 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking
    Compiler.Compiler.Compile(
        <@ combChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%expr9 @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private lineChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle (pointFormat:string option) subtitle (stacking:Stacking) =
    let options = createEmpty<HighchartsOptions>()
    setChartOptions "chart" "line" options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let barChart = createEmpty<HighchartsBarChart>()
    barChart.showInLegend <- legend
//    match stacking with
//    | Disabled -> ()
//    | Normal -> barChart.stacking <- "normal"
//    | Percent -> barChart.stacking <- "percent"
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.bar <- barChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let line series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8, expr9 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking
    Compiler.Compiler.Compile(
        <@ lineChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%expr9 @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private pieChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle (pointFormat:string option) subtitle (stacking:Stacking) =
    let options = createEmpty<HighchartsOptions>()
    setChartOptions "chart" "line" options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let barChart = createEmpty<HighchartsBarChart>()
    barChart.showInLegend <- legend
//    match stacking with
//    | Disabled -> ()
//    | Normal -> barChart.stacking <- "normal"
//    | Percent -> barChart.stacking <- "percent"
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.bar <- barChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let pie series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8, expr9 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking
    Compiler.Compiler.Compile(
        <@ pieChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%expr9 @>,
        noReturn=true,
        shouldCompress=true)

[<ReflectedDefinition>]
let private scatterChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle (pointFormat:string option) subtitle (stacking:Stacking) =
    let options = createEmpty<HighchartsOptions>()
    setChartOptions "chart" "scatter" options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let barChart = createEmpty<HighchartsBarChart>()
    barChart.showInLegend <- legend
//    match stacking with
//    | Disabled -> ()
//    | Normal -> barChart.stacking <- "normal"
//    | Percent -> barChart.stacking <- "percent"
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.bar <- barChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    setSubtitle subtitle options
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let scatter series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8, expr9 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle stacking
    Compiler.Compiler.Compile(
        <@ scatterChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 %%expr9 @>,
        noReturn=true,
        shouldCompress=true)

//            let options = createEmpty<HighchartsOptions>()
//            // x axis options
//            setXAxisOptions series.[0] options categories xTitle
//            // y axis options
//            setYAxisOptions  series.[0] options yTitle
//            // title options
//            setTitleOptions chartTitle options
//            // series options
//            setSeriesOptions series options
//            let chartElement = Utils.jq "#chart"
//            chartElement.highcharts(options) |> ignore



















//
//    module Line =
//
//        [<ReflectedDefinition>]
//        let chart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle =
//            let options = createEmpty<HighchartsOptions>()
//            // chart options
//            setChartOptions "chart" "line" options
//            // x axis options
//            setXAxisOptions series.[0] options categories xTitle
//            // y axis options
//            setYAxisOptions  series.[0] options yTitle
//            // plot options
//            let lineChart = createEmpty<HighchartsLineChart>()
//            lineChart.showInLegend <- legend
//            let plotOptions = createEmpty<HighchartsPlotOptions>()
//            plotOptions.line <- lineChart
//            options.plotOptions <- plotOptions
//            // title options
//            setTitleOptions chartTitle options
//            // series options
//            setSeriesOptions series options
//            let chartElement = Utils.jq "#chart"
//            chartElement.highcharts(options) |> ignore
//
//        let js series chartTitle legend categories xTitle yTitle =
//            let expr1, expr2, expr3, expr4, expr5, expr6 = quoteArgs series chartTitle legend categories xTitle yTitle
//            Compiler.Compiler.Compile(
//                <@ chart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 @>,
//                noReturn=true,
//                shouldCompress=true)
//
//    module Pie =
//
//        [<ReflectedDefinition>]
//        let chart (series:Series []) chartTitle legend categories xTitle yTitle =
//            let options = createEmpty<HighchartsOptions>()
//            // chart options
//            setChartOptions "chart" "pie" options
//            // x axis options
//            setXAxisOptions series.[0] options categories xTitle
//            // y axis options
//            setYAxisOptions  series.[0] options yTitle
//            // plot options
//            let pieChart = createEmpty<HighchartsPieChart>()
//            pieChart.allowPointSelect <- true
//            pieChart.showInLegend <- legend
//            //pieChart.center
//            let plotOptions = createEmpty<HighchartsPlotOptions>()
//            plotOptions.pie <- pieChart
//            options.plotOptions <- plotOptions
//            // title options
//            setTitleOptions chartTitle options
//            // series options
//            setSeriesOptions series options
//            let chartElement = Utils.jq "#chart"
//            chartElement.highcharts(options) |> ignore
//
//        let js series chartTitle legend categories xTitle yTitle =
//            let expr1, expr2, expr3, expr4, expr5, expr6 = quoteArgs series chartTitle legend categories xTitle yTitle
//            Compiler.Compiler.Compile(
//                <@ chart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 @>,
//                noReturn=true,
//                shouldCompress=true)
//
//    module Scatter =
//
//        [<ReflectedDefinition>]
//        let chart (series:Series []) chartTitle legend categories xTitle yTitle =
//            let options = createEmpty<HighchartsOptions>()
//            // chart options
//            setChartOptions "chart" "scatter" options
//            // x axis options
//            setXAxisOptions series.[0] options categories xTitle
//            // y axis options
//            setYAxisOptions  series.[0] options yTitle
//            // plot options
//            let scatterChart = createEmpty<HighchartsScatterChart>()
//            scatterChart.allowPointSelect <- true
//            scatterChart.showInLegend <- legend
//            let plotOptions = createEmpty<HighchartsPlotOptions>()
//            plotOptions.scatter <- scatterChart
//            options.plotOptions <- plotOptions
//            // title options
//            setTitleOptions chartTitle options
//            // series options
//            setSeriesOptions series options
//            let chartElement = Utils.jq "#chart"
//            chartElement.highcharts(options) |> ignore
//
//        let js series chartTitle legend categories xTitle yTitle =
//            let expr1, expr2, expr3, expr4, expr5, expr6 = quoteArgs series chartTitle legend categories xTitle yTitle
//            Compiler.Compiler.Compile(
//                <@ chart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 @>,
//                noReturn=true,
//                shouldCompress=true)
//
//    module Combination =
//
//        [<ReflectedDefinition>]
//        let chart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle =
//            let options = createEmpty<HighchartsOptions>()
//            // x axis options
//            setXAxisOptions series.[0] options categories xTitle
//            // y axis options
//            setYAxisOptions  series.[0] options yTitle
//            // title options
//            setTitleOptions chartTitle options
//            // series options
//            setSeriesOptions series options
//            let chartElement = Utils.jq "#chart"
//            chartElement.highcharts(options) |> ignore
//
//        let js series chartTitle legend categories xTitle yTitle =
//            let expr1, expr2, expr3, expr4, expr5, expr6 = quoteArgs series chartTitle legend categories xTitle yTitle
//            Compiler.Compiler.Compile(
//                <@ chart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 @>,
//                noReturn=true,
//                shouldCompress=true)
