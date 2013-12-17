module FsPlot.HighchartsJs

#if INTERACTIVE
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#endif

open System
open DataSeries
open Expr
open FunScript

[<ReflectedDefinition>]
module internal Utils =

    let jq(selector:string) = Globals.Dollar.Invoke selector

    let setChartOptions renderTo chartType (options:HighchartsOptions) =
        let chartOptions = createEmpty<HighchartsChartOptions>()
        chartOptions.renderTo <- renderTo
        chartOptions._type <- chartType
        options.chart <- chartOptions

    let setXAxisOptions xType (options:HighchartsOptions) categories xTitle =
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let xAxisType =
            match xType with
            | TypeCode.DateTime -> "datetime"
            | TypeCode.String ->
                axisOptions.categories <- categories
                "category"
            | _ -> "linear"
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

//module Highcharts =

open Utils
open Microsoft.FSharp.Quotations

let private quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle =
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
let private areaChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle pointFormat subtitle =
    let options = createEmpty<HighchartsOptions>()
    setChartOptions "chart" "area" options
    setXAxisOptions series.[0].XType options categories xTitle
    setYAxisOptions options yTitle
    let areaChart = createEmpty<HighchartsAreaChart>()
    areaChart.showInLegend <- legend
    let marker = createEmpty<HighchartsMarker>()
    marker.enabled <- false
    marker.radius <- 2.
    let state = createEmpty<HighchartsMarkerState>()
    state.enabled <- true
    let states = createEmpty<AnonymousType1905>()
    states.hover <- state
    marker.states <- states
    areaChart.marker <- marker
    let plotOptions = createEmpty<HighchartsPlotOptions>()
    plotOptions.area <- areaChart
    options.plotOptions <- plotOptions
    setTitleOptions chartTitle options
    match subtitle with
    | None -> ()
    | Some value ->
        let subtitleOptions = createEmpty<HighchartsSubtitleOptions>()
        subtitleOptions.text <- value
        options.subtitle <- subtitleOptions
    setSeriesOptions series options
    setTooltipOptions pointFormat options
    let chartElement = Utils.jq "#chart"
    chartElement.highcharts(options) |> ignore

let area series chartTitle legend categories xTitle yTitle pointFormat subtitle =
    let expr1, expr2, expr3, expr4, expr5, expr6, expr7, expr8 =
        quoteArgs series chartTitle legend categories xTitle yTitle pointFormat subtitle
    Compiler.Compiler.Compile(
        <@ areaChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 %%expr7 %%expr8 @>,
        noReturn=true,
        shouldCompress=true)

//[<ReflectedDefinition>]
//let private barChart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle =
//    let options = createEmpty<HighchartsOptions>()
//    setChartOptions "chart" "bar" options
//    setXAxisOptions series.[0].XType options categories xTitle
//    setYAxisOptions options yTitle
//    let barChart = createEmpty<HighchartsBarChart>()
//    barChart.showInLegend <- legend
//    let plotOptions = createEmpty<HighchartsPlotOptions>()
//    plotOptions.bar <- barChart
//    options.plotOptions <- plotOptions
//    setTitleOptions chartTitle options
//    setSeriesOptions series options
//    let chartElement = Utils.jq "#chart"
//    chartElement.highcharts(options) |> ignore
//
//let bar series chartTitle legend categories xTitle yTitle =
//    let expr1, expr2, expr3, expr4, expr5, expr6 =
//        quoteArgs series chartTitle legend categories xTitle yTitle
//    Compiler.Compiler.Compile(
//        <@ barChart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 @>,
//        noReturn=true,
//        shouldCompress=true)

//    module Bubble =
//
//        [<ReflectedDefinition>]
//        let chart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle =
//            let options = createEmpty<HighchartsOptions>()
//            // chart options
//            setChartOptions "chart" "bubble" options
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
//
//    module Column =
//
//        [<ReflectedDefinition>]
//        let chart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle =
//            let options = createEmpty<HighchartsOptions>()
//            // chart options
//            setChartOptions "chart" "column" options
//            // x axis options
//            setXAxisOptions series.[0] options categories xTitle
//            // y axis options
//            setYAxisOptions  series.[0] options yTitle
//            // plot options
//            let columnChart = createEmpty<HighchartsBarChart>()
//            columnChart.showInLegend <- legend
//            let plotOptions = createEmpty<HighchartsPlotOptions>()
//            plotOptions.column <- columnChart
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
