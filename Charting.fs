module FsPlot.Charting

open Options
open DataSeries

type ChartData =
    {
        mutable Categories : string []
        mutable Data : Series []
        mutable Legend : bool
        mutable PointFormat : string option
        mutable Subtitle : string option
        mutable Title : string option
        Type : ChartType
        mutable XTitle : string option
        mutable YTitle : string option
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
    | Area -> HighchartsJs.area data title legend categories xTitle yTitle pointFormat subtitle Stacking.Disabled
//    | Bar -> HighchartsJs.bar data title legend categories xTitle yTitle
//    | Bubble -> Highcharts.Bubble.js data title legend categories xTitle yTitle
//    | Combination -> Highcharts.Combination.js data title legend categories xTitle yTitle
//    | Column -> Highcharts.Column.js data title legend categories xTitle yTitle
//    | Line -> Highcharts.Line.js data title legend categories xTitle yTitle
//    | Pie -> Highcharts.Pie.js data title legend categories xTitle yTitle
//    | Scatter -> Highcharts.Scatter.js data title legend categories xTitle yTitle
    | _ -> ""

type GenericChart() as chart =

    [<DefaultValue>] val mutable private chartData : ChartData
    [<DefaultValue>] val mutable private jsField : string
    [<DefaultValue>] val mutable private htmlField : string
    
    let mutable compileFun = compileJs
    
    let wnd, browser = ChartWindow.show()
    
    let navigate() =
        let js = compileFun chart.chartData
        let html = Html.highcharts js
        browser.NavigateToString html
        do chart.jsField <- js
        do chart.htmlField <- html

    member __.JsFun
        with set(f) = compileFun <- f

    member __.SetSubtite subtitle =
        chart.chartData.Subtitle <- Some subtitle
        navigate()

    member __.Categories
        with get() = chart.chartData.Categories |> Array.toSeq
        and set(categories) =
            chart.chartData.Categories <- Seq.toArray categories    
            navigate()
    
    member __.SetPointFormat(pointFormat) =
        chart.chartData.PointFormat <- Some pointFormat
        navigate()

    member __.Refresh() = navigate()

    member __.SetData (data:seq<#key*#value>) =
        let series = Series.New(chart.chartData.Type, data)
        chart.chartData.Data <- [|series|] 
        navigate()

    member __.SetData (data:seq<#value>) =
        let series = Series.New(chart.chartData.Type, data)
        chart.chartData.Data <- [|series|] 
        navigate()

    member __.SetData series =
        chart.chartData.Data <- [|series|] 
        navigate()

    static member internal Create(chartData:ChartData, f: unit -> #GenericChart ) =
        let t = f()
        t.chartData <- chartData
        t.Refresh()
        t

    member __.Title = chart.chartData.Title

    member __.SetTitle title =
        chart.chartData.Title <- Some title
        navigate()

    member __.HideLegend() =
        chart.chartData.Legend <- false
        navigate()

    member __.ShowLegend() =
        chart.chartData.Legend <- true
        navigate()
        
    member __.XTitle = chart.chartData.XTitle
    
    member __.SetXTitle(title) =
        chart.chartData.XTitle <- Some title
        navigate()

    member __.YTitle = chart.chartData.YTitle

    member __.SetYTitle(title) =
        chart.chartData.YTitle <- Some title
        navigate()

type HighchartsArea() =
    inherit GenericChart()
    let mutable stacking = Stacking.Disabled

    let compileJs (chartData:ChartData) =
        let chartType, data, title, pointFormat, legend, categories, xTitle, yTitle, subtitle =
            chartData.Type, chartData.Data, chartData.Title, chartData.PointFormat,
            chartData.Legend , chartData.Categories, chartData.XTitle,
            chartData.YTitle, chartData.Subtitle
        HighchartsJs.area data title legend categories xTitle yTitle pointFormat subtitle stacking

    do base.JsFun <- compileJs

    member __.SetStacking x =
        stacking <- x
        base.Refresh()

//    areaChart    

//    __.Stacking
//        with get() = stacking
//        and set(stacking) = 
        
         
type HighchartsBar() =
    inherit GenericChart()

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

//    /// <summary>Creates a bar chart.</summary>
//    /// <param name="series">The chart's data.</param>
//    /// <param name="categories">The X-axis categories.</param>
//    /// <param name="legend">Whether to display a legend or not.</param>
//    /// <param name="title">The chart's title.</param>
//    /// <param name="xTitle">The X-axis title.</param>
//    /// <param name="yTitle">The Y-axis title.</param>
//    static member Bar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
//        let chartData = newChartData categories [|data|] legend title Bar xTitle yTitle
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    /// <summary>Creates a bar chart.</summary>
//    /// <param name="data">The chart's data.</param>
//    /// <param name="categories">The X-axis categories.</param>
//    /// <param name="legend">Whether to display a legend or not.</param>
//    /// <param name="title">The chart's title.</param>
//    /// <param name="xTitle">The X-axis title.</param>
//    /// <param name="yTitle">The Y-axis title.</param>
//    static member Bar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
//        let series = Series.Bar data
//        let chartData = newChartData categories [|series|] legend title Bar xTitle yTitle
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    /// <summary>Creates a bar chart.</summary>
//    /// <param name="data">The chart's data.</param>
//    /// <param name="categories">The X-axis categories.</param>
//    /// <param name="legend">Whether to display a legend or not.</param>
//    /// <param name="title">The chart's title.</param>
//    /// <param name="xTitle">The X-axis title.</param>
//    /// <param name="yTitle">The Y-axis title.</param>
//    static member Bar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
//        let series = Series.Bar data
//        let chartData = newChartData categories [|series|] legend title Bar xTitle yTitle
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//        
//    /// <summary>Creates a bar chart.</summary>
//    /// <param name="series">The chart's data.</param>
//    /// <param name="categories">The X-axis categories.</param>
//    /// <param name="legend">Whether to display a legend or not.</param>
//    /// <param name="title">The chart's title.</param>
//    /// <param name="xTitle">The X-axis title.</param>
//    /// <param name="yTitle">The Y-axis title.</param>
//    static member Bar(data:seq<(#key*#value) list>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
//        let data =
//            data
//            |> Seq.map Series.Bar
//            |> Seq.toArray
//        let chartData = newChartData categories data legend title Bar xTitle yTitle
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    /// <summary>Creates a bar chart.</summary>
//    /// <param name="series">The chart's data.</param>
//    /// <param name="categories">The X-axis categories.</param>
//    /// <param name="legend">Whether to display a legend or not.</param>
//    /// <param name="title">The chart's title.</param>
//    /// <param name="xTitle">The X-axis title.</param>
//    /// <param name="yTitle">The Y-axis title.</param>
//    static member Bar(data:seq<(#key*#value) []>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
//        let data =
//            data
//            |> Seq.map Series.Bar
//            |> Seq.toArray
//        let chartData = newChartData categories data legend title Bar xTitle yTitle
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    /// <summary>Creates a bar chart.</summary>
//    /// <param name="series">The chart's data.</param>
//    /// <param name="categories">The X-axis categories.</param>
//    /// <param name="legend">Whether to display a legend or not.</param>
//    /// <param name="title">The chart's title.</param>
//    /// <param name="xTitle">The X-axis title.</param>
//    /// <param name="yTitle">The Y-axis title.</param>
//    static member Bar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
//        let data = Seq.toArray data
//        let chartData = newChartData categories data legend title Bar xTitle yTitle
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    /// <summary>Creates a bar chart.</summary>
//    /// <param name="series">The chart's data.</param>
//    /// <param name="categories">The X-axis categories.</param>
//    /// <param name="legend">Whether to display a legend or not.</param>
//    /// <param name="title">The chart's title.</param>
//    /// <param name="xTitle">The X-axis title.</param>
//    /// <param name="yTitle">The Y-axis title.</param>
//    static member Bar(data:seq<seq<#key*#value>>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
//        let data =
//            data
//            |> Seq.map Series.Bar
//            |> Seq.toArray
//        let chartData = newChartData categories data legend title Bar xTitle yTitle
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
















//    static member Combine(data:seq<Series>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
////        let series = Series.New("", Pie, data)
//        let chartData =
//            {
//                Data = Seq.toArray data
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Combination
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsCombination()))
//
//    static member Pie(data:seq<#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.New("", Pie, data)
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Pie
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsPie()))
//    
//    static member Pie(data:seq<#key*#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.Pie "" data
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Pie
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsPie()))
//
//    static member Pie(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Pie
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsPie()))
//
//
//    static member Line(data:seq<#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.New("", Line, data)
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Line
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
//
//    static member Line(data:seq<#key*#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.Line "" data
//        let ty = Seq.head data |> snd
//        let ty = ty.GetTypeCode()
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Line
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
//
//    static member Line(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Line
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
//
//    static member Line(data:seq<seq<#key*#value>>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Line "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Line
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
//
//    static member Line(data:seq<(#key*#value) list>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Line "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Line
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
//
//    static member Line(data:seq<(#key*#value) []>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Line "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Line
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
//
//    static member Line(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = series |> Seq.toArray
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Line
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsLine()))
//
//    static member Bar(data:seq<#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.New("", Bar, data)
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Bar
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    static member Bar(data:seq<#key*#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.Bar "" data
//        let ty = Seq.head data |> snd
//        let ty = ty.GetTypeCode()
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bar
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    static member Bar(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bar
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    static member Bar(data:seq<seq<#key*#value>>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Bar "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bar
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    static member Bar(data:seq<(#key*#value) list>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Bar "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bar
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    static member Bar(data:seq<(#key*#value) []>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Bar "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bar
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    static member Bar(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = series |> Seq.toArray
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bar
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBar()))
//
//    static member Bubble(data:seq<#key*#value*#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.Bubble "" data
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bubble
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
//
//    static member Bubble(data:seq<#value*#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.NewBubble("", data)
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bubble
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
//
//    static member Bubble(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bubble
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
//
//    static member Bubble(data:seq<seq<#value*#value>>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.NewBubble("", x))
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bubble
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
//
//    static member Bubble(data:seq<(#value*#value) list>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.NewBubble("", x))
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bubble
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
//
//    static member Bubble(data:seq<(#value*#value) []>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.NewBubble("", x))
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bubble
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
//
//    static member Bubble(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = series |> Seq.toArray
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Bubble
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsBubble()))
//
//    static member Column(data:seq<#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.New("", Column, data)
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Column
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
//
//    static member Column(data:seq<#key*#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.Column "" data
//        let ty = Seq.head data |> snd
//        let ty = ty.GetTypeCode()
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//
//                Type = Column
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
//
//    static member Column(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Column
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
//
//    static member Column(data:seq<seq<#key*#value>>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Column "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Column
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
//
//    static member Column(data:seq<(#key*#value) list>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Column "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Column
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
//
//    static member Column(data:seq<(#key*#value) []>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Column "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Column
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
//
//    static member Column(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = series |> Seq.toArray
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Column
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsColumn()))
//
//    static member Scatter(data:seq<#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.New("", Scatter, data)
//        let chartData =
//            {
//                Data = [|series|]
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Scatter
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))
//
//    static member Scatter(data:seq<#key*#value>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let series = Series.Scatter "" data
//        let ty = Seq.head data |> snd
//        let ty = ty.GetTypeCode()
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Scatter
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))
//
//    static member Scatter(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = [|series|]
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Scatter
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))
//
//    static member Scatter(data:seq<seq<#key*#value>>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Scatter "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Scatter
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))
//
//    static member Scatter(data:seq<(#key*#value) list>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Scatter "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title=chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Scatter
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))
//
//    static member Scatter(data:seq<(#key*#value) []>, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let dataSeries =
//            data
//            |> Seq.map (fun x -> Series.Scatter "" x)
//            |> Seq.toArray
//        let chartData =
//            {
//                Data = dataSeries
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Scatter
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))
//
//    static member Scatter(series, ?chartTitle, ?legend, ?categories, ?xTitle, ?yTitle) =
//        let chartData =
//            {
//                Data = series |> Seq.toArray
//                Title = chartTitle
//                Legend = defaultArg legend false
//                Categories = match categories with None -> [||] | Some value -> Seq.toArray value
//                XTitle = xTitle
//                YTitle = yTitle
//                Type = Scatter
//            }
//        GenericChart.Create(chartData, (fun () -> HighchartsScatter()))