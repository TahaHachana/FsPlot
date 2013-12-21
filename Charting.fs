module FsPlot.Charting

open Options
open DataSeries

type ChartData =
    {
        Categories : string []
        Data : Series []
        Legend : bool
        PointFormat : string option
        Subtitle : string option
        Title : string option
        Type : ChartType
        XTitle : string option
        YTitle : string option
    }

    static member New categories data legend pointFormat subtitle title chartType xTitle yTitle =
        {
            Categories = categories
            Data = data
            Legend = legend
            PointFormat = pointFormat
            Subtitle = subtitle
            Title = title
            Type = chartType
            XTitle = xTitle
            YTitle = yTitle
        }

let private compileJs (chartData:ChartData) =
    let chartType, data, title, pointFormat, legend, categories, xTitle, yTitle, subtitle =
        chartData.Type, chartData.Data, chartData.Title, chartData.PointFormat,
        chartData.Legend , chartData.Categories, chartData.XTitle,
        chartData.YTitle, chartData.Subtitle
    match chartType with
    | Area -> HighchartsJs.area data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled false
    | Areaspline -> HighchartsJs.areaspline data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled false
    | Arearange -> HighchartsJs.arearange data title legend categories xTitle yTitle pointFormat subtitle
    | Bar -> HighchartsJs.bar data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled
    | Bubble -> HighchartsJs.bubble data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled
    | Column -> HighchartsJs.column data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled
    | Combination -> HighchartsJs.comb data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled
    | Line -> HighchartsJs.line data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled
    | Pie -> HighchartsJs.pie data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled
    | Scatter -> HighchartsJs.scatter data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled

