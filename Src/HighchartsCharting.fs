module FsPlot.Highcharts.Charting

open FsPlot
open FsPlot.Config
open FsPlot.Data
open System
open System.IO

type HighchartsChart() as chart =
    
    [<DefaultValue>] val mutable private config : ChartConfig    

    let mutable jsFun = Js.highcharts
    let htmlFun = Html.highcharts

    let guid = Guid.NewGuid().ToString()
    let htmlFile = Path.GetTempPath() + guid + ".html"
    do File.WriteAllText(htmlFile, "")
    let browser = Browser.start htmlFile

    let agent =
        MailboxProcessor<ChartConfig>.Start(fun inbox ->
            let rec loop() =
                async {
                    let! msg = inbox.Receive()
                    match inbox.CurrentQueueLength with
                    | 0 ->
                        let js = jsFun msg
                        match inbox.CurrentQueueLength with
                        | 0 ->
                            let html = htmlFun msg.Type js
                            match inbox.CurrentQueueLength with
                            | 0 ->
                                System.IO.File.WriteAllText(htmlFile, html)
                                browser.Navigate().Refresh()
                                return! loop()
                            | _ -> return! loop()
                        | _ -> return! loop()
                    | _ -> return! loop()
                }
            loop())

    static member internal Create x (f:unit -> #HighchartsChart) =
        let gc = f()
        gc.config <- x
        gc.Refresh()
        gc

    member internal __.Refresh() = agent.Post chart.config

    /// <summary>Set the chart's title.</summary>
    member __.WithTitle title =
        chart.config <-
            {
                chart.config with
                    Title = Some title
            }
        chart.Refresh()

    /// <summary>Set the chart's data set name.</summary>
    member __.WithName name =
        chart.config <-
            {
                chart.config with
                    Data = [|chart.config.Data.[0] |> Series.WithName name |]
            }
        chart.Refresh()

    /// <summary>Set the chart's data sets names.</summary>
    member __.WithNames names =
        let series =
            Seq.zip names chart.config.Data
            |> Seq.map (fun (name, dataset) -> Series.WithName name dataset)
            |> Seq.toArray
        chart.config <-
            {
                chart.config with
                    Data = series
            }
        chart.Refresh()

    /// <summary>Set the chart's data points labels.</summary>
    member __.WithLabels labels =
        chart.config <-
            {
                chart.config with
                    Categories = Seq.toArray labels
            }
        chart.Refresh()

    /// <summary>Set the chart's X-axis title.</summary>
    member __.WithXTitle xTitle =
        chart.config <-
            {
                chart.config with
                    XTitle = Some xTitle
            }
        chart.Refresh()

    /// <summary>Set the chart's Y-axis title.</summary>
    member __.WithYTitle yTitle =
        chart.config <-
            {
                chart.config with
                    YTitle = Some yTitle
            }
        chart.Refresh()

    /// <summary>Display/hide the legend of the chart.</summary>
    member __.WithLegend enabled =
        chart.config <-
            {
                chart.config with
                    Legend = enabled
            }
        chart.Refresh()

    /// <summary>Close the chart's window.</summary>
    member __.Close() = //wnd.Close()
        browser.Quit()
        File.Delete htmlFile

    /// Save the chart as a PNG image.
    member __.SavePng(fileName) =
        browser
            .GetScreenshot()
            .SaveAsFile(fileName, Drawing.Imaging.ImageFormat.Png)
            
type AreaChart() =
    inherit HighchartsChart()

//    let mutable stacking = Disabled
//    let mutable inverted = false
//
//    let compileJs (config:ChartConfig) = Js.area config stacking inverted
//
//    do base.SetJsFun compileJs
//
//    member __.SetStacking(x) =
//        stacking <- x
//        base.Navigate()
//
//    member __.SetInverted(x) =
//        inverted <- x
//        base.Navigate()


type ArearangeChart() =
    inherit HighchartsChart()

type AreasplineChart() =
    inherit HighchartsChart()

//    let mutable stacking = Disabled
//    let mutable inverted = false
//
//    let compileJs (config:ChartConfig) = Js.areaspline config stacking inverted
//
//    do base.SetJsFun compileJs
//
//    member __.SetStacking(x) =
//        stacking <- x
//        base.Navigate()
//
//    member __.SetInverted(x) =
//        inverted <- x
//        base.Navigate()

type BarChart() =
    inherit HighchartsChart()

//    let mutable stacking = Disabled
//
//    let compileJs (config:ChartConfig) = Js.bar config stacking
//
//    do base.SetJsFun compileJs
//
//    member __.SetStacking(x) =
//        stacking <- x
//        base.Navigate()

type BubbleChart() =
    inherit HighchartsChart()

type ColumnChart() =
    inherit HighchartsChart()

//    let mutable stacking = Disabled
//
//    let compileJs (config:ChartConfig) = Js.column config stacking
//
//    do base.SetJsFun compileJs
//
//    member __.SetStacking(x) =
//        stacking <- x
//        base.Navigate()

type CombinationChart() =
    inherit HighchartsChart()

type DonutChart() =
    inherit HighchartsChart()

type FunnelChart() =
    inherit HighchartsChart()

type LineChart() =
    inherit HighchartsChart()

type PercentAreaChart() =
    inherit HighchartsChart()

//    let mutable inverted = false
//
//    let compileJs (config:ChartConfig) = Js.percentArea config inverted
//
//    do base.SetJsFun compileJs
//
//    member __.SetInverted(x) =
//        inverted <- x
//        base.Navigate()

type PercentBarChart() =
    inherit HighchartsChart()

type PercentColumnChart() =
    inherit HighchartsChart()

type PieChart() =
    inherit HighchartsChart()

type RadarChart() =
    inherit HighchartsChart()

type ScatterChart() =
    inherit HighchartsChart()

type SplineChart() =
    inherit HighchartsChart()

type StackedAreaChart() =
    inherit HighchartsChart()

//    let mutable inverted = false
//
//    let compileJs (config:ChartConfig) = Js.stackedArea config inverted
//
//    do base.SetJsFun compileJs
//
//    member __.SetInverted(x) =
//        inverted <- x
//        base.Navigate()

type StackedBarChart() =
    inherit HighchartsChart()

type StackedColumnChart() =
    inherit HighchartsChart()

// TODO: refactor members body
// type ChartMaker =
//     static member New...

type Chart =
        
    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Area(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Area(data, defaultArg Name "")|] None None Title None Area XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreaChart())

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Area(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Area(data, defaultArg Name "")|] None None Title None Area XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreaChart())

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Area(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Area
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Area(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Area XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreaChart())

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Area(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Area
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Area(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Area XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreaChart())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#key*#value*#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Arearange(data, defaultArg Name "")|] None None Title None Arearange XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ArearangeChart())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#value*#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Arearange(data, defaultArg Name "")|] None None Title None Arearange XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ArearangeChart())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#seq<'K * 'V * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Arearange
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Arearange(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Arearange XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ArearangeChart())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#seq<'V * 'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Arearange
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Arearange(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Arearange XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ArearangeChart())

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Areaspline(data, defaultArg Name "")|] None None Title None Areaspline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreasplineChart())

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Areaspline(data, defaultArg Name "")|] None None Title None Areaspline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreasplineChart())

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Areaspline
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Areaspline(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Areaspline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreasplineChart())

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Areaspline
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Areaspline(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Areaspline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> AreasplineChart())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bar(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Bar(data, defaultArg Name "")|] None None Title None Bar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BarChart())

    /// <summary>Creates an bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bar(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Bar(data, defaultArg Name "")|] None None Title None Bar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BarChart())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bar(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Bar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Bar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Bar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BarChart())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bar(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Bar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Bar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Bar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BarChart())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#key*#value*#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Bubble(data, defaultArg Name "")|] None None Title None Bubble XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BubbleChart())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#value*#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Bubble(data, defaultArg Name "")|] None None Title None Bubble XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BubbleChart())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#seq<'K * 'V * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Bubble
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Bubble(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Bubble XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BubbleChart())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#seq<'V * 'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Bubble
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Bubble(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Bubble XTitle YTitle
        HighchartsChart.Create chartData (fun () -> BubbleChart())

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Column(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Column(data, defaultArg Name "")|] None None Title None Column XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ColumnChart())

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Column(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Column(data, defaultArg Name "")|] None None Title None Column XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ColumnChart())

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Column(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Column
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Column(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Column XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ColumnChart())

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Column(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Column
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Column(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Column XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ColumnChart())

    /// <summary>Creates a combination chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Combine(data:seq<Series>, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data
            | Some x ->
                Seq.zip x data
                |> Seq.map (fun (name, series) ->
                    Series.WithName name series)
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Column XTitle YTitle
        HighchartsChart.Create chartData (fun () -> CombinationChart())

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Donut(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Donut(data, defaultArg Name "")|] None None Title None Donut XTitle YTitle
        HighchartsChart.Create chartData (fun () -> DonutChart())

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Donut(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Donut(data, defaultArg Name "")|] None None Title None Donut XTitle YTitle
        HighchartsChart.Create chartData (fun () -> DonutChart())

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Funnel(data, defaultArg Name "")|] None None Title None Funnel XTitle YTitle
        HighchartsChart.Create chartData (fun () -> FunnelChart())

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Funnel(data, defaultArg Name "")|] None None Title None Funnel XTitle YTitle
        HighchartsChart.Create chartData (fun () -> FunnelChart())

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Line(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Line(data, defaultArg Name "")|] None None Title None Line XTitle YTitle
        HighchartsChart.Create chartData (fun () -> LineChart())

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Line(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Line(data, defaultArg Name "")|] None None Title None Line XTitle YTitle
        HighchartsChart.Create chartData (fun () -> LineChart())

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Line(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Line
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Line(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Line XTitle YTitle
        HighchartsChart.Create chartData (fun () -> LineChart())

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Line(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Line
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Line(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Line XTitle YTitle
        HighchartsChart.Create chartData (fun () -> LineChart())

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.PercentArea(data, defaultArg Name "")|] None None Title None PercentArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentAreaChart())

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.PercentArea(data, defaultArg Name "")|] None None Title None PercentArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentAreaChart())

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.PercentArea
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.PercentArea(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None PercentArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentAreaChart())

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.PercentArea
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.PercentArea(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None PercentArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentAreaChart())

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.PercentBar(data, defaultArg Name "")|] None None Title None PercentBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentBarChart())

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.PercentBar(data, defaultArg Name "")|] None None Title None PercentBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentBarChart())

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.PercentBar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.PercentBar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None PercentBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentBarChart())

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.PercentBar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.PercentBar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None PercentBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentBarChart())

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.PercentColumn(data, defaultArg Name "")|] None None Title None PercentColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentColumnChart())

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.PercentColumn(data, defaultArg Name "")|] None None Title None PercentColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentColumnChart())

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.PercentColumn
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.PercentColumn(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None PercentColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentColumnChart())

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.PercentColumn
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.PercentColumn(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None PercentColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PercentColumnChart())

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Pie(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Pie(data, defaultArg Name "")|] None None Title None Pie XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PieChart())

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Pie(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Pie(data, defaultArg Name "")|] None None Title None Pie XTitle YTitle
        HighchartsChart.Create chartData (fun () -> PieChart())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Radar(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Radar(data, defaultArg Name "")|] None None Title None Radar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> RadarChart())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Radar(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Radar(data, defaultArg Name "")|] None None Title None Radar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> RadarChart())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Radar(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Radar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Radar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Radar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> RadarChart())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Radar(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Radar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Radar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Radar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> RadarChart())

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Scatter(data, defaultArg Name "")|] None None Title None Scatter XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ScatterChart())

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Scatter(data, defaultArg Name "")|] None None Title None Scatter XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ScatterChart())

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Scatter
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Scatter(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Scatter XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ScatterChart())

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Scatter
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Scatter(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Scatter XTitle YTitle
        HighchartsChart.Create chartData (fun () -> ScatterChart())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Spline(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Spline(data, defaultArg Name "")|] None None Title None Spline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> SplineChart())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Spline(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.Spline(data, defaultArg Name "")|] None None Title None Spline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> SplineChart())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Spline(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Spline
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Spline(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Spline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> SplineChart())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Spline(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Spline
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Spline(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None Spline XTitle YTitle
        HighchartsChart.Create chartData (fun () -> SplineChart())

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.StackedArea(data, defaultArg Name "")|] None None Title None StackedArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedAreaChart())

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.StackedArea(data, defaultArg Name "")|] None None Title None StackedArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedAreaChart())

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedArea
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedArea(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None StackedArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedAreaChart())

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedArea
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedArea(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None StackedArea XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedAreaChart())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.StackedBar(data, defaultArg Name "")|] None None Title None StackedBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedBarChart())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.StackedBar(data, defaultArg Name "")|] None None Title None StackedBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedBarChart())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedBar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedBar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None StackedBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedBarChart())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedBar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedBar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None StackedBar XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedBarChart())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#key * #value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.StackedColumn(data, defaultArg Name "")|] None None Title None StackedColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedColumnChart())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#value>, ?Name, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts Labels [|Series.StackedColumn(data, defaultArg Name "")|] None None Title None StackedColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedColumnChart())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedColumn
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedColumn(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None StackedColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedColumnChart())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#seq<'V>> when 'V :> value, ?Names:seq<string>, ?Title, ?Labels, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedColumn
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedColumn(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts Labels series None None Title None StackedColumn XTitle YTitle
        HighchartsChart.Create chartData (fun () -> StackedColumnChart())

type Chart with

    /// Set the chart's data set name.
    static member WithName name (chart:#HighchartsChart) =
        chart.WithName name
        chart

    /// Set the chart's data sets names.
    static member WithNames names (chart:#HighchartsChart) =
        chart.WithNames names
        chart

    /// Set the title of a chart.
    static member WithTitle title (chart:#HighchartsChart) =
        chart.WithTitle title
        chart

    /// Set the chart's data points labels.
    static member WithLabels labels (chart:#HighchartsChart) =
        chart.WithLabels labels
        chart

    /// Set the chart's X-axis title.
    static member WithXTitle xTitle (chart:#HighchartsChart) =
        chart.WithXTitle xTitle
        chart

    /// Set the chart's Y-axis title.
    static member WithYTitle yTitle (chart:#HighchartsChart) =
        chart.WithYTitle yTitle
        chart

    /// Display/hide the legend of the chart.
    static member WithLegend enabled (chart:#HighchartsChart) =
        chart.WithLegend enabled
        chart

    /// Close the chart's window.
    static member Close (chart:#HighchartsChart) =
        chart.Close()

    /// Save the chart as a PNG image.
    static member SavePng fileName (chart:#HighchartsChart) =
        chart.SavePng fileName
        chart

open FsPlot.HighchartsDynamic

type DynamicChart =

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Area(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Area(data, defaultArg Name "")|] None None Title None Area XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates an area spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Areaspline(data, defaultArg Name "")|] None None Title None Areaspline XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates an area range chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#key*#value*#value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Arearange(data, defaultArg Name "")|] None None Title None Arearange XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bar(data:seq<#key*#value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Bar(data, defaultArg Name "")|] None None Title None Bar XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#key*#value*#value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Bubble(data, defaultArg Name "")|] None None Title None Bubble XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Column(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Column(data, defaultArg Name "")|] None None Title None Column XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Donut(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Donut(data, defaultArg Name "")|] None None Title None Donut XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Funnel(data, defaultArg Name "")|] None None Title None Funnel XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Line(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Line(data, defaultArg Name "")|] None None Title None Line XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Pie(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Pie(data, defaultArg Name "")|] None None Title None Pie XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Radar(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Radar(data, defaultArg Name "")|] None None Title None Radar XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Scatter(data, defaultArg Name "")|] None None Title None Scatter XTitle YTitle
        HighchartsDynamicChart.Create chartData

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Spline(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Highcharts None [|Series.Spline(data, defaultArg Name "")|] None None Title None Spline XTitle YTitle
        HighchartsDynamicChart.Create chartData

type DynamicChart with

    /// Set the chart's data set name.
    static member WithName name (chart:HighchartsDynamicChart) =
        chart.WithName name
        chart

    /// Set the chart's title.
    static member WithTitle title (chart:HighchartsDynamicChart) =
        chart.WithTitle title
        chart

    /// Set the chart's X-Axis title.
    static member WithXTitle xTitle (chart:HighchartsDynamicChart) =
        chart.WithXTitle xTitle
        chart

    /// Set the chart's Y-axis title.
    static member WithYTitle yTitle (chart:HighchartsDynamicChart) =
        chart.WithYTitle yTitle
        chart

    /// Close the chart's browser window.
    static member Close (chart:HighchartsDynamicChart) =
        chart.Close()

    /// Save the chart as a PNG image.
    static member SavePng fileName (chart:HighchartsDynamicChart) =
        chart.SavePng fileName
        chart

    /// Display/hide the legend of a chart.
    static member WithLegend enabled (chart:HighchartsDynamicChart) =
        chart.WithLegend enabled
        chart

    /// Set the shift property that determines whether
    /// one point is shifted off the start of the series as one
    /// is appended to the end.
    static member WithShift enabled (chart:HighchartsDynamicChart) =
        chart.WithShift enabled
        chart






