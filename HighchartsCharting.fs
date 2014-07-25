module FsPlot.Highcharts.Charting

open FsPlot.Config
open FsPlot.Data
open FsPlot.GenericChart
open FsPlot.GenericDynamicChart
open FsPlot.Highcharts.Options

type HighchartsArea() =
    inherit HighchartsChart()

    let mutable stacking = Disabled
    let mutable inverted = false

    let compileJs (config:ChartConfig) = Js.area config stacking inverted

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsAreaspline() =
    inherit HighchartsChart()

    let mutable stacking = Disabled
    let mutable inverted = false

    let compileJs (config:ChartConfig) = Js.areaspline config stacking inverted

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsArearange() =
    inherit HighchartsChart()

type HighchartsBar() =
    inherit HighchartsChart()

    let mutable stacking = Disabled

    let compileJs (config:ChartConfig) = Js.bar config stacking

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

type HighchartsBubble() =
    inherit HighchartsChart()

type HighchartsColumn() =
    inherit HighchartsChart()

    let mutable stacking = Disabled

    let compileJs (config:ChartConfig) = Js.column config stacking

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

type HighchartsCombination() =
    inherit HighchartsChart()

    let mutable pieOptions = None

    let js (config:ChartConfig) = Js.combine config pieOptions

    do base.SetJsFun js

    member __.SetPieOptions x =
        pieOptions <- Some x
        base.Navigate()

type HighchartsDonut() =
    inherit HighchartsChart()

type HighchartsFunnel() =
    inherit HighchartsChart()

type HighchartsLine() =
    inherit HighchartsChart()

type HighchartsPercentArea() =
    inherit HighchartsChart()

    let mutable inverted = false

    let compileJs (config:ChartConfig) = Js.percentArea config inverted

    do base.SetJsFun compileJs

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsPercentBar() =
    inherit HighchartsChart()

type HighchartsPercentColumn() =
    inherit HighchartsChart()

type HighchartsPie() =
    inherit HighchartsChart()

type HighchartsRadar() =
    inherit HighchartsChart()

type HighchartsScatter() =
    inherit HighchartsChart()

type HighchartsSpline() =
    inherit HighchartsChart()

type HighchartsStackedArea() =
    inherit HighchartsChart()

    let mutable inverted = false

    let compileJs (config:ChartConfig) = Js.stackedArea config inverted

    do base.SetJsFun compileJs

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsStackedBar() =
    inherit HighchartsChart()

type HighchartsStackedColumn() =
    inherit HighchartsChart()

type Highcharts =

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Area xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Area xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Area xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArea()) 
        
    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Area
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Area xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Area
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Area xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Area xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Area
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Area xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Areaspline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Areaspline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Areaspline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Areaspline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Areaspline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsAreaspline()) 
        
    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Areaspline
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Areaspline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Areaspline
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Areaspline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Areaspline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Areaspline
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Areaspline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Arearange xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Arearange data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Arearange xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArearange())
        
    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<(#key*#value*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Arearange
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Arearange xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<(#key*#value*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Arearange
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Arearange xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Arearange xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<seq<#key*#value*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Arearange
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Arearange xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Bar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBar())
        
    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Bar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bubble data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bubble data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<(#key*#value*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bubble
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())
        
    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<(#value*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bubble
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<(#key*#value*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bubble
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<(#value*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bubble
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<seq<#key*#value*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bubble
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<seq<#value*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Bubble
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Bubble xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Column xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Column data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Column xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Column data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Column xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsColumn()) 
        
    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Column
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Column xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Column
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Column xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Column xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Column
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Column xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a combination chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Combine(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories (Seq.toArray data) legend None title None Combination xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsCombination())

    /// <summary>Creates a donut chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Donut xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsDonut()) 

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Donut data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Donut xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsDonut()) 

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Donut data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Donut xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsDonut()) 

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Funnel xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsFunnel())

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Funnel data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Funnel xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsFunnel())

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Funnel data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Funnel xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsFunnel())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Line xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Line data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Line xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Line data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Line xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsLine())
        
    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Line
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Line xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Line
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Line xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Line xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Line
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Line xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None PercentArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentArea data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None PercentArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentArea data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None PercentArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentArea()) 
        
    /// <summary>Creates a percent area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentArea
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentArea
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentArea
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None PercentBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentBar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None PercentBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentBar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None PercentBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentBar()) 
        
    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentBar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentBar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentBar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None PercentColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentColumn data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None PercentColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentColumn data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None PercentColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentColumn()) 
        
    /// <summary>Creates a percent column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentColumn
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentColumn
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.PercentColumn
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None PercentColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a pie chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Pie xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPie()) 

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Pie data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Pie xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPie()) 

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Pie data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Pie xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsPie())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Radar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Radar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Radar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Radar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Radar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsRadar())
        
    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Radar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Radar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Radar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Radar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Radar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Radar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Radar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsRadar())
        
    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Scatter xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Scatter data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Scatter xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Scatter data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Scatter xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsScatter())
        
    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Scatter
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Scatter xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Scatter
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Scatter xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Scatter xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Scatter
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Scatter xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Spline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Spline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Spline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Spline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Spline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsSpline())
        
    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Spline
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Spline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Spline
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Spline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None Spline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Spline
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None Spline xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None StackedArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None StackedArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None StackedArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedArea()) 
        
    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Area
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Area
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.Area
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedArea xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None StackedBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedBar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None StackedBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedBar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None StackedBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedBar()) 
        
    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.StackedBar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.StackedBar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.StackedBar
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedBar xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None StackedColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedColumn data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None StackedColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedColumn data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None StackedColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedColumn()) 
        
    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.StackedColumn
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.StackedColumn
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data =
            data
            |> Seq.map Series.StackedColumn
            |> Seq.toArray
        let chartData = ChartConfig.Highcharts categories data legend None title None StackedColumn xTitle yTitle
        HighchartsChart.Create chartData (fun () -> HighchartsStackedColumn()) 

