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

    let setXAxisOptions xType (options:HighchartsOptions) categories xTitle =
        let axisOptions = createEmpty<HighchartsAxisOptions>()
        let xAxisType =
            match xType with
            | TypeCode.DateTime -> "datetime"
            | TypeCode.String -> "category"
            | _ -> "linear"
        axisOptions._type <- xAxisType
        axisOptions.categories <- categories
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

    let boxDataPoints xType (k:key, v:value) =
        match xType with
        | TypeCode.Empty -> box v
        | _ -> box [|box k; box v|]

    let setSeriesOptions (series:AreaSeries []) (options:HighchartsOptions) =
        let seriesOptions =
            [|
                for x in series do
                    let options = createEmpty<HighchartsSeriesOptions>()
//                    let dataPoints = Array.map (fun dp -> boxDataPoints x.XType dp) x.Values
                    options.data <- x.Values // dataPoints
                    options.name <- x.Name
                    setSeriesChartType x.Type options
                    yield options
            |]
        options.series <- seriesOptions

module Highcharts =

    open Utils
    open Microsoft.FSharp.Quotations

    let private quoteArgs (exprFun:'a -> Expr) series chartTitle legend categories xTitle yTitle =
        let seriesExpr = exprFun series
        let chartTitleExpr = quoteStrOption chartTitle
        let legendExpr = quoteBool legend
        let categoriesExpr = quoteStringArr categories
        let xTitleExpr = quoteStrOption xTitle
        let yTitleExpr = quoteStrOption yTitle
        seriesExpr, chartTitleExpr, legendExpr, categoriesExpr, xTitleExpr, yTitleExpr

    module Area =

        [<ReflectedDefinition>]
        let chart (series:AreaSeries []) chartTitle (legend:bool) categories xTitle yTitle =
            let options = createEmpty<HighchartsOptions>()
            // chart options
            setChartOptions "chart" "area" options
            // x axis options
            setXAxisOptions series.[0].XType options categories xTitle
            // y axis options
            setYAxisOptions options yTitle
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

        let js series chartTitle legend categories xTitle yTitle =
            let expr1, expr2, expr3, expr4, expr5, expr6 = quoteArgs quoteAreaSeriesArr series chartTitle legend categories xTitle yTitle
            Compiler.Compiler.Compile(
                <@ chart %%expr1 %%expr2 %%expr3 %%expr4 %%expr5 %%expr6 @>,
                noReturn=true,
                shouldCompress=true)

//    module Bar =
//
//        [<ReflectedDefinition>]
//        let chart (series:Series []) chartTitle (legend:bool) categories xTitle yTitle =
//            let options = createEmpty<HighchartsOptions>()
//            // chart options
//            setChartOptions "chart" "bar" options
//            // x axis options
//            setXAxisOptions series.[0] options categories xTitle
//            // y axis options
//            setYAxisOptions  series.[0] options yTitle
//            // plot options
//            let barChart = createEmpty<HighchartsBarChart>()
//            barChart.showInLegend <- legend
//            let plotOptions = createEmpty<HighchartsPlotOptions>()
//            plotOptions.bar <- barChart
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
