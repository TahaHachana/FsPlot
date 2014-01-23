module internal FsPlot.Highcharts.Js

#if INTERACTIVE
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#endif

open FsPlot.Config
open FsPlot.Data
open FsPlot.Highcharts.Options
open FsPlot.Quote
open FunScript
open FunScript.Compiler
open System

module Inline =

    [<JSEmitInline("{0}.center = {1}")>]
    let pieCenter options arr : unit = failwith ""

    [<JSEmitInline("{0}.size = {1}")>]
    let pieSize options size : unit = failwith ""

    [<JSEmitInline("{0}.showInLegend = false")>]
    let disableLegend options : unit = failwith ""

    [<JSEmitInline("{0}.dataLabels = {enabled: false}")>]
    let disableLabels options : unit = failwith ""

    [<JSEmitInline("{0}.gridLineInterpolation = 'polygon'")>]
    let polygon options : unit = failwith ""

    [<JSEmitInline("{0}.neckHeight = '0%'")>]
    let funnelNeck options : unit = failwith ""

    [<JSEmitInline("{0}.pointPlacement = 'on'")>]
    let pointPlacement options : unit = failwith ""

    [<JSEmitInline "$('#chart').highcharts().series[0]">]
    let getSeries() : HighchartsSeriesObject = failwith ""

    [<JSEmitInline "{0}.addPoint({1}, true, {2})">]
    let addPoint series point shift : unit = failwith ""

open Inline

[<ReflectedDefinition>]
module Utils =

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

    let setXAxisOptions xAxis (options:HighchartsOptions) (categories:string []) xTitle =
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let xAxisType =
            match categories.Length with
            | 0 ->
                match xAxis with
                | Category -> "category"
                | DateTime -> "datetime"
                | Linear -> "linear"
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

    let setTitle chartTitle (options:HighchartsOptions) =
        let titleOptions = createEmpty<HighchartsTitleOptions>()
        titleOptions.text <- defaultArg chartTitle ""
        options.title <- titleOptions

    let setSeriesChartType chartType (options:HighchartsSeriesOptions) =
        let chartTypeStr = 
            match chartType with
            | Area | StackedArea | PercentArea -> "area"
            | Areaspline -> "areaspline"
            | Arearange -> "arearange"
            | Bar | StackedBar | PercentBar -> "bar"
            | Bubble -> "bubble"
            | Column | StackedColumn | PercentColumn -> "column"
            | Combination -> ""
            | Donut | Pie -> "pie"
            | Funnel -> "funnel"
            | Line | Radar -> "line"
            | Scatter -> "scatter"
            | Spline -> "spline"
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

    let setTooltipOptions tooltip (options:HighchartsOptions) =
        match tooltip with
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

    let setLegendOptions legend (options:HighchartsOptions) =
        let legendOptions = createEmpty<HighchartsLegendOptions>()
        legendOptions.enabled <- legend
        options.legend <- legendOptions

    let initSignalr address guid =
        let hub = Globals.Dollar.connection.hub
        hub.url <- (address + "/signalr")
        let proxy = hub.createHubProxy("dataHub")
        hub.start()._done(fun () ->
            let proxyGuid = proxy.connection.id
            proxy.invoke("storeGuids", guid, proxyGuid)
            ) |> ignore
        proxy

    let setDynamicChartOptions (proxy:HubProxy) shift renderTo chartType (options:HighchartsOptions) =
        let chartOptions = createEmpty<HighchartsChartOptions>()
        let events = createEmpty<HighchartsChartEvents>()
        events.load <- (fun _ ->
            let series = getSeries()
            proxy.on("push", (fun arr -> addPoint series arr shift)) |> ignore)
        chartOptions.renderTo <- renderTo
        chartOptions._type <- chartType
        chartOptions.events <- events
        options.chart <- chartOptions

open Utils