type Chart =

    /// <summary>Sets the categories of a chart's X-axis.</summary>
    static member categories categories (chart:#HighchartsChart) =
        chart.SetCategories categories
        chart

    /// <summary>Closes the chart's window.</summary>
    static member close (chart:#HighchartsChart) = chart.Close()

    /// <summary>Sets the data series used by a chart.</summary>
    static member data (series:seq<Series>) (chart:#HighchartsChart) = chart.SetData series

    /// <summary>Hides the legend of a chart.</summary>
    static member hideLegend (chart:#HighchartsChart) =
        chart.HideLegend()
        chart

    static member plot (series:Series) =
        match series.Type with
        | Area -> Highcharts.Area series :> HighchartsChart
        | Areaspline -> Highcharts.Areaspline series :> HighchartsChart
        | Arearange -> Highcharts.Arearange series :> HighchartsChart
        | Bar -> Highcharts.Bar series :> HighchartsChart
        | Bubble -> Highcharts.Bubble series :> HighchartsChart
        | Column -> Highcharts.Column series :> HighchartsChart
        | Donut -> Highcharts.Donut series :> HighchartsChart
        | Funnel -> Highcharts.Funnel series :> HighchartsChart
        | Line -> Highcharts.Line series :> HighchartsChart
        | PercentArea -> Highcharts.PercentArea series :> HighchartsChart
        | PercentBar -> Highcharts.PercentBar series :> HighchartsChart
        | PercentColumn -> Highcharts.PercentColumn series :> HighchartsChart
        | Pie -> Highcharts.Pie series :> HighchartsChart
        | Radar -> Highcharts.Radar series :> HighchartsChart
        | Scatter -> Highcharts.Scatter series :> HighchartsChart
        | Spline -> Highcharts.Spline series :> HighchartsChart
        | StackedArea -> Highcharts.StackedArea series :> HighchartsChart
        | StackedBar -> Highcharts.StackedBar series :> HighchartsChart
        | _ -> Highcharts.StackedColumn series :> HighchartsChart

    static member plot (series:seq<Series>) =
        let types =
            series
            |> Seq.map (fun x -> x.Type)
            |> Seq.distinct
        match Seq.length types with
        | 1 ->
            match Seq.nth 0 types with
            | Area -> Highcharts.Area series :> HighchartsChart
            | Areaspline -> Highcharts.Areaspline series :> HighchartsChart
            | Arearange -> Highcharts.Arearange series :> HighchartsChart
            | Bar -> Highcharts.Bar series :> HighchartsChart
            | Bubble -> Highcharts.Bubble series :> HighchartsChart
            | Column -> Highcharts.Column series :> HighchartsChart
//            | Donut -> Highcharts.Donut series :> HighchartsChart
//            | Funnel -> Highcharts.Funnel series :> HighchartsChart
            | Line -> Highcharts.Line series :> HighchartsChart
            | PercentArea -> Highcharts.PercentArea series :> HighchartsChart
            | PercentBar -> Highcharts.PercentBar series :> HighchartsChart
            | PercentColumn -> Highcharts.PercentColumn series :> HighchartsChart
//            | Pie -> Highcharts.Pie series :> HighchartsChart
            | Radar -> Highcharts.Radar series :> HighchartsChart
            | Scatter -> Highcharts.Scatter series :> HighchartsChart
            | Spline -> Highcharts.Spline series :> HighchartsChart
            | StackedArea -> Highcharts.StackedArea series :> HighchartsChart
            | StackedBar -> Highcharts.StackedBar series :> HighchartsChart
            | _ -> Highcharts.StackedColumn series :> HighchartsChart
        | _ -> Highcharts.Combine series :> HighchartsChart

    /// <summary>Displays the chart's legend.</summary>
    static member showLegend (chart:#HighchartsChart) =
        chart.ShowLegend()
        chart

    /// <summary>Sets the chart's subtitle.</summary>
    static member subtitle subtitle (chart:#HighchartsChart) =
        chart.SetSubtitle subtitle
        chart

    /// <summary>Sets the chart's title.</summary>
    static member title title (chart:#HighchartsChart) =
        chart.SetTitle title
        chart

    /// <summary>Modifies the data points' tooltip format.</summary>
    static member tooltip format (chart:#HighchartsChart) =
        chart.SetTooltip format
        chart

    /// <summary>Sets the chart's X-axis title.</summary>
    static member xTitle xTitle (chart:#HighchartsChart) =
        chart.SetXTitle xTitle
        chart

    /// <summary>Sets the chart's Y-axis title.</summary>
    static member yTitle yTitle (chart:#HighchartsChart) =
        chart.SetYTitle yTitle
        chart

type DynamicHighcharts =

    /// <summary>Creates a dynamic area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Area(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Area xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Area(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Area data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Area xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Area(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Area data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Area xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Areaspline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Areaspline xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Areaspline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Areaspline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Areaspline xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Areaspline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Areaspline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Areaspline xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Arearange(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Arearange xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Arearange(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Arearange data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Arearange xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Bar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Bar xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Bar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Bar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bar xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Bar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Bar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bar xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Bubble(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Bubble xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Bubble(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Bubble data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bubble xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Bubble(data:seq<#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Bubble data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Bubble xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Column(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Column xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Column(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Column data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Column xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Column(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Column data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Column xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic donut chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Donut(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Donut xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Donut(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Donut data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Donut xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Donut(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Donut data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Donut xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic funnel chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Funnel(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Funnel xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Funnel(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Funnel data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Funnel xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Funnel(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Funnel data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Funnel xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Line(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Line xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Line(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Line data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Line xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Line(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Line data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Line xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic pie chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Pie(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Pie xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Pie(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Pie data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Pie xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Pie(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Pie data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Pie xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Radar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Radar xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Radar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Radar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Radar xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Radar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Radar data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Radar xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Scatter(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Scatter xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Scatter(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Scatter data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Scatter xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Scatter(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Scatter data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Scatter xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Spline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let chartData = ChartConfig.Highcharts categories [|data|] legend None title None Spline xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Spline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Spline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Spline xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

    /// <summary>Creates a dynamic spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    /// <param name="shift">When shift is true, one point is shifted off the start of the series as one is appended to the end.</param>
    static member Spline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle, ?shift) =
        let series = Series.Spline data
        let chartData = ChartConfig.Highcharts categories [|series|] legend None title None Spline xTitle yTitle
        let shift = defaultArg shift false
        GenericDynamicChart.Create chartData shift

type DynamicChart =

    /// <summary>Sets the categories of a chart's X-axis.</summary>
    static member categories categories (chart:GenericDynamicChart) =
        chart.SetCategories categories
        chart

    /// <summary>Closes the chart's window.</summary>
    static member close (chart:GenericDynamicChart) = chart.Close()

    /// <summary>Sets the data series used by a chart.</summary>
    static member data (series:Series) (chart:GenericDynamicChart) = chart.SetData series

    /// <summary>Hides the legend of a chart.</summary>
    static member hideLegend (chart:GenericDynamicChart) =
        chart.HideLegend()
        chart

    static member plot (series:Series) =
        match series.Type with
        | Area -> DynamicHighcharts.Area series
        | Areaspline -> DynamicHighcharts.Areaspline series
        | Arearange -> DynamicHighcharts.Arearange series
        | Bar -> DynamicHighcharts.Bar series
        | Bubble -> DynamicHighcharts.Bubble series
        | Column -> DynamicHighcharts.Column series
        | Donut -> DynamicHighcharts.Donut series
        | Funnel -> DynamicHighcharts.Funnel series
        | Line -> DynamicHighcharts.Line series
        | Pie -> DynamicHighcharts.Pie series
        | Radar -> DynamicHighcharts.Radar series
        | Scatter -> DynamicHighcharts.Scatter series
        | _ -> DynamicHighcharts.Spline series

    /// <summary>Sets the shift property that determines whether one point is shifted off the start of the series as one is appended to the end.</summary>
    static member shift shift (chart:GenericDynamicChart) =
        chart.SetShift shift
        chart

    /// <summary>Displays the chart's legend.</summary>
    static member showLegend (chart:GenericDynamicChart) =
        chart.ShowLegend()
        chart

    /// <summary>Sets the chart's subtitle.</summary>
    static member subtitle subtitle (chart:GenericDynamicChart) =
        chart.SetSubtitle subtitle
        chart

    /// <summary>Sets the chart's title.</summary>
    static member title title (chart:GenericDynamicChart) =
        chart.SetTitle title
        chart

    /// <summary>Modifies the data points' tooltip format.</summary>
    static member tooltip format (chart:GenericDynamicChart) =
        chart.SetTooltip format
        chart

    /// <summary>Sets the chart's X-axis title.</summary>
    static member xTitle xTitle (chart:GenericDynamicChart) =
        chart.SetXTitle xTitle
        chart

    /// <summary>Sets the chart's Y-axis title.</summary>
    static member yTitle yTitle (chart:GenericDynamicChart) =
        chart.SetYTitle yTitle
        chart