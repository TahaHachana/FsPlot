module internal FsPlot.HighchartsJs

#if INTERACTIVE
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#endif

open System
open FunScript
open DataSeries
open Options
open Quote

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

    let setLegendOptions legend (options:HighchartsOptions) =
        let legendOptions = createEmpty<HighchartsLegendOptions>()
        legendOptions.enabled <- legend
        options.legend <- legendOptions

    let quoteArgs a b c d e f g h =
        quoteSeriesArr a,
        quoteStrOption b,
        quoteBool c,
        quoteStringArr d,
        quoteStrOption e,
        quoteStrOption f,
        quoteStrOption g,
        quoteStrOption h

module Inline =

    [<JSEmitInline("{0}.center = {1}")>]
    let pieCenter options arr : unit = failwith "never"

    [<JSEmitInline("{0}.size = {1}")>]
    let pieSize options size : unit = failwith "never"

    [<JSEmitInline("{0}.showInLegend = false")>]
    let disableLegend options : unit = failwith "never"

    [<JSEmitInline("{0}.dataLabels = {enabled: false}")>]
    let disableLabels options : unit = failwith "never"

    [<JSEmitInline("{0}.gridLineInterpolation = 'polygon'")>]
    let polygon options : unit = failwith "never"

    [<JSEmitInline("{0}.neckHeight = '0%'")>]
    let funnelNeck options : unit = failwith "never"

    [<JSEmitInline("{0}.pointPlacement = 'on'")>]
    let pointPlacement options : unit = failwith "never"

open Utils
open Inline

