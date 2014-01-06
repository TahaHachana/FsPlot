module FsPlot.Highcharts

open DataSeries
open GenericChart
open HighchartsOptions

type HighchartsArea() =
    inherit GenericChart()

    let mutable stacking = Disabled
    let mutable inverted = false

    let compileJs (x:ChartData) =
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        HighchartsJs.area series title legend categories xTitle yTitle tooltip subtitle stacking inverted

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsAreaspline() =
    inherit GenericChart()

    let mutable stacking = Disabled
    let mutable inverted = false

    let compileJs (x:ChartData) =
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        HighchartsJs.areaspline series title legend categories xTitle yTitle tooltip subtitle stacking inverted

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsArearange() =
    inherit GenericChart()

type HighchartsBar() =
    inherit GenericChart()

    let mutable stacking = Disabled

    let compileJs (x:ChartData) =
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        HighchartsJs.bar series title legend categories xTitle yTitle tooltip subtitle stacking

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

type HighchartsBubble() =
    inherit GenericChart()

type HighchartsColumn() =
    inherit GenericChart()

    let mutable stacking = Disabled

    let compileJs (x:ChartData) =
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        HighchartsJs.column series title legend categories xTitle yTitle tooltip subtitle stacking

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

type HighchartsCombination() =
    inherit GenericChart()

    let mutable pieOptions = None

    let js (x:ChartData) =
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        HighchartsJs.combine series title legend categories xTitle yTitle tooltip subtitle pieOptions

    do base.SetJsFun js

    member __.SetPieOptions x =
        pieOptions <- Some x
        base.Navigate()

type HighchartsDonut() =
    inherit GenericChart()

type HighchartsFunnel() =
    inherit GenericChart()

type HighchartsLine() =
    inherit GenericChart()

type HighchartsPercentArea() =
    inherit GenericChart()

    let mutable inverted = false

    let compileJs (x:ChartData) =
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        HighchartsJs.percentArea series title legend categories xTitle yTitle tooltip subtitle inverted

    do base.SetJsFun compileJs

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsPercentBar() =
    inherit GenericChart()

type HighchartsPercentColumn() =
    inherit GenericChart()

type HighchartsPie() =
    inherit GenericChart()

type HighchartsRadar() =
    inherit GenericChart()

type HighchartsScatter() =
    inherit GenericChart()

type HighchartsSpline() =
    inherit GenericChart()

type HighchartsStackedArea() =
    inherit GenericChart()

    let mutable inverted = false

    let compileJs (x:ChartData) =
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        HighchartsJs.stackedArea series title legend categories xTitle yTitle tooltip subtitle inverted

    do base.SetJsFun compileJs

    member __.SetInverted(x) =
        inverted <- x
        base.Navigate()

type HighchartsStackedBar() =
    inherit GenericChart()

type HighchartsStackedColumn() =
    inherit GenericChart()

let private newChartData categories data legend subtitle title tooltip chartType xTitle yTitle =
    let legend' = defaultArg legend false
    let categories' =
        match categories with 
        | None -> [||]
        | Some value -> Seq.toArray value
    ChartData.New categories' data legend' subtitle title tooltip chartType xTitle yTitle