[<ReflectedDefinition>]
module Chart =
    
    let area config stacking inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "area" inverted options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        areaStacking stacking areaChart
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.area <- areaChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let areaspline config stacking inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "areaspline" inverted options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        setAreaMarker areaChart
        areaStacking stacking areaChart
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let arearange config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "arearange" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        let tooltipOptions = createEmpty<HighchartsTooltipOptions>()
        match config.Tooltip with
        | None -> ()
        | Some value -> tooltipOptions.pointFormat <- value
        tooltipOptions.crosshairs <- true
        options.tooltip <- tooltipOptions
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let bar config stacking =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bar" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        match stacking with
        | Disabled -> ()
        | Normal -> barChart.stacking <- "normal"
        | Percent -> barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.bar <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let bubble config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bubble" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setTooltipOptions config.Tooltip options    
        setSeriesOptions config.Data options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let column config stacking =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "column" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        match stacking with
        | Disabled -> ()
        | Normal -> barChart.stacking <- "normal"
        | Percent -> barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.column <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let combine config pieOptions =
        let options = createEmpty<HighchartsOptions>()
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        let seriesOptions =
            [|
                for x in config.Data do
                    let options = createEmpty<HighchartsSeriesOptions>()
                    options.data <- x.Values
                    options.name <- x.Name
                    match x.Type with
                    | Pie ->
                        match pieOptions with
                        | None -> ()
                        | Some value ->
                            pieCenter options value.Center
                            pieSize options value.Size
                            disableLegend options
                            disableLabels options
                    | _ -> ()    
                    setSeriesChartType x.Type options
                    yield options
            |]
        options.series <- seriesOptions
        setTooltipOptions config.Tooltip options            
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let donut config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "pie" options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let pieChart = createEmpty<HighchartsPieChart>()
        pieChart.showInLegend <- config.Legend
        pieChart.innerSize <- "50%"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.pie <- pieChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let funnel config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "funnel" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        let seriesChart = createEmpty<HighchartsSeriesChart>()
        funnelNeck seriesChart
        plotOptions.series <- seriesChart
        options.plotOptions<- plotOptions
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let line config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "line" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let percentArea config inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "area" inverted options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        areaChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.area <- areaChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let percentBar config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bar" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.bar <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let percentColumn config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "column" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.column <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let pie config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "pie" options
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        let pieChart = createEmpty<HighchartsPieChart>()
        pieChart.showInLegend <- config.Legend
        plotOptions.pie <- pieChart
        options.plotOptions <- plotOptions
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let radar config =
        let options = createEmpty<HighchartsOptions>()
        let chartOptions = createEmpty<HighchartsChartOptions>()
        chartOptions.renderTo <- "chart"
        chartOptions._type <- "line"
        chartOptions.polar <- true
        options.chart <- chartOptions
        setLegendOptions config.Legend options
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let xAxisType =
            let categories = config.Categories
            match categories.Length with
            | 0 ->
                match config.XAxis with
                | Category -> "category"
                | DateTime -> "datetime"
                | Linear -> "linear"
            | _ ->
                axisOptions.categories <- categories
                "category"
        axisOptions._type <- xAxisType
        let axisTitle = createEmpty<HighchartsAxisTitle>()
        axisTitle.text <- defaultArg config.XTitle ""
        axisOptions.title <- axisTitle
        axisOptions.lineWidth <- 0.
        axisOptions.tickmarkPlacement <- "on"
        options.xAxis <- axisOptions
        let yAxisOptions = createEmpty<HighchartsAxisOptions>()
        let yAxisTitle = createEmpty<HighchartsAxisTitle>()
        yAxisTitle.text <- defaultArg config.YTitle ""
        yAxisOptions.title <- yAxisTitle
        yAxisOptions.lineWidth <- 0.
        yAxisOptions.min <- 0.
        polygon yAxisOptions
        options.yAxis <- yAxisOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        let seriesOptions =
            [|
                for x in config.Data do
                    let options = createEmpty<HighchartsSeriesOptions>()
                    options.data <- x.Values
                    options.name <- x.Name
                    pointPlacement options
                    setSeriesChartType x.Type options
                    yield options
            |]
        options.series <- seriesOptions
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let scatter config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "scatter" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    // Line chart with smooth lines.
    let spline config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "spline" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let stackedArea config inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "area" inverted options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        areaChart.stacking <- "normal"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.area <- areaChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let stackedBar config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bar" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "normal"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.bar <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let stackedColumn config =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "column" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "normal"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.column <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

let inline compile expr =
    Compiler.Compile(
        expr,
        noReturn = true,
        shouldCompress = true)

let area config stacking inverted =
    let configExpr = quoteChartConfig config
    let stackingExpr = quoteStacking stacking
    let invertedExpr = quoteBool inverted
    compile <@ Chart.area %%configExpr %%stackingExpr %%invertedExpr @>

let areaspline config stacking inverted =
    let configExpr = quoteChartConfig config
    let stackingExpr = quoteStacking stacking
    let invertedExpr = quoteBool inverted
    compile <@ Chart.areaspline %%configExpr %%stackingExpr %%invertedExpr @>

