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

    static member New a b c d e f g h i=
        {
            Categories = a
            Data = b
            Legend = c
            PointFormat = d
            Subtitle = e
            Title = f
            Type = g
            XTitle = h
            YTitle = i
        }

    member __.Fields =
        __.Categories,
        __.Data,
        __.Legend,
        __.PointFormat,
        __.Subtitle,
        __.Title,
        __.Type,
        __.XTitle,
        __.YTitle

let private compileJs (x:ChartData) =
    let a, b, c, d, e, f, g, h, i = x.Fields
    match g with
    | Area -> HighchartsJs.area b f c a h i d e Disabled false
    | Areaspline -> HighchartsJs.areaspline b f c a h i d e Disabled false
    | Arearange -> HighchartsJs.arearange b f c a h i d e
    | Bar -> HighchartsJs.bar b f c a h i d e Disabled
    | Bubble -> HighchartsJs.bubble b f c a h i d e
    | Column -> HighchartsJs.column b f c a h i d e Disabled
    | Combination -> HighchartsJs.combine b f c a h i d e None
    | Donut -> HighchartsJs.donut b f c a h i d e
    | Funnel -> HighchartsJs.funnel b f c a h i d e
    | Line -> HighchartsJs.line b f c a h i d e
    | Pie -> HighchartsJs.pie b f c a h i d e
    | Radar -> HighchartsJs.radar b f c a h i d e
    | Scatter -> HighchartsJs.scatter b f c a h i d e
    | Spline -> HighchartsJs.spline b f c a h i d e

