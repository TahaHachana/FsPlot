module FsPlot.JS

#if INTERACTIVE
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#endif

open Microsoft.FSharp.Quotations

[<ReflectedDefinition>]
module internal Utils =

    let jq(selector:string) = Globals.Dollar.Invoke selector

// data, window title, chart title, slices labels, color, XTitle, YTitle

module Highcharts =

    module Pie =
        
        [<ReflectedDefinitionAttribute>]
        let chart (data:(string*int) []) chartTitle =
            let options = createEmpty<HighchartsOptions>()
            let chartOpts = createEmpty<HighchartsChartOptions>()
            chartOpts.renderTo <- "chart"
            chartOpts._type <- "pie"
            options.chart <- chartOpts        
            let titleOptions = createEmpty<HighchartsTitleOptions>()
            titleOptions.text <- defaultArg chartTitle ""
            options.title <- titleOptions
            let seriesOp = createEmpty<HighchartsSeriesOptions>()
            let data' = data |> Array.map (fun (k, v) -> box [|box k; box v|])
            seriesOp.data <- data'
            options.series <- [|seriesOp|]
            let chartElement = Utils.jq "#chart"
            chartElement.highcharts(options) |> ignore

        let js (data:seq<string*int>) (chartTitle:string option) =
            let dataExpr =
                Expr.NewArray(
                    typeof<System.Tuple<System.String,System.Int32>>,
                    [
                        for (x, y) in data do
                            yield Expr.NewTuple( [ Expr.Value(x); Expr.Value(y)])
                    ])
            let chartTitleExpr =
                let infos = Reflection.FSharpType.GetUnionCases(typeof<string option>)
                match chartTitle with
                | None -> Expr.NewUnionCase(infos.[0], [])
                | Some value -> Expr.NewUnionCase(infos.[1], [Expr.Value value])

            FunScript.Compiler.Compiler.Compile(<@ chart %%dataExpr %%chartTitleExpr @>, noReturn=true, shouldCompress=true)

//        let data = [|"IE", 200; "Chrome", 253; "Firefox", 158|]
//    
//        let test = js data None
