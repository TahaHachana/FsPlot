module FsPlot.Google.Charting

open FsPlot
open FsPlot.Config
open FsPlot.Data
open FsPlot.Google.Options
open System
open System.IO

type GoogleChart() as chart =
    
    [<DefaultValue>] val mutable private config : ChartConfig    

    let mutable jsFun = Js.google
    let htmlFun = Html.google

    let guid = Guid.NewGuid().ToString()
    let htmlFile = Path.GetTempPath() + guid + ".html"
    do File.WriteAllText(htmlFile, "")
    let browser = Browser.start htmlFile

    let ctx = System.Threading.SynchronizationContext.Current

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

    static member internal Create x (f:unit -> #GoogleChart) =
        let gc = f()
        gc.config <- x
        gc.Refresh()
        gc

    member internal __.Refresh() = agent.Post chart.config

    /// Set the chart's title.
    member __.WithTitle title =
        chart.config <-
            {
                chart.config with
                    Title = Some title
            }
        chart.Refresh()

    /// Set the chart's data set name.
    member __.WithName name =
        chart.config <-
            {
                chart.config with
                    Data = [|chart.config.Data.[0] |> Series.WithName name |]
            }
        chart.Refresh()

    /// Set the chart's data sets names.
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

    /// Set the chart's X-axis title.
    member __.WithXTitle xTitle =
        chart.config <-
            {
                chart.config with
                    XTitle = Some xTitle
            }
        chart.Refresh()

    /// Set the chart's Y-axis title.
    member __.WithYTitle yTitle =
        chart.config <-
            {
                chart.config with
                    YTitle = Some yTitle
            }
        chart.Refresh()

    /// Display/hide the legend of the chart.
    member __.WithLegend enabled =
        chart.config <-
            {
                chart.config with
                    Legend = enabled
            }
        chart.Refresh()

    /// Close the chart's window.
    member __.Close() =
        browser.Quit()
        File.Delete htmlFile

    /// Save the chart as a PNG image.
    member __.SavePng(fileName) =
        browser
            .GetScreenshot()
            .SaveAsFile(fileName, Drawing.Imaging.ImageFormat.Png)

type BarChart() =
    inherit GoogleChart()

type ColumnChart() =
    inherit GoogleChart()

type LineChart() =
    inherit GoogleChart()

type SplineChart() =
    inherit GoogleChart()

type StackedBarChart() =
    inherit GoogleChart()

type StackedColumnChart() =
    inherit GoogleChart()

type Chart =

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bar(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Google None [|Series.Bar(data, defaultArg Name "")|] None None Title None Bar XTitle YTitle
        GoogleChart.Create chartData (fun () -> BarChart())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Bar(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Bar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Bar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Google None series None None Title None Bar XTitle YTitle
        GoogleChart.Create chartData (fun () -> BarChart())

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Column(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Google None [|Series.Column(data, defaultArg Name "")|] None None Title None Column XTitle YTitle
        GoogleChart.Create chartData (fun () -> ColumnChart())

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Column(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Column
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Column(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Google None series None None Title None Column XTitle YTitle
        GoogleChart.Create chartData (fun () -> ColumnChart())

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Line(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Google None [|Series.Line(data, defaultArg Name "")|] None None Title None Line XTitle YTitle
        GoogleChart.Create chartData (fun () -> LineChart())

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Line(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Line
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Line(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Google None series None None Title None Line XTitle YTitle
        GoogleChart.Create chartData (fun () -> LineChart())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Spline(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Google None [|Series.Spline(data, defaultArg Name "")|] None None Title None Spline XTitle YTitle
        GoogleChart.Create chartData (fun () -> SplineChart())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member Spline(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.Spline
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.Spline(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Google None series None None Title None Spline XTitle YTitle
        GoogleChart.Create chartData (fun () -> SplineChart())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Google None [|Series.StackedColumn(data, defaultArg Name "")|] None None Title None StackedBar XTitle YTitle
        GoogleChart.Create chartData (fun () -> StackedBarChart())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedBar
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedBar(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Google None series None None Title None StackedBar XTitle YTitle
        GoogleChart.Create chartData (fun () -> StackedBarChart())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Name">The data set name.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#key * #value>, ?Name, ?Title, ?XTitle, ?YTitle) =
        let chartData = ChartConfig.Google None [|Series.StackedColumn(data, defaultArg Name "")|] None None Title None StackedColumn XTitle YTitle
        GoogleChart.Create chartData (fun () -> StackedColumnChart())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="Names">The data sets names.</param>
    /// <param name="Title">The chart's title.</param>
    /// <param name="Labels">The chart's data points labels.</param>
    /// <param name="XTitle">The X-axis title.</param>
    /// <param name="YTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#seq<'K * 'V>> when 'K :> key and 'V :> value, ?Names:seq<string>, ?Title, ?XTitle, ?YTitle) =
        let series =
            match Names with
            | None -> data |> Seq.map Series.StackedColumn
            | Some x ->
                Seq.zip data x
                |> Seq.map (fun (dataset, name) ->
                    Series.StackedColumn(dataset, name))
            |> Seq.toArray
        let chartData = ChartConfig.Google None series None None Title None StackedColumn XTitle YTitle
        GoogleChart.Create chartData (fun () -> StackedColumnChart())

type Chart with

    /// <summary>Sets the chart's data set name.</summary>  
    static member WithName name (chart:#GoogleChart) =
        chart.WithName name
        chart

    /// <summary>Sets the chart's data sets names.</summary>  
    static member WithNames names (chart:#GoogleChart) =
        chart.WithNames names
        chart

    /// <summary>Sets the title of a chart.</summary>  
    static member WithTitle title (chart:#GoogleChart) =
        chart.WithTitle title
        chart

    /// <summary>Sets the chart's X-axis title.</summary>
    static member WithXTitle xTitle (chart:#GoogleChart) =
        chart.WithXTitle xTitle
        chart

    /// <summary>Sets the chart's Y-axis title.</summary>
    static member WithYTitle yTitle (chart:#GoogleChart) =
        chart.WithYTitle yTitle
        chart

    /// <summary>Display/hide the legend of the chart.</summary>
    static member WithLegend enabled (chart:#GoogleChart) =
        chart.WithLegend enabled
        chart

    /// <summary>Closes the chart's window.</summary>
    static member Close (chart:#GoogleChart) =
        chart.Close()

    /// Save the chart as a PNG image.
    static member SavePng fileName (chart:#GoogleChart) =
        chart.SavePng fileName
        chart

type GoogleGeochart() as chart =
    
    [<DefaultValue>] val mutable private config: ChartConfig    
    [<DefaultValue>] val mutable private region: string option
    [<DefaultValue>] val mutable private mode: string option
    [<DefaultValue>] val mutable private sizeAxis: SizeAxis option

    let mutable jsFun = Google.Js.geo 
    let htmlFun = Html.google

    let guid = Guid.NewGuid().ToString()
    let htmlFile = Path.GetTempPath() + guid + ".html"
    do File.WriteAllText(htmlFile, "")
    let browser = Browser.start htmlFile

    let ctx = System.Threading.SynchronizationContext.Current

    let agent =
        MailboxProcessor<ChartConfig>.Start(fun inbox ->
            let rec loop() =
                async {
                    let! msg = inbox.Receive()
                    match inbox.CurrentQueueLength with
                    | 0 ->
                        let js = jsFun msg chart.region chart.mode chart.sizeAxis
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

    static member internal Create x region mode sizeAxis =
        let gc = GoogleGeochart()
        gc.region <- region
        gc.mode <- mode
        gc.sizeAxis <- sizeAxis
        gc.config <- x
        gc.Refresh()
        gc

    member internal __.Refresh() = agent.Post chart.config

    /// The area to display on the geochart.
    member __.WithRegion region = 
        chart.region <- Some region
        agent.Post chart.config

    /// Which type of geochart this is: "auto", "regions", "markers" or "text".
    member __.WithMode mode = 
        chart.mode <- Some mode
        agent.Post chart.config

    /// Configure how values are associated with bubble size.
    member __.WithSizeAxis sizeAxis = 
        chart.sizeAxis <- Some sizeAxis
        agent.Post chart.config

    /// Set the chart's data set name.
    member __.WithName name =
        chart.config <-
            {
                chart.config with
                    Data = [|chart.config.Data.[0] |> Series.WithName name |]
            }
        chart.Refresh()

    /// Set the chart's data sets names.
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

    /// Close the chart's window.
    member __.Close() =
        browser.Quit()
        File.Delete htmlFile

    /// Save the chart as a PNG image.
    member __.SavePng(fileName) =
        browser
            .GetScreenshot()
            .SaveAsFile(fileName, Drawing.Imaging.ImageFormat.Png)

type Geochart =

    /// <summary>Creates a geo chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="region">The area to display on the geochart.</param>
    /// <param name="mode">The geochart type: auto, regions, markers or text.</param>
    /// <param name="sizeAxis">The range used to display proportional marker values.</param>
    static member New(data:seq<string * #value>, ?Name, ?Region, ?Mode, ?SizeAxis) =
        let chartData = ChartConfig.Google None [|Series.Geo(data, defaultArg Name "")|] None None None None Geo None None
        GoogleGeochart.Create chartData Region Mode SizeAxis

    /// <summary>Creates a geo chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="region">The area to display on the geochart.</param>
    /// <param name="mode">The geochart type: auto, regions, markers or text.</param>
    /// <param name="sizeAxis">The range used to display proportional marker values.</param>
    static member New(data:seq<string * #value * #value>, ?Names, ?Region, ?Mode, ?SizeAxis) =
        let s1 =
            data
            |> Seq.map (fun (k, v, _) -> k, v)
            |> Series.Geo
        let s2 =
            data
            |> Seq.map (fun (k, _, v) -> k, v)
            |> Series.Geo
        let series =
            match Names with
            | None -> [|s1; s2|]
            | Some x ->
                Seq.zip x [|s1; s2|]
                |> Seq.map (fun (name, s) ->
                    Series.WithName name s)
                |> Seq.toArray
        let chartData = ChartConfig.Google None series None None None None Geo None None
        GoogleGeochart.Create chartData Region Mode SizeAxis

    /// The area to display on the geochart.
    static member WithRegion region (geochart:GoogleGeochart) = 
        geochart.WithRegion region
        geochart

    /// Which type of geochart this is: "auto", "regions", "markers" or "text".
    static member WithMode mode (geochart:GoogleGeochart) = 
        geochart.WithMode mode
        geochart

    /// Configure how values are associated with bubble size.
    static member WithSizeAxis sizeAxis (geochart:GoogleGeochart) = 
        geochart.WithSizeAxis sizeAxis
        geochart

    static member Close (geochart:GoogleGeochart) = geochart.Close()

    /// Set the chart's data set name.
    static member WithName name (geochart:GoogleGeochart) =
        geochart.WithName name
        geochart

    /// Set the chart's data sets names.
    static member WithNames names (geochart:GoogleGeochart) =
        geochart.WithNames names
        geochart

    /// Save the chart as a PNG image.
    static member SavePng fileName (geochart:GoogleGeochart) =
        geochart.SavePng fileName
        geochart