let arearange config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.arearange %%configExpr @>

let bar config stacking =
    let configExpr = quoteChartConfig config
    let stackingExpr = quoteStacking stacking
    compile <@ Chart.bar %%configExpr %%stackingExpr @>

let bubble config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.bubble %%configExpr @>

let column config stacking =
    let configExpr = quoteChartConfig config
    let stackingExpr = quoteStacking stacking
    compile <@ Chart.column %%configExpr %%stackingExpr @>

let combine config pieOptions =
    let configExpr = quoteChartConfig config
    let pieOptionsExpr = quotePieOptions pieOptions
    compile <@ Chart.combine %%configExpr %%pieOptionsExpr @>

let donut config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.donut %%configExpr @>

let funnel config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.funnel %%configExpr @>

let line config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.line %%configExpr @>

let percentArea config inverted =
    let configExpr = quoteChartConfig config
    let invertedExpr = quoteBool inverted
    compile <@ Chart.percentArea %%configExpr %%invertedExpr @>
    
let percentBar config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.percentBar %%configExpr @>

let percentColumn config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.percentColumn %%configExpr @>

let pie config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.pie %%configExpr @>

let radar config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.radar %%configExpr @>

let scatter config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.scatter %%configExpr @>

let spline config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.spline %%configExpr @>

let stackedArea config inverted =
    let configExpr = quoteChartConfig config
    let invertedExpr = quoteBool inverted
    compile <@ Chart.stackedArea %%configExpr %%invertedExpr @>

let stackedBar config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.stackedBar %%configExpr @>

let stackedColumn config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.stackedColumn %%configExpr @>

[<ReflectedDefinition>]
module DynamicChart =
    
    let area address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "area" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.area <- areaChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options)

    let areaspline address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "areaspline" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        setAreaMarker areaChart
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let arearange address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "arearange" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        let tooltipOptions = createEmpty<HighchartsTooltipOptions>()
        match config.Tooltip with
        | None -> ()
        | Some value -> tooltipOptions.pointFormat <- value
        tooltipOptions.crosshairs <- true
        options.tooltip <- tooltipOptions
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let bar address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "bar" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.bar <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore
       
    let bubble address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "bubble" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setTooltipOptions config.Tooltip options    
        setSeriesOptions config.Data options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let column address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "column" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let barChart = createEmpty<HighchartsBarChart>()
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.column <- barChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let donut address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "pie" options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        let pieChart = createEmpty<HighchartsPieChart>()
        pieChart.showInLegend <- config.Legend
        pieChart.innerSize <- "50%"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.pie <- pieChart
        options.plotOptions <- plotOptions
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let funnel address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "funnel" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        let seriesChart = createEmpty<HighchartsSeriesChart>()
        funnelNeck seriesChart
        plotOptions.series <- seriesChart
        options.plotOptions<- plotOptions
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let line address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "line" options
        setLegendOptions config.Legend options
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let pie address guid shift config =
        let proxy = initSignalr address guid
        let options = createEmpty<HighchartsOptions>()
        setDynamicChartOptions proxy shift "chart" "pie" options
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        let pieChart = createEmpty<HighchartsPieChart>()
        pieChart.showInLegend <- config.Legend
        plotOptions.pie <- pieChart
        options.plotOptions <- plotOptions
        setXAxisOptions config.XAxis options config.Categories config.XTitle
        setYAxisOptions options config.YTitle
        setTitle config.Title options
        setSubtitle config.Subtitle options
        setSeriesOptions config.Data options
        setTooltipOptions config.Tooltip options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore
        
let dynamicArea address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.area address guid shift %%configExpr @>

let dynamicAreaspline address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.areaspline address guid shift %%configExpr @>

let dynamicArearange address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.arearange address guid shift %%configExpr @>

let dynamicBar address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.bar address guid shift %%configExpr @>

let dynamicBubble address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.bubble address guid shift %%configExpr @>

let dynamicColumn address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.column address guid shift %%configExpr @>

let dynamicDonut address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.donut address guid shift %%configExpr @>

let dynamicFunnel address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.funnel address guid shift %%configExpr @>

let dynamicLine address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.line address guid shift %%configExpr @>

let dynamicPie address guid shift config =
    let configExpr = quoteChartConfig config
    compile <@ DynamicChart.pie address guid shift %%configExpr @>
