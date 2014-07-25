module FsPlot.Google.Charting

open FsPlot.Config
open FsPlot.Data
open FsPlot.GenericChart
open FsPlot.GenericDynamicChart
open FsPlot.Highcharts.Options

type GoogleBar() =
    inherit GoogleChart()

type Google =

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    // (data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle)
    static member Bar(data:Series) =
        let chartData = ChartConfig.Google None [|data|] None None None None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar()) 
