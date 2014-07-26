module FsPlot.Google.Charting

open FsPlot.Config
open FsPlot.Data
open FsPlot.GenericChart
open FsPlot.GenericDynamicChart
open FsPlot.Highcharts.Options

type GoogleBar() =
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