[<ReflectedDefinition>]
module Chart =

    let area (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle stacking inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "area" inverted options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        areaStacking stacking areaChart
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.area <- areaChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let areaspline (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle stacking inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "areaspline" inverted options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        setAreaMarker areaChart
        areaStacking stacking areaChart
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let arearange (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "arearange" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        let tooltipOptions = createEmpty<HighchartsTooltipOptions>()
        match pointFormat with
        | None -> ()
        | Some value -> tooltipOptions.pointFormat <- value
        tooltipOptions.crosshairs <- true
        options.tooltip <- tooltipOptions
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let bar (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bar" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let barChart = createEmpty<HighchartsBarChart>()
        match stacking with
        | Disabled -> ()
        | Normal -> barChart.stacking <- "normal"
        | Percent -> barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.bar <- barChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let bubble (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bubble" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions  options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        setTooltipOptions pointFormat options    
        setSeriesOptions series options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let column (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle stacking =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "column" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let barChart = createEmpty<HighchartsBarChart>()
        match stacking with
        | Disabled -> ()
        | Normal -> barChart.stacking <- "normal"
        | Percent -> barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.column <- barChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let combine (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle pieOptions =
        let options = createEmpty<HighchartsOptions>()
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        let seriesOptions =
            [|
                for x in series do
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
        setTooltipOptions pointFormat options            
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let donut (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "pie" options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let pieChart = createEmpty<HighchartsPieChart>()
        pieChart.showInLegend <- legend
        pieChart.innerSize <- "50%"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.pie <- pieChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let funnel (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "funnel" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        let seriesChart = createEmpty<HighchartsSeriesChart>()
        funnelNeck seriesChart
        plotOptions.series <- seriesChart
        options.plotOptions<- plotOptions
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let line (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "line" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let percentArea (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "area" inverted options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        areaChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.area <- areaChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let percentBar (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bar" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.bar <- barChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let percentColumn (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "column" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "percent"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.column <- barChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let pie (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "pie" options
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        let pieChart = createEmpty<HighchartsPieChart>()
        pieChart.showInLegend <- legend
        plotOptions.pie <- pieChart
        options.plotOptions <- plotOptions
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let radar (series:Series []) chartTitle legend (categories:string []) xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        let chartOptions = createEmpty<HighchartsChartOptions>()
        chartOptions.renderTo <- "chart"
        chartOptions._type <- "line"
        chartOptions.polar <- true
        options.chart <- chartOptions
        setLegendOptions legend options
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let xAxisType =
            match categories.Length with
            | 0 ->
                match series.[0].XType with
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
        axisOptions.lineWidth <- 0.
        axisOptions.tickmarkPlacement <- "on"
        options.xAxis <- axisOptions
        let yAxisOptions = createEmpty<HighchartsAxisOptions>()
        let yAxisTitle = createEmpty<HighchartsAxisTitle>()
        yAxisTitle.text <- defaultArg yTitle ""
        yAxisOptions.title <- yAxisTitle
        yAxisOptions.lineWidth <- 0.
        yAxisOptions.min <- 0.
        polygon yAxisOptions
        options.yAxis <- yAxisOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        let seriesOptions =
            [|
                for x in series do
                    let options = createEmpty<HighchartsSeriesOptions>()
                    options.data <- x.Values
                    options.name <- x.Name
                    pointPlacement options
                    setSeriesChartType x.Type options
                    yield options
            |]
        options.series <- seriesOptions
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let scatter (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "scatter" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let spline (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "spline" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let stackedArea (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle inverted =
        let options = createEmpty<HighchartsOptions>()
        areaChartOptions "chart" "area" inverted options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let areaChart = createEmpty<HighchartsAreaChart>()
        areaChart.stacking <- "normal"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.area <- areaChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let stackedBar (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "bar" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "normal"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.bar <- barChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

    let stackedColumn (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle =
        let options = createEmpty<HighchartsOptions>()
        setChartOptions "chart" "column" options
        setLegendOptions legend options
        setXAxisOptions series.[0].XType options categories xTitle
        setYAxisOptions options yTitle
        let barChart = createEmpty<HighchartsBarChart>()
        barChart.stacking <- "normal"
        let plotOptions = createEmpty<HighchartsPlotOptions>()
        plotOptions.column <- barChart
        options.plotOptions <- plotOptions
        setTitle chartTitle options
        setSubtitle subtitle options
        setSeriesOptions series options
        setTooltipOptions pointFormat options
        let chartElement = Utils.jq "#chart"
        chartElement.highcharts(options) |> ignore

let area a b c d e f g h i j =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    let e9 = quoteStacking i
    let e10 = quoteBool j
    Compiler.Compiler.Compile(
        <@ Chart.area %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 %%e10 @>,
        noReturn=true,
        shouldCompress=true)

let areaspline a b c d e f g h i j =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    let e9 = quoteStacking i
    let e10 = quoteBool j
    Compiler.Compiler.Compile(
        <@ Chart.areaspline %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 %%e10 @>,
        noReturn=true,
        shouldCompress=true)

let arearange a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.arearange %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let bar a b c d e f g h i =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    let e9 = quoteStacking i
    Compiler.Compiler.Compile(
        <@ Chart.bar %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 @>,
        noReturn=true,
        shouldCompress=true)

let bubble a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.bubble %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let column a b c d e f g h i =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    let e9 = quoteStacking i
    Compiler.Compiler.Compile(
        <@ Chart.column %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 @>,
        noReturn=true,
        shouldCompress=true)

let combine a b c d e f g h i =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    let e9 = quotePieOptions i
    Compiler.Compiler.Compile(
        <@ Chart.combine %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 @>,
        noReturn=true,
        shouldCompress=true)

let donut a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.donut %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let funnel a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.funnel %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let line a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.line %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let percentArea a b c d e f g h i =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    let e9 = quoteBool i
    Compiler.Compiler.Compile(
        <@ Chart.percentArea %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 @>,
        noReturn=true,
        shouldCompress=true)

let percentBar a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.percentBar %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let percentColumn a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.percentColumn %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let pie a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.pie %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let radar a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.radar %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let scatter a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.scatter %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let spline a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.spline %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let stackedArea a b c d e f g h i =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    let e9 = quoteBool i
    Compiler.Compiler.Compile(
        <@ Chart.stackedArea %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 @>,
        noReturn=true,
        shouldCompress=true)

let stackedBar a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.stackedBar %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)

let stackedColumn a b c d e f g h =
    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
    Compiler.Compiler.Compile(
        <@ Chart.stackedColumn %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 @>,
        noReturn=true,
        shouldCompress=true)
