module FsPlot.GenericChart

open DataSeries
open HighchartsOptions

type ChartData =
    {
        Categories : string []
        Data : Series []
        Legend : bool
        Subtitle : string option
        Title : string option
        Tooltip : string option
        Type : ChartType
        XTitle : string option
        YTitle : string option
    }

    static member New a b c d e f g h i=
        {
            Categories = a
            Data = b
            Legend = c
            Subtitle = d
            Title = e
            Tooltip = f
            Type = g
            XTitle = h
            YTitle = i
        }

    member __.Fields =
        __.Categories,
        __.Data,
        __.Legend,
        __.Subtitle,
        __.Title,
        __.Tooltip,
        __.Type,
        __.XTitle,
        __.YTitle

module private Js =

    let highcharts (x:ChartData) =
        let a, b, c, d, e, f, g, h, i = x.Fields
        match g with
        | Area -> HighchartsJs.area b e c a h i f d Disabled false
        | Areaspline -> HighchartsJs.areaspline b e c a h i f d Disabled false
        | Arearange -> HighchartsJs.arearange b e c a h i f d
        | Bar -> HighchartsJs.bar b e c a h i f d Disabled
        | Bubble -> HighchartsJs.bubble b e c a h i f d
        | Column -> HighchartsJs.column b e c a h i f d Disabled
        | Combination -> HighchartsJs.combine b e c a h i f d None
        | Donut -> HighchartsJs.donut b e c a h i f d
        | Funnel -> HighchartsJs.funnel b e c a h i f d
        | Line -> HighchartsJs.line b e c a h i f d
        | PercentArea -> HighchartsJs.percentArea b e c a h i f d false
        | PercentBar -> HighchartsJs.percentBar b e c a h i f d
        | PercentColumn -> HighchartsJs.percentColumn b e c a h i f d
        | Pie -> HighchartsJs.pie b e c a h i f d
        | Radar -> HighchartsJs.radar b e c a h i f d
        | Scatter -> HighchartsJs.scatter b e c a h i f d
        | Spline -> HighchartsJs.spline b e c a h i f d
        | StackedArea -> HighchartsJs.stackedArea b e c a h i f d false
        | StackedBar -> HighchartsJs.stackedBar b e c a h i f d
        | StackedColumn -> HighchartsJs.stackedColumn b e c a h i f d

module private Html =
    
    let highcharts chartType =
        match chartType with
        | Arearange | Bubble | Radar -> HighchartsHtml.more
        | Combination -> HighchartsHtml.combine
        | Funnel -> HighchartsHtml.funnel
        | _ -> HighchartsHtml.common

type GenericChart() as chart =
    
    [<DefaultValue>] val mutable private chartData : ChartData    

    let mutable jsFun = Js.highcharts    
    let htmlFun = Html.highcharts

    let wnd, browser = ChartWindow.show()

    let ctx = System.Threading.SynchronizationContext.Current

    let agent =
        MailboxProcessor<ChartData>.Start(fun inbox ->
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
                                do! Async.SwitchToContext ctx
                                browser.NavigateToString html
                                return! loop()
                            | _ -> return! loop()
                        | _ -> return! loop()
                    | _ -> return! loop()
                }
            loop())

    /// <summary>Closes the chart's window.</summary>
    member __.Close() = wnd.Close()

    static member internal Create x (f:unit -> #GenericChart) =
        let gc = f()
        gc.SetChartData  x
        gc

    /// <summary>Hides the legend of a chart.</summary>
    member __.HideLegend() =
        chart.chartData <- { chart.chartData with Legend = false }
        agent.Post chart.chartData

    member internal __.Navigate() = agent.Post chart.chartData

    /// <summary>Sets the categories of a chart's X-axis.</summary>
    member __.SetCategories(categories) =
        chart.chartData <- { chart.chartData with Categories = Seq.toArray categories}
        agent.Post chart.chartData

    member internal __.SetChartData chartData = 
        chart.chartData <- chartData
        agent.Post chart.chartData

    /// <summary>Sets the data series used by a chart.</summary>
    member __.SetData series =
        chart.chartData <- { chart.chartData with Data = [|series|] }
        agent.Post chart.chartData

    /// <summary>Sets the data series used by a chart.</summary>
    member __.SetData (data:seq<Series>) =
        let series = Seq.toArray data
        chart.chartData <- { chart.chartData with Data = series }
        agent.Post chart.chartData

    member internal __.SetJsFun(f) = jsFun <- f

    /// <summary>Modifies the tooltip format for each data point.</summary>
    member __.SetTooltip(tooltip) =
        chart.chartData <- { chart.chartData with Tooltip = Some tooltip }
        agent.Post chart.chartData

    /// <summary>Sets the chart's subtitle.</summary>
    member __.SetSubtitle subtitle =
        chart.chartData <- { chart.chartData with Subtitle = Some subtitle }
        agent.Post chart.chartData

    /// <summary>Sets the chart's title.</summary>
    member __.SetTitle title =
        chart.chartData <- { chart.chartData with Title = Some title }
        agent.Post chart.chartData

    /// <summary>Sets the chart's X-axis title.</summary>
    member __.SetXTitle(title) =
        chart.chartData <- { chart.chartData with XTitle = Some title }
        agent.Post chart.chartData

    /// <summary>Sets the chart's Y-axis title.</summary>
    member __.SetYTitle(title) =
        chart.chartData <- { chart.chartData with YTitle = Some title }
        agent.Post chart.chartData

    /// <summary>Displays the legend of a chart.</summary>
    member __.ShowLegend() =
        chart.chartData <- { chart.chartData with Legend = true }
        agent.Post chart.chartData