type Highcharts =

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Area xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = newChartData categories [|series|] legend None title None Area xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = newChartData categories [|series|] legend None title None Area xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArea()) 
        
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
        let chartData = newChartData categories data legend None title None Area xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArea()) 

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
        let chartData = newChartData categories data legend None title None Area xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Area xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArea()) 

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
        let chartData = newChartData categories data legend None title None Area xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArea()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Areaspline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Areaspline data
        let chartData = newChartData categories [|series|] legend None title None Areaspline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Areaspline data
        let chartData = newChartData categories [|series|] legend None title None Areaspline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsAreaspline()) 
        
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
        let chartData = newChartData categories data legend None title None Areaspline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsAreaspline()) 

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
        let chartData = newChartData categories data legend None title None Areaspline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Areaspline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsAreaspline()) 

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
        let chartData = newChartData categories data legend None title None Areaspline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsAreaspline()) 

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Arearange xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Arearange data
        let chartData = newChartData categories [|series|] legend None title None Arearange xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArearange())
        
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
        let chartData = newChartData categories data legend None title None Arearange xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArearange())

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
        let chartData = newChartData categories data legend None title None Arearange xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Arearange xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArearange())

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
        let chartData = newChartData categories data legend None title None Arearange xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsArearange())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Bar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bar data
        let chartData = newChartData categories [|series|] legend None title None Bar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bar data
        let chartData = newChartData categories [|series|] legend None title None Bar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBar()) 
        
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
        let chartData = newChartData categories data legend None title None Bar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBar()) 

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
        let chartData = newChartData categories data legend None title None Bar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Bar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBar()) 

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
        let chartData = newChartData categories data legend None title None Bar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBar()) 

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bubble data
        let chartData = newChartData categories [|series|] legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bubble data
        let chartData = newChartData categories [|series|] legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

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
        let chartData = newChartData categories data legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())
        
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
        let chartData = newChartData categories data legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

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
        let chartData = newChartData categories data legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

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
        let chartData = newChartData categories data legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

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
        let chartData = newChartData categories data legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

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
        let chartData = newChartData categories data legend None title None Bubble xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsBubble())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Column xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Column data
        let chartData = newChartData categories [|series|] legend None title None Column xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Column data
        let chartData = newChartData categories [|series|] legend None title None Column xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsColumn()) 
        
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
        let chartData = newChartData categories data legend None title None Column xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsColumn()) 

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
        let chartData = newChartData categories data legend None title None Column xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Column xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsColumn()) 

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
        let chartData = newChartData categories data legend None title None Column xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsColumn()) 

    /// <summary>Creates a combination chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Combine(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories (Seq.toArray data) legend None title None Combination xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsCombination())

    /// <summary>Creates a donut chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Donut xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsDonut()) 

    /// <summary>Creates a donut chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories (Seq.toArray data) legend None title None Donut xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsDonut()) 

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Donut data
        let chartData = newChartData categories [|series|] legend None title None Donut xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsDonut()) 

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Donut data
        let chartData = newChartData categories [|series|] legend None title None Donut xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsDonut()) 

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Funnel xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsFunnel())

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories (Seq.toArray data) legend None title None Funnel xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsFunnel())

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Funnel data
        let chartData = newChartData categories [|series|] legend None title None Funnel xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsFunnel())

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Funnel data
        let chartData = newChartData categories [|series|] legend None title None Funnel xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsFunnel())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Line xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Line data
        let chartData = newChartData categories [|series|] legend None title None Line xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Line data
        let chartData = newChartData categories [|series|] legend None title None Line xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsLine()) 
        
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
        let chartData = newChartData categories data legend None title None Line xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsLine()) 

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
        let chartData = newChartData categories data legend None title None Line xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Line xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsLine()) 

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
        let chartData = newChartData categories data legend None title None Line xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsLine()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None PercentArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentArea data
        let chartData = newChartData categories [|series|] legend None title None PercentArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentArea data
        let chartData = newChartData categories [|series|] legend None title None PercentArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentArea()) 
        
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
        let chartData = newChartData categories data legend None title None PercentArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentArea()) 

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
        let chartData = newChartData categories data legend None title None PercentArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentArea(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None PercentArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentArea()) 

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
        let chartData = newChartData categories data legend None title None PercentArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentArea()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None PercentBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentBar data
        let chartData = newChartData categories [|series|] legend None title None PercentBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentBar data
        let chartData = newChartData categories [|series|] legend None title None PercentBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentBar()) 
        
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
        let chartData = newChartData categories data legend None title None PercentBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentBar()) 

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
        let chartData = newChartData categories data legend None title None PercentBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentBar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None PercentBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentBar()) 

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
        let chartData = newChartData categories data legend None title None PercentBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentBar()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None PercentColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentColumn data
        let chartData = newChartData categories [|series|] legend None title None PercentColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.PercentColumn data
        let chartData = newChartData categories [|series|] legend None title None PercentColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentColumn()) 
        
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
        let chartData = newChartData categories data legend None title None PercentColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentColumn()) 

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
        let chartData = newChartData categories data legend None title None PercentColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a percent column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member PercentColumn(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None PercentColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentColumn()) 

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
        let chartData = newChartData categories data legend None title None PercentColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPercentColumn()) 

    /// <summary>Creates a pie chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Pie xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPie()) 

    /// <summary>Creates a pie chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories (Seq.toArray data) legend None title None Pie xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPie()) 

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Pie data
        let chartData = newChartData categories [|series|] legend None title None Pie xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPie()) 

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Pie data
        let chartData = newChartData categories [|series|] legend None title None Pie xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsPie()) 

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Radar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Radar data
        let chartData = newChartData categories [|series|] legend None title None Radar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a Radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Radar data
        let chartData = newChartData categories [|series|] legend None title None Radar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsRadar())
        
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
        let chartData = newChartData categories data legend None title None Radar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsRadar())

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
        let chartData = newChartData categories data legend None title None Radar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsRadar())

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Radar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsRadar())

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
        let chartData = newChartData categories data legend None title None Radar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsRadar())
        
    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Scatter xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Scatter data
        let chartData = newChartData categories [|series|] legend None title None Scatter xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Scatter data
        let chartData = newChartData categories [|series|] legend None title None Scatter xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsScatter()) 
        
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
        let chartData = newChartData categories data legend None title None Scatter xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsScatter()) 

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
        let chartData = newChartData categories data legend None title None Scatter xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Scatter xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsScatter()) 

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
        let chartData = newChartData categories data legend None title None Scatter xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsScatter()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None Spline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Spline data
        let chartData = newChartData categories [|series|] legend None title None Spline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Spline data
        let chartData = newChartData categories [|series|] legend None title None Spline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsSpline()) 
        
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
        let chartData = newChartData categories data legend None title None Spline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsSpline()) 

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
        let chartData = newChartData categories data legend None title None Spline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None Spline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsSpline()) 

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
        let chartData = newChartData categories data legend None title None Spline xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsSpline()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None StackedArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = newChartData categories [|series|] legend None title None StackedArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = newChartData categories [|series|] legend None title None StackedArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedArea()) 
        
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
        let chartData = newChartData categories data legend None title None StackedArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedArea()) 

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
        let chartData = newChartData categories data legend None title None StackedArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedArea(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None StackedArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedArea()) 

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
        let chartData = newChartData categories data legend None title None StackedArea xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedArea()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None StackedBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedBar data
        let chartData = newChartData categories [|series|] legend None title None StackedBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedBar data
        let chartData = newChartData categories [|series|] legend None title None StackedBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedBar()) 
        
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
        let chartData = newChartData categories data legend None title None StackedBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedBar()) 

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
        let chartData = newChartData categories data legend None title None StackedBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedBar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None StackedBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedBar()) 

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
        let chartData = newChartData categories data legend None title None StackedBar xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedBar()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None title None StackedColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedColumn data
        let chartData = newChartData categories [|series|] legend None title None StackedColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.StackedColumn data
        let chartData = newChartData categories [|series|] legend None title None StackedColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedColumn()) 
        
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
        let chartData = newChartData categories data legend None title None StackedColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedColumn()) 

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
        let chartData = newChartData categories data legend None title None StackedColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member StackedColumn(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None title None StackedColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedColumn()) 

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
        let chartData = newChartData categories data legend None title None StackedColumn xTitle yTitle
        GenericChart.Create chartData (fun () -> HighchartsStackedColumn()) 

    /// <summary>Sets the categories of a chart's X-axis.</summary>
    static member categories categories (chart:#GenericChart) =
        chart.SetCategories categories
        chart

    /// <summary>Closes the chart's window.</summary>
    static member close (chart:#GenericChart) = chart.Close()

    /// <summary>Sets the data series used by a chart.</summary>
    static member data (series:seq<Series>) (chart:#GenericChart) = chart.SetData series

    /// <summary>Hides the legend of a chart.</summary>
    static member hideLegend (chart:#GenericChart) =
        chart.HideLegend()
        chart

    static member plot (series:Series) =
        match series.Type with
        | Area -> Highcharts.Area series :> GenericChart
        | Areaspline -> Highcharts.Areaspline series :> GenericChart
        | Arearange -> Highcharts.Arearange series :> GenericChart
        | Bar -> Highcharts.Bar series :> GenericChart
        | Bubble -> Highcharts.Bubble series :> GenericChart
        | Column -> Highcharts.Column series :> GenericChart
        | Donut -> Highcharts.Donut series :> GenericChart
        | Funnel -> Highcharts.Funnel series :> GenericChart
        | Line -> Highcharts.Line series :> GenericChart
        | PercentArea -> Highcharts.PercentArea series :> GenericChart
        | PercentBar -> Highcharts.PercentBar series :> GenericChart
        | PercentColumn -> Highcharts.PercentColumn series :> GenericChart
        | Pie -> Highcharts.Pie series :> GenericChart
        | Radar -> Highcharts.Radar series :> GenericChart
        | Scatter -> Highcharts.Scatter series :> GenericChart
        | Spline -> Highcharts.Spline series :> GenericChart
        | StackedArea -> Highcharts.StackedArea series :> GenericChart
        | StackedBar -> Highcharts.StackedBar series :> GenericChart
        | _ -> Highcharts.StackedColumn series :> GenericChart

    static member plot (series:seq<Series>) =
        let types =
            series
            |> Seq.map (fun x -> x.Type)
            |> Seq.distinct
        match Seq.length types with
        | 1 ->
            match Seq.nth 0 types with
            | Area -> Highcharts.Area series :> GenericChart
            | Areaspline -> Highcharts.Areaspline series :> GenericChart
            | Arearange -> Highcharts.Arearange series :> GenericChart
            | Bar -> Highcharts.Bar series :> GenericChart
            | Bubble -> Highcharts.Bubble series :> GenericChart
            | Column -> Highcharts.Column series :> GenericChart
            | Donut -> Highcharts.Donut series :> GenericChart
            | Funnel -> Highcharts.Funnel series :> GenericChart
            | Line -> Highcharts.Line series :> GenericChart
            | PercentArea -> Highcharts.PercentArea series :> GenericChart
            | PercentBar -> Highcharts.PercentBar series :> GenericChart
            | PercentColumn -> Highcharts.PercentColumn series :> GenericChart
            | Pie -> Highcharts.Pie series :> GenericChart
            | Radar -> Highcharts.Radar series :> GenericChart
            | Scatter -> Highcharts.Scatter series :> GenericChart
            | Spline -> Highcharts.Spline series :> GenericChart
            | StackedArea -> Highcharts.StackedArea series :> GenericChart
            | StackedBar -> Highcharts.StackedBar series :> GenericChart
            | _ -> Highcharts.StackedColumn series :> GenericChart
        | _ -> Highcharts.Combine series :> GenericChart

    /// <summary>Displays the chart's legend.</summary>
    static member showLegend (chart:#GenericChart) =
        chart.ShowLegend()
        chart

    /// <summary>Sets the chart's subtitle.</summary>
    static member subtitle subtitle (chart:#GenericChart) =
        chart.SetSubtitle subtitle
        chart

    /// <summary>Sets the chart's title.</summary>
    static member title title (chart:#GenericChart) =
        chart.SetTitle title
        chart

    /// <summary>Modifies the data points' tooltip format.</summary>
    static member tooltip format (chart:#GenericChart) =
        chart.SetTooltip format
        chart

    /// <summary>Sets the chart's X-axis title.</summary>
    static member xTitle xTitle (chart:#GenericChart) =
        chart.SetXTitle xTitle
        chart

    /// <summary>Sets the chart's Y-axis title.</summary>
    static member yTitle yTitle (chart:#GenericChart) =
        chart.SetYTitle yTitle
        chart