type GenericChart() as chart =

    [<DefaultValue>] val mutable private chartData : ChartData
    [<DefaultValue>] val mutable private jsField : string
    [<DefaultValue>] val mutable private htmlField : string
    
    let mutable compileFun = compileJs
    
    let wnd, browser = ChartWindow.show()
    
    member __.JsFun
        with set(f) = compileFun <- f

    member __.SetSubtite subtitle =
        chart.chartData <- { chart.chartData with Subtitle = Some subtitle }
        __.Navigate()

    member __.Categories
        with get() = chart.chartData.Categories |> Array.toSeq
        and set(categories) =
            chart.chartData <- { chart.chartData with Categories = Seq.toArray categories}
            __.Navigate()
    
    member __.SetPointFormat(pointFormat) =
        chart.chartData <- { chart.chartData with PointFormat = Some pointFormat }
        __.Navigate()

    member __.Navigate() =
        let js = compileFun chart.chartData
        let html = Html.highcharts js
        browser.NavigateToString html
        do chart.jsField <- js
        do chart.htmlField <- html

    member __.SetData (data:seq<#key*#value>) =
        let series = Series.New(chart.chartData.Type, data)
        chart.chartData <- { chart.chartData with Data = [|series|] }
        __.Navigate()

    member __.SetData (data:seq<#value>) =
        let series = Series.New(chart.chartData.Type, data)
        chart.chartData <- { chart.chartData with Data = [|series|] }
        __.Navigate()

    member __.SetData series =
        chart.chartData <- { chart.chartData with Data = [|series|] }
        __.Navigate()

    static member internal Create(chartData:ChartData, f: unit -> #GenericChart ) =
        let t = f()
        t.chartData <- chartData
        t.Navigate()
        t

    member __.Title = chart.chartData.Title

    member __.SetTitle title =
        chart.chartData <- { chart.chartData with Title = Some title }
        __.Navigate()

    member __.HideLegend() =
        chart.chartData <- { chart.chartData with Legend = false }
        __.Navigate()

    member __.ShowLegend() =
        chart.chartData <- { chart.chartData with Legend = true }
        __.Navigate()
        
    member __.XTitle = chart.chartData.XTitle
    
    member __.SetXTitle(title) =
        chart.chartData <- { chart.chartData with XTitle = Some title }
        __.Navigate()

    member __.YTitle = chart.chartData.YTitle

    member __.SetYTitle(title) =
        chart.chartData <- { chart.chartData with YTitle = Some title }
        __.Navigate()

type HighchartsArea() =
    inherit GenericChart()
    let mutable stacking = Stacking.Disabled
    let mutable inverted = false

    let compileJs (chartData:ChartData) =
        let chartType, data, title, pointFormat, legend, categories, xTitle, yTitle, subtitle =
            chartData.Type, chartData.Data, chartData.Title, chartData.PointFormat,
            chartData.Legend , chartData.Categories, chartData.XTitle,
            chartData.YTitle, chartData.Subtitle
        HighchartsJs.area data title legend categories xTitle yTitle pointFormat subtitle stacking inverted

    do base.JsFun <- compileJs

    member __.SetStacking x =
        stacking <- x
        base.Navigate()

    member __.Inverted
        with get() = inverted
        and set(x) =
            inverted <- x
            base.Navigate()

type HighchartsAreaspline() =
    inherit GenericChart()
    let mutable stacking = Stacking.Disabled
    let mutable inverted = false

    let compileJs (chartData:ChartData) =
        let chartType, data, title, pointFormat, legend, categories, xTitle, yTitle, subtitle =
            chartData.Type, chartData.Data, chartData.Title, chartData.PointFormat,
            chartData.Legend , chartData.Categories, chartData.XTitle,
            chartData.YTitle, chartData.Subtitle
        HighchartsJs.areaspline data title legend categories xTitle yTitle pointFormat subtitle stacking inverted

    do base.JsFun <- compileJs

    member __.SetStacking x =
        stacking <- x
        base.Navigate()

    member __.Inverted
        with get() = inverted
        and set(x) =
            inverted <- x
            base.Navigate()

type HighchartsArearange() =
    inherit GenericChart()

type HighchartsBar() =
    inherit GenericChart()
    let mutable stacking = Stacking.Disabled

    let compileJs (chartData:ChartData) =
        let chartType, data, title, pointFormat, legend, categories, xTitle, yTitle, subtitle =
            chartData.Type, chartData.Data, chartData.Title, chartData.PointFormat,
            chartData.Legend , chartData.Categories, chartData.XTitle,
            chartData.YTitle, chartData.Subtitle
        HighchartsJs.bar data title legend categories xTitle yTitle pointFormat subtitle stacking

    do base.JsFun <- compileJs

    member __.SetStacking x =
        stacking <- x
        base.Navigate()

type HighchartsBubble() =
    inherit GenericChart()

type HighchartsCombination() =
    inherit GenericChart()

type HighchartsColumn() =
    inherit GenericChart()

type HighchartsLine() =
    inherit GenericChart()

type HighchartsPie() =
    inherit GenericChart()

type HighchartsScatter() =
    inherit GenericChart()

let newChartData categories data legend pointFormat subtitle title chartType xTitle yTitle =
    let legend = defaultArg legend false
    let categories =
        match categories with 
        | None -> [||]
        | Some value -> Seq.toArray value
    ChartData.New categories data legend pointFormat subtitle title chartType xTitle yTitle

type Highcharts =

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Area xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = newChartData categories [|series|] legend None None title Area xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Area data
        let chartData = newChartData categories [|series|] legend None None title Area xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))
        
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
        let chartData = newChartData categories data legend None None title Area xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

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
        let chartData = newChartData categories data legend None None title Area xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Area xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

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
        let chartData = newChartData categories data legend None None title Area xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Areaspline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsAreaspline()))

    /// <summary>Creates an area chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Areaspline data
        let chartData = newChartData categories [|series|] legend None None title Areaspline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsAreaspline()))

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Areaspline data
        let chartData = newChartData categories [|series|] legend None None title Areaspline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsAreaspline()))
        
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
        let chartData = newChartData categories data legend None None title Areaspline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsAreaspline()))

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
        let chartData = newChartData categories data legend None None title Areaspline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsAreaspline()))

    /// <summary>Creates an areaspline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Areaspline(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Areaspline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsAreaspline()))

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
        let chartData = newChartData categories data legend None None title Areaspline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsAreaspline()))

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Arearange xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArearange()))

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Arearange data
        let chartData = newChartData categories [|series|] legend None None title Arearange xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArearange()))
        
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
        let chartData = newChartData categories data legend None None title Arearange xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArearange()))

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
        let chartData = newChartData categories data legend None None title Arearange xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArearange()))

    /// <summary>Creates an arearange chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Arearange(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Arearange xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArearange()))

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
        let chartData = newChartData categories data legend None None title Arearange xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsArearange()))

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBar()))

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bar data
        let chartData = newChartData categories [|series|] legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBar()))

    /// <summary>Creates a bar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bar data
        let chartData = newChartData categories [|series|] legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
        
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
        let chartData = newChartData categories data legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBar()))

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
        let chartData = newChartData categories data legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBar()))

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBar()))

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
        let chartData = newChartData categories data legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBar()))

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#key*#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bubble data
        let chartData = newChartData categories [|series|] legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<#value*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Bubble data
        let chartData = newChartData categories [|series|] legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

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
        let chartData = newChartData categories data legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
        
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
        let chartData = newChartData categories data legend None None title Bar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

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
        let chartData = newChartData categories data legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

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
        let chartData = newChartData categories data legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

    /// <summary>Creates a bubble chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Bubble(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

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
        let chartData = newChartData categories data legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

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
        let chartData = newChartData categories data legend None None title Bubble xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Column xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Column data
        let chartData = newChartData categories [|series|] legend None None title Column xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))

    /// <summary>Creates a column chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Column data
        let chartData = newChartData categories [|series|] legend None None title Column xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
        
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
        let chartData = newChartData categories data legend None None title Column xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))

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
        let chartData = newChartData categories data legend None None title Column xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Column(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Column xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))

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
        let chartData = newChartData categories data legend None None title Column xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))

    /// <summary>Creates a combination chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Combine(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories (Seq.toArray data) legend None None title Combination xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsCombination()))

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Line xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Line data
        let chartData = newChartData categories [|series|] legend None None title Line xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    /// <summary>Creates a line chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Line data
        let chartData = newChartData categories [|series|] legend None None title Line xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
        
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
        let chartData = newChartData categories data legend None None title Line xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

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
        let chartData = newChartData categories data legend None None title Line xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Line(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Line xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    /// <summary>Creates an line chart.</summary>
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
        let chartData = newChartData categories data legend None None title Line xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    /// <summary>Creates a pie chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Pie xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsPie()))

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Pie data
        let chartData = newChartData categories [|series|] legend None None title Pie xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsPie()))

    /// <summary>Creates a pie chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Pie(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Pie data
        let chartData = newChartData categories [|series|] legend None None title Pie xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsPie()))
        
    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Scatter xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Scatter data
        let chartData = newChartData categories [|series|] legend None None title Scatter xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Scatter data
        let chartData = newChartData categories [|series|] legend None None title Scatter xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))
        
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
        let chartData = newChartData categories data legend None None title Scatter xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))

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
        let chartData = newChartData categories data legend None None title Scatter xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))

    /// <summary>Creates a scatter chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Scatter(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Scatter xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))

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
        let chartData = newChartData categories data legend None None title Scatter xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))