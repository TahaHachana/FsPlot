module FsPlot.JS

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

    let setXAxisOptions (series:Series) (options:HighchartsOptions) =
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let xAxisType =
            match series.XType with
            | TypeCode.DateTime -> "datetime"
            | TypeCode.String -> "category"
            | _ -> "linear"
        axisOptions._type <- xAxisType
        options.xAxis <- axisOptions

    let setTitleOptions chartTitle (options:HighchartsOptions) =
        let titleOptions = createEmpty<HighchartsTitleOptions>()
        titleOptions.text <- defaultArg chartTitle ""
        options.title <- titleOptions

    let setSeriesChartType (series:Series) (options:HighchartsSeriesOptions) =
        let chartType = 
            match series.Type with
            | Area -> "area"
            | Bar -> "bar"
            | Column -> "column"
            | Line -> "line"
            | Pie -> "pie"
            | Scatter -> "scatter"
        options._type <- chartType

    let boxDataPoints (k:key, v:value) = box [|box k; box v|]

    let setSeriesOptions (series:Series []) (options:HighchartsOptions) =
        let seriesOptions =
            [|
                for x in series do
                    let options = createEmpty<HighchartsSeriesOptions>()
                    let dataPoints = Array.map (fun dp -> boxDataPoints dp) x.Values
                    options.data <- dataPoints
                    options.name <- x.Name
                    setSeriesChartType x options
                    yield options
            |]
        options.series <- seriesOptions

module Highcharts =

    open Utils

    let private quoteArgs (series:Series []) chartTitle legend =
        let seriesExpr = quoteDataSeriesArr series
        let chartTitleExpr = quoteStrOption chartTitle
        let legendExpr = quoteBool legend
        seriesExpr, chartTitleExpr, legendExpr    

    module Area =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle (legend:bool) =
            let options = createEmpty<HighchartsOptions>()
            // chart options
            setChartOptions "chart" "area" options
            // x axis options
            setXAxisOptions series.[0] options        
            // plot options
            let areaChart = createEmpty<HighchartsAreaChart>()
            areaChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.area <- areaChart
            options.plotOptions <- plotOptions
            // title options
            setTitleOptions chartTitle options
            // series options
            setSeriesOptions series options
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js series chartTitle legend =
            let expr, expr', expr'' = quoteArgs series chartTitle legend
            Compiler.Compiler.Compile(
                <@ chart %%expr %%expr' %%expr'' @>,
                noReturn=true,
                shouldCompress=true)

    module Bar =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle (legend:bool) =
            let options = createEmpty<HighchartsOptions>()
            // chart options
            setChartOptions "chart" "bar" options
            // x axis options
            setXAxisOptions series.[0] options        
            // plot options
            let barChart = createEmpty<HighchartsBarChart>()
            barChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.bar <- barChart
            options.plotOptions <- plotOptions
            // title options
            setTitleOptions chartTitle options
            // series options
            setSeriesOptions series options
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js series chartTitle legend =
            let expr, expr', expr'' = quoteArgs series chartTitle legend
            Compiler.Compiler.Compile(
                <@ chart %%expr %%expr' %%expr'' @>,
                noReturn=true,
                shouldCompress=true)

    module Column =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle (legend:bool) =
            let options = createEmpty<HighchartsOptions>()
            // chart options
            setChartOptions "chart" "column" options
            // x axis options
            setXAxisOptions series.[0] options        
            // plot options
            let columnChart = createEmpty<HighchartsBarChart>()
            columnChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.column <- columnChart
            options.plotOptions <- plotOptions
            // title options
            setTitleOptions chartTitle options
            // series options
            setSeriesOptions series options
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js series chartTitle legend =
            let expr, expr', expr'' = quoteArgs series chartTitle legend
            Compiler.Compiler.Compile(
                <@ chart %%expr %%expr' %%expr'' @>,
                noReturn=true,
                shouldCompress=true)

    module Line =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle (legend:bool) =
            let options = createEmpty<HighchartsOptions>()
            // chart options
            setChartOptions "chart" "line" options
            // x axis options
            setXAxisOptions series.[0] options        
            // plot options
            let lineChart = createEmpty<HighchartsLineChart>()
            lineChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.line <- lineChart
            options.plotOptions <- plotOptions
            // title options
            setTitleOptions chartTitle options
            // series options
            setSeriesOptions series options
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js series chartTitle legend =
            let expr, expr', expr'' = quoteArgs series chartTitle legend
            Compiler.Compiler.Compile(
                <@ chart %%expr %%expr' %%expr'' @>,
                noReturn=true,
                shouldCompress=true)

    module Pie =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle legend =
            let options = createEmpty<HighchartsOptions>()
            // chart options
            setChartOptions "chart" "pie" options
            // x axis options
            setXAxisOptions series.[0] options
            // plot options
            let pieChart = createEmpty<HighchartsPieChart>()
            pieChart.allowPointSelect <- true
            pieChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.pie <- pieChart
            options.plotOptions <- plotOptions
            // title options
            setTitleOptions chartTitle options
            // series options
            setSeriesOptions series options
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js series chartTitle legend =
            let expr, expr', expr'' = quoteArgs series chartTitle legend
            Compiler.Compiler.Compile(
                <@ chart %%expr %%expr' %%expr'' @>,
                noReturn=true,
                shouldCompress=true)

    module Scatter =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle legend =
            let options = createEmpty<HighchartsOptions>()
            // chart options
            let chartOptions = createEmpty<HighchartsChartOptions>()
            chartOptions.renderTo <- "chart"
            chartOptions._type <- "scatter"
            chartOptions.zoomType <- "xy"
            options.chart <- chartOptions

//            setChartOptions "chart" "scatter" options
            // x axis options
            setXAxisOptions series.[0] options
            // plot options
            let scatterChart = createEmpty<HighchartsScatterChart>()
            scatterChart.allowPointSelect <- true
            scatterChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.scatter <- scatterChart
            options.plotOptions <- plotOptions
            // title options
            setTitleOptions chartTitle options
            // series options
            setSeriesOptions series options
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js series chartTitle legend =
            let expr, expr', expr'' = quoteArgs series chartTitle legend
            Compiler.Compiler.Compile(
                <@ chart %%expr %%expr' %%expr'' @>,
                noReturn=true,
                shouldCompress=true)