type GenericChart() as chart =
    
    [<DefaultValue>] val mutable private chartData : ChartData    
    let mutable compileFun = compileJs
    let mutable htmlFun = Html.highcharts

    let wnd, browser = ChartWindow.show()

    let ctx = System.Threading.SynchronizationContext.Current

    let agent =
        MailboxProcessor<ChartData>.Start(fun inbox ->
            let rec loop() =
                async {
                    let! msg = inbox.Receive()
                    match inbox.CurrentQueueLength with
                    | 0 ->
                        let js = compileFun msg
                        match inbox.CurrentQueueLength with
                        | 0 ->
                            let html = htmlFun js
                            match inbox.CurrentQueueLength with
                            | 0 ->
                                do! Async.SwitchToContext ctx
                                browser.NavigateToString html
                                return! loop()
                            | _ -> return! loop()
                        | _ -> return! loop()
                    | _ -> return! loop()
                }
            loop())

    member __.Close() = wnd.Close()

    static member internal Create(x:ChartData, f:unit -> #GenericChart) =
        let gc = f()
        gc.chartData <- x
        match x.Type with
        | Arearange | Bubble | Radar -> gc.SetHtmlFun Html.highchartsMore
        | Combination -> gc.SetHtmlFun Html.highchartsCombine
        | Funnel -> gc.SetHtmlFun Html.highchartsFunnel
        | _ -> ()
        gc.Navigate()
        gc

//    member __.GetCategories() = chart.chartData.Categories |> Array.toSeq
//
//    member __.GetTitle() = chart.chartData.Title
//
//    member __.GetXTitle() = chart.chartData.XTitle
//
//    member __.GetYTitle() = chart.chartData.YTitle

    member __.HideLegend() =
        chart.chartData <- { chart.chartData with Legend = false }
        __.Navigate()

    member internal __.Navigate() = agent.Post chart.chartData

    member __.SetCategories(categories) =
        chart.chartData <- { chart.chartData with Categories = Seq.toArray categories}
        __.Navigate()

    member __.SetData series =
        chart.chartData <- { chart.chartData with Data = [|series|] }
        __.Navigate()

    member __.SetData (data:seq<Series>) =
        let series = Seq.toArray data
        chart.chartData <- { chart.chartData with Data = series }
        __.Navigate()

    member internal __.SetHtmlFun f = htmlFun <- f

    member internal __.SetJsFun(f) = compileFun <- f

    member __.SetTooltip(format) =
        chart.chartData <- { chart.chartData with PointFormat = Some format }
        __.Navigate()

    member __.SetSubtitle subtitle =
        chart.chartData <- { chart.chartData with Subtitle = Some subtitle }
        __.Navigate()

    member __.SetTitle title =
        chart.chartData <- { chart.chartData with Title = Some title }
        __.Navigate()

    member __.SetXTitle(title) =
        chart.chartData <- { chart.chartData with XTitle = Some title }
        __.Navigate()

    member __.SetYTitle(title) =
        chart.chartData <- { chart.chartData with YTitle = Some title }
        __.Navigate()

    member __.ShowLegend() =
        chart.chartData <- { chart.chartData with Legend = true }
        __.Navigate()

type HighchartsArea() =
    inherit GenericChart()

    let mutable stacking = Disabled
    let mutable inverted = false

    let compileJs (x:ChartData) =
        let a, b, c, d, e, f, _, h, i = x.Fields
        HighchartsJs.area b f c a h i d e stacking inverted

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
        let a, b, c, d, e, f, _, h, i = x.Fields
        HighchartsJs.areaspline b f c a h i d e stacking inverted

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
        let a, b, c, d, e, f, _, h, i = x.Fields
        HighchartsJs.bar b f c a h i d e stacking

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
        let a, b, c, d, e, f, _, h, i = x.Fields
        HighchartsJs.column b f c a h i d e stacking

    do base.SetJsFun compileJs

    member __.SetStacking(x) =
        stacking <- x
        base.Navigate()

type HighchartsCombination() =
    inherit GenericChart()

    let mutable pieOptions = None

    let compileJs (x:ChartData) =
        let a, b, c, d, e, f, _, h, i = x.Fields
        HighchartsJs.combine b f c a h i d e pieOptions

    do base.SetJsFun compileJs

    member __.SetPieOptions x =
        pieOptions <- Some x
        base.Navigate()

type HighchartsDonut() =
    inherit GenericChart()

type HighchartsFunnel() =
    inherit GenericChart()

type HighchartsLine() =
    inherit GenericChart()

type HighchartsPie() =
    inherit GenericChart()

type HighchartsRadar() =
    inherit GenericChart()

type HighchartsScatter() =
    inherit GenericChart()

type HighchartsSpline() =
    inherit GenericChart()

let private newChartData a b c d e f g h i =
    let c' = defaultArg c false
    let a' =
        match a with 
        | None -> [||]
        | Some value -> Seq.toArray value
    ChartData.New a' b c' d e f g h i

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

    /// <summary>Creates a donut chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Donut xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsDonut()))

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Donut data
        let chartData = newChartData categories [|series|] legend None None title Donut xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsDonut()))

    /// <summary>Creates a donut chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Donut(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Donut data
        let chartData = newChartData categories [|series|] legend None None title Donut xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsDonut()))

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Funnel xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsFunnel()))

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Funnel data
        let chartData = newChartData categories [|series|] legend None None title Funnel xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsFunnel()))

    /// <summary>Creates a funnel chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Funnel(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Funnel data
        let chartData = newChartData categories [|series|] legend None None title Funnel xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsFunnel()))

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

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Radar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsRadar()))

    /// <summary>Creates a radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Radar data
        let chartData = newChartData categories [|series|] legend None None title Radar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsRadar()))

    /// <summary>Creates a Radar chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Radar data
        let chartData = newChartData categories [|series|] legend None None title Radar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsRadar()))
        
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
        let chartData = newChartData categories data legend None None title Radar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsRadar()))

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
        let chartData = newChartData categories data legend None None title Radar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsRadar()))

    /// <summary>Creates a radar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Radar(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Radar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsRadar()))

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
        let chartData = newChartData categories data legend None None title Radar xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsRadar()))
        
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

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartData categories [|data|] legend None None title Spline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsSpline()))

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Spline data
        let chartData = newChartData categories [|series|] legend None None title Spline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsSpline()))

    /// <summary>Creates a spline chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<#key*#value>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let series = Series.Spline data
        let chartData = newChartData categories [|series|] legend None None title Spline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsSpline()))
        
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
        let chartData = newChartData categories data legend None None title Spline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsSpline()))

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
        let chartData = newChartData categories data legend None None title Spline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsSpline()))

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Spline(data:seq<Series>, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let data = Seq.toArray data
        let chartData = newChartData categories data legend None None title Spline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsSpline()))

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
        let chartData = newChartData categories data legend None None title Spline xTitle yTitle
        GenericChart.Create(chartData, (fun () -> HighchartsSpline()))


type Chart =

    static member categories categories (chart:#GenericChart) =
        chart.SetCategories categories
        chart

    static member close (chart:#GenericChart) = chart.Close()

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
        | Pie -> Highcharts.Pie series :> GenericChart
        | Radar -> Highcharts.Radar series :> GenericChart
        | Scatter -> Highcharts.Scatter series :> GenericChart
        | _ -> Highcharts.Spline series :> GenericChart

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
            | Line -> Highcharts.Line series :> GenericChart
            | Radar -> Highcharts.Radar series :> GenericChart
            | Scatter -> Highcharts.Scatter series :> GenericChart
            | _ -> Highcharts.Spline series :> GenericChart
        | _ -> Highcharts.Combine series :> GenericChart

    static member showLegend (chart:#GenericChart) =
        chart.ShowLegend()
        chart

    static member subtitle subtitle (chart:#GenericChart) =
        chart.SetSubtitle subtitle
        chart

    static member title title (chart:#GenericChart) =
        chart.SetTitle title
        chart

    static member tooltip format (chart:#GenericChart) =
        chart.SetTooltip format
        chart

    static member xTitle xTitle (chart:#GenericChart) =
        chart.SetXTitle xTitle
        chart

    static member yTitle yTitle (chart:#GenericChart) =
        chart.SetYTitle yTitle
        chart