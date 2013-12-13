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

module Highcharts =

    module Pie =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle legend =
            let data = series.[0]
            let ho = createEmpty<HighchartsOptions>()
            // chart options
            let chartOptions = createEmpty<HighchartsChartOptions>()
            chartOptions.renderTo <- "chart"
            chartOptions._type <- "pie"
            ho.chart <- chartOptions
            // x axis options
            let axisOptions = createEmpty<HighchartsAxisOptions>()
            let xAxisType = match data.XType with System.TypeCode.DateTime -> "datetime" | System.TypeCode.String -> "category" | _ -> "linear"
            axisOptions._type <- xAxisType
            ho.xAxis <- axisOptions            
            // plot options
            let pieChart = createEmpty<HighchartsPieChart>()
            pieChart.allowPointSelect <- true
            pieChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.pie <- pieChart
            ho.plotOptions <- plotOptions
            // title options
            let titleOptions = createEmpty<HighchartsTitleOptions>()
            titleOptions.text <- defaultArg chartTitle ""
            ho.title <- titleOptions
            // series options
            let seriesOptions =
                [|
                    for s in series do
                        let options = createEmpty<HighchartsSeriesOptions>()
                        let data =
                            Array.map (fun (k, v) ->
                                match data.XType with
                                | System.TypeCode.DateTime ->
//                                    Globals.Number.Create
                                    let d = Globals.parseFloat (k.ToString())
                                    
                                    box [|box d; box v|]
                                | _ -> box [|box k; box v|]) s.Values
                        options.data <- data
                        options.name <- s.Name
                        let chartType =  match s.Type with Area -> "area" | Pie -> "pie"
                        options._type <- chartType
                        yield options
                |]
            ho.series <- seriesOptions
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(ho) |> ignore

        let js (series:Series []) chartTitle legend =
            let seriesExpr = quoteDataSeriesArr series
            let chartTitleExpr = quoteStrOption chartTitle
            let legendExpr = quoteBool legend
            Compiler.Compiler.Compile(
                <@ chart %%seriesExpr %%chartTitleExpr %%legendExpr @>,
                noReturn=true,
                shouldCompress=true)

    module Area =

        [<ReflectedDefinition>]
        let chart (series:Series []) chartTitle (legend:bool) =
            let data = series.[0]
            let ho = createEmpty<HighchartsOptions>()
            // chart options
            let chartOptions = createEmpty<HighchartsChartOptions>()
            chartOptions.renderTo <- "chart"
            chartOptions._type <- "area"
            ho.chart <- chartOptions
            // x axis
            let axisOptions = createEmpty<HighchartsAxisOptions>()
            let xAxisType = match data.XType with System.TypeCode.DateTime -> "datetime" | System.TypeCode.String -> "category" |  _ -> "linear"
            axisOptions._type <- xAxisType
            ho.xAxis <- axisOptions            
            // plot options
            let areaChart = createEmpty<HighchartsAreaChart>()
            areaChart.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.area <- areaChart
            ho.plotOptions <- plotOptions
            // title options
            let titleOptions = createEmpty<HighchartsTitleOptions>()
            titleOptions.text <- defaultArg chartTitle ""
            ho.title <- titleOptions
            // series options
            let seriesOptions =
                [|
                    for s in series do
                        let options = createEmpty<HighchartsSeriesOptions>()
                        let data =
                            Array.map (fun (k, v) ->
                                match data.XType with
                                | System.TypeCode.DateTime ->
//                                    Globals.Number.Create
                                    let d = Globals.parseFloat (k.ToString())
                                    
                                    box [|box d; box v|]
                                | _ -> box [|box k; box v|]) s.Values
                        options.data <- data
                        options.name <- s.Name
                        let chartType =  match s.Type with Area -> "area" | Pie -> "pie"
                        options._type <- chartType
                        yield options
                |]
            ho.series <- seriesOptions
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(ho) |> ignore

        let js (series:Series []) chartTitle legend =
            let seriesExpr = quoteDataSeriesArr series
            let chartTitleExpr = quoteStrOption chartTitle
            let legendExpr = quoteBool legend
            Compiler.Compiler.Compile(
                <@ chart %%seriesExpr %%chartTitleExpr %%legendExpr @>,
                noReturn=true,
                shouldCompress=true)

