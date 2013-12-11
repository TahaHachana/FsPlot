module FsPlot.JS

#if INTERACTIVE
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#endif

open Microsoft.FSharp.Quotations
open System
open Expr

[<ReflectedDefinition>]
module internal Utils =

    let jq(selector:string) = Globals.Dollar.Invoke selector

module Highcharts =

    module Pie =
       
        [<ReflectedDefinition>]
        let chart (data:(string*#value) []) chartTitle legend =
            let options = createEmpty<HighchartsOptions>()
            let chartOpts = createEmpty<HighchartsChartOptions>()
            chartOpts.renderTo <- "chart"
            chartOpts._type <- "pie"
            options.chart <- chartOpts
//            let legendOptions = createEmpty<HighchartsLegendOptions>()
//            legendOptions.
//            legendOptions.enabled <- legend
//            options.legend <- legendOptions
            let highPie = createEmpty<HighchartsPieChart>()
            highPie.allowPointSelect <- true
            highPie.showInLegend <- legend
            let plotOptions = createEmpty<HighchartsPlotOptions>()
            plotOptions.pie <- highPie
            options.plotOptions <- plotOptions
            let titleOptions = createEmpty<HighchartsTitleOptions>()
            titleOptions.text <- defaultArg chartTitle ""
            options.title <- titleOptions
            let seriesOp = createEmpty<HighchartsSeriesOptions>()
            let data' = data |> Array.map (fun (k, v) -> box [|box k; box v|])
            seriesOp.data <- data'
            options.series <- [|seriesOp|]
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js data chartTitle legend =
            let dataExpr = quoteTupleSeq data
            let chartTitleExpr = quoteStrOption chartTitle
            let legendExpr = quoteBool legend
            FunScript.Compiler.Compiler.Compile(
                <@ chart %%dataExpr %%chartTitleExpr %%legendExpr @>,
                noReturn=true,
                shouldCompress=true)



//        let data = [|200|]
//        let test = Pie.Js(data, None)
//
//        let data' = [|"IE", 200; "Chrome", 253; "Firefox", 158|]
//        let test' = Pie.Js(data', None)



