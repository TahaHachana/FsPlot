module FsPlot.Google.Js

open FsPlot.Config
open FsPlot.Data
open FsPlot.Highcharts.Options
open FsPlot.Quote
open FunScript
open FunScript.Compiler
open FunScript.TypeScript
open System

[<JSEmitInline("google.visualization.data.join({0}, {1}, 'full', [[0,0]], [1], [1])")>]
let join dt1 dt2 : google.visualization.DataTable = failwith ""

[<ReflectedDefinition>]
module Chart =
    
    type pack = {packages : string []}

    let _type typeCode =
        match typeCode with
        | TypeCode.Boolean -> "boolean"
        | TypeCode.DateTime -> "datetime"
        | TypeCode.String -> "string"
        | _ -> "number"

    let columnLabel config idx series =
        let categories = config.Categories
        match Array.isEmpty categories with
        | false -> categories.[idx]
        | true -> series.Name

    let dataTables (config:ChartConfig) =
        config.Data
        |> Array.mapi(fun idx series ->
            let dataTable = google.visualization.DataTable.Create()        
            dataTable.addColumn(_type series.XType) |> ignore
            let label = columnLabel config idx series
            dataTable.addColumn(_type series.YType, label) |> ignore
            dataTable.addRows series.Values |> ignore  
            dataTable   
        )
        |> Array.toList

    let rec joinDataTables dts =
        match dts with
        | [dt] -> dt
        | [dt1; dt2] -> join dt1 dt2
        | x ->
            dts
            |> Seq.skip 2
            |> Seq.toList
            |> List.append [join x.[0] x.[1]]
            |> joinDataTables
//            let dt = join x.[0] x.[1]
//            let dts' = Seq.skip 2 dts |> Seq.toList
//            joinDataTables <| List.append [dt] dts'

    let drawOnLoad (drawChart:unit -> unit) =
        google.Globals.load("visualization", "1", {packages = [|"corechart"|]})
        google.Globals.setOnLoadCallback drawChart

    let barChartOptions config =
        let options = createEmpty<google.visualization.BarChartOptions>()
            
        match config.Title with
        | None -> ()
        | Some x -> options.title <- x

        match config.XTitle with
        | None -> ()
        | Some x ->
            let xAxis = createEmpty<google.visualization.ChartAxis>()
            xAxis.title <- x
            options.hAxis <- xAxis 

        match config.YTitle with
        | None -> ()
        | Some x ->
            let yAxis = createEmpty<google.visualization.ChartAxis>()
            yAxis.title <- x
            options.vAxis <- yAxis 

        let legend = createEmpty<google.visualization.ChartLegend>()
        match config.Legend with
        | false -> legend.position <- "none"
        | true -> legend.position <- "bottom"
        options.legend <- legend

        options

    let bar (config:ChartConfig) =
        let drawChart() =
            let options = barChartOptions config

            let data =
                dataTables config
                |> joinDataTables

            let chart = google.visualization.BarChart.Create(Globals.document.getElementById("chart"))
            chart.draw(data, options)

        drawOnLoad drawChart

    let column (config:ChartConfig) =
        let drawChart() =
            let options = createEmpty<google.visualization.ColumnChartOptions>()
            
            match config.Title with
            | None -> ()
            | Some x -> options.title <- x

            match config.XTitle with
            | None -> ()
            | Some x ->
                let xAxis = createEmpty<google.visualization.ChartAxis>()
                xAxis.title <- x
                options.hAxis <- xAxis 

            match config.YTitle with
            | None -> ()
            | Some x ->
                let yAxis = createEmpty<google.visualization.ChartAxis>()
                yAxis.title <- x
                options.vAxis <- yAxis 

            let legend = createEmpty<google.visualization.ChartLegend>()
            match config.Legend with
            | false -> legend.position <- "none"
            | true -> legend.position <- "bottom"
            options.legend <- legend

            let data =
                dataTables config
                |> joinDataTables

            let chart = google.visualization.ColumnChart.Create(Globals.document.getElementById("chart"))
            chart.draw(data, options)

        drawOnLoad drawChart

    let line (config:ChartConfig) =
        let drawChart() =
            let options = createEmpty<google.visualization.LineChartOptions>()
            
            match config.Title with
            | None -> ()
            | Some x -> options.title <- x

            match config.XTitle with
            | None -> ()
            | Some x ->
                let xAxis = createEmpty<google.visualization.ChartAxis>()
                xAxis.title <- x
                options.hAxis <- xAxis 

            match config.YTitle with
            | None -> ()
            | Some x ->
                let yAxis = createEmpty<google.visualization.ChartAxis>()
                yAxis.title <- x
                options.vAxis <- yAxis 

            let legend = createEmpty<google.visualization.ChartLegend>()
            match config.Legend with
            | false -> legend.position <- "none"
            | true -> legend.position <- "bottom"
            options.legend <- legend

            let data =
                dataTables config
                |> joinDataTables

            let chart = google.visualization.LineChart.Create(Globals.document.getElementById("chart"))
            chart.draw(data, options)

        drawOnLoad drawChart

    let stackedBar (config:ChartConfig) =
        let drawChart() =
            let options = barChartOptions config

            options.isStacked <- true

            let data =
                dataTables config
                |> joinDataTables

            let chart = google.visualization.BarChart.Create(Globals.document.getElementById("chart"))
            chart.draw(data, options)

        drawOnLoad drawChart

let inline compile expr =
    Compiler.Compile(
        expr,
        noReturn = true,
        shouldCompress = true)

let bar config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.bar %%configExpr @>

let column config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.column %%configExpr @>

let line config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.line %%configExpr @>

let stackedBar config =
    let configExpr = quoteChartConfig config
    compile <@ Chart.stackedBar %%configExpr @>
