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

    static member New categories data legend subtitle title tooltip chartType xTitle yTitle =
        {
            Categories = categories
            Data = data
            Legend = legend
            Subtitle = subtitle
            Title = title
            Tooltip = tooltip
            Type = chartType
            XTitle = xTitle
            YTitle = yTitle
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
        let categories, series, legend, subtitle, title, tooltip, chartType, xTitle, yTitle = x.Fields
        match chartType with
        | Area -> HighchartsJs.area series title legend categories xTitle yTitle tooltip subtitle Disabled false
        | Areaspline -> HighchartsJs.areaspline series title legend categories xTitle yTitle tooltip subtitle Disabled false
        | Arearange -> HighchartsJs.arearange series title legend categories xTitle yTitle tooltip subtitle
        | Bar -> HighchartsJs.bar series title legend categories xTitle yTitle tooltip subtitle Disabled
        | Bubble -> HighchartsJs.bubble series title legend categories xTitle yTitle tooltip subtitle
        | Column -> HighchartsJs.column series title legend categories xTitle yTitle tooltip subtitle Disabled
        | Combination -> HighchartsJs.combine series title legend categories xTitle yTitle tooltip subtitle None
        | Donut -> HighchartsJs.donut series title legend categories xTitle yTitle tooltip subtitle
        | Funnel -> HighchartsJs.funnel series title legend categories xTitle yTitle tooltip subtitle
        | Line -> HighchartsJs.line series title legend categories xTitle yTitle tooltip subtitle
        | PercentArea -> HighchartsJs.percentArea series title legend categories xTitle yTitle tooltip subtitle false
        | PercentBar -> HighchartsJs.percentBar series title legend categories xTitle yTitle tooltip subtitle
        | PercentColumn -> HighchartsJs.percentColumn series title legend categories xTitle yTitle tooltip subtitle
        | Pie -> HighchartsJs.pie series title legend categories xTitle yTitle tooltip subtitle
        | Radar -> HighchartsJs.radar series title legend categories xTitle yTitle tooltip subtitle
        | Scatter -> HighchartsJs.scatter series title legend categories xTitle yTitle tooltip subtitle
        | Spline -> HighchartsJs.spline series title legend categories xTitle yTitle tooltip subtitle
        | StackedArea -> HighchartsJs.stackedArea series title legend categories xTitle yTitle tooltip subtitle false
        | StackedBar -> HighchartsJs.stackedBar series title legend categories xTitle yTitle tooltip subtitle
        | StackedColumn -> HighchartsJs.stackedColumn series title legend categories xTitle yTitle tooltip subtitle

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