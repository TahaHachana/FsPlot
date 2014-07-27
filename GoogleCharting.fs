module FsPlot.Google.Charting

open FsPlot.Config
open FsPlot.Data
open FsPlot.GenericChart
open FsPlot.GenericDynamicChart
open FsPlot.Highcharts.Options

type GoogleBar() =
    inherit GoogleChart()

type GoogleColumn() =
    inherit GoogleChart()

type GoogleLine() =
    inherit GoogleChart()

type GoogleStackedBar() =
    inherit GoogleChart()

type GoogleStackedColumn() =
    inherit GoogleChart()

type Google =

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:Series, ?label, ?title) =
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|data|] None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:Series list, ?labels, ?title) =
        let chartData = ChartConfig.Google labels (List.toArray data) None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:seq<#key * #value>, ?label, ?title) =
        let series = Series.Bar data
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|series|] None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:#seq<#key * #value> seq, ?labels, ?title) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Google labels data None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:Series, ?label, ?title) =
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|data|] None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:Series list, ?labels, ?title) =
        let chartData = ChartConfig.Google labels (List.toArray data) None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:seq<#key * #value>, ?label, ?title) =
        let series = Series.Bar data
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|series|] None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:#seq<#key * #value> seq, ?labels, ?title) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Google labels data None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a geo chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="region">The area to display on the geochart.</param>
    /// <param name="mode">The geochart type: auto, regions, markers or text.</param>
    /// <param name="sizeAxis">The range used to display proportional marker values.</param>
    static member Geo(data:seq<string * #value>, ?label, ?region, ?mode, ?sizeAxis) =
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let data = Series.Geo data
        let chartData = ChartConfig.Google labels [|data|] None None None None Geo None None
        GoogleGeochart.Create chartData region mode sizeAxis

    /// <summary>Creates a geo chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="region">The area to display on the geochart.</param>
    /// <param name="mode">The geochart type: auto, regions, markers or text.</param>
    /// <param name="sizeAxis">The range used to display proportional marker values.</param>
    static member Geo(data:seq<string * #value * #value>, ?labels, ?region, ?mode, ?sizeAxis) =
        let s1 =
            data
            |> Seq.map (fun (k, v, _) -> k, v)
            |> Series.Geo
        let s2 =
            data
            |> Seq.map (fun (k, _, v) -> k, v)
            |> Series.Geo
        let chartData = ChartConfig.Google labels [|s1; s2|] None None None None Geo None None
        GoogleGeochart.Create chartData region mode sizeAxis

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:Series, ?label, ?title) =
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|data|] None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:Series list, ?labels, ?title) =
        let chartData = ChartConfig.Google labels (List.toArray data) None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:seq<#key * #value>, ?label, ?title) =
        let series = Series.Line data
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|series|] None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:#seq<#key * #value> seq, ?labels, ?title) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Google labels data None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:Series, ?label, ?title) =
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|data|] None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:Series list, ?labels, ?title) =
        let chartData = ChartConfig.Google labels (List.toArray data) None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:seq<#key * #value>, ?label, ?title) =
        let series = Series.StackedBar data
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|series|] None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:#seq<#key * #value> seq, ?labels, ?title) =
        let data =
            data
            |> Seq.map Series.StackedBar
            |> Seq.toArray
        let chartData = ChartConfig.Google labels data None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:Series, ?label, ?title) =
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|data|] None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:Series list, ?labels, ?title) =
        let chartData = ChartConfig.Google labels (List.toArray data) None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:seq<#key * #value>, ?label, ?title) =
        let series = Series.StackedColumn data
        let labels =
            match label with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google labels [|series|] None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data labels displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:#seq<#key * #value> seq, ?labels, ?title) =
        let data =
            data
            |> Seq.map Series.StackedColumn
            |> Seq.toArray
        let chartData = ChartConfig.Google labels data None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())
