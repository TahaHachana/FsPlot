module FsPlot.GenericChart

open FsPlot.Config
open FsPlot.Data
open FsPlot.Highcharts.Options
open FsPlot.Highcharts

module private Js =

    let highcharts (config:ChartConfig) =
        match config.Type with
        | Area -> Js.area config Disabled false
        | Areaspline -> Js.areaspline config Disabled false
        | Arearange -> Js.arearange config
        | Bar -> Js.bar config Disabled
        | Bubble -> Js.bubble config
        | Column -> Js.column config Disabled
        | Combination -> Js.combine config None
        | Donut -> Js.donut config
        | Funnel -> Js.funnel config
        | Line -> Js.line config
        | PercentArea -> Js.percentArea config false
        | PercentBar -> Js.percentBar config
        | PercentColumn -> Js.percentColumn config
        | Pie -> Js.pie config
        | Radar -> Js.radar config
        | Scatter -> Js.scatter config
        | Spline -> Js.spline config
        | StackedArea -> Js.stackedArea config false
        | StackedBar -> Js.stackedBar config
        | StackedColumn -> Js.stackedColumn config

module private Html =
    
    let highcharts chartType =
        match chartType with
        | Arearange | Bubble | Radar -> Html.more
        | Combination -> Html.combine
        | Funnel -> Html.funnel
        | _ -> Html.common

type GenericChart() as chart =
    
    [<DefaultValue>] val mutable private chartData : ChartConfig    

    let mutable jsFun = Js.highcharts    
    let htmlFun = Html.highcharts

    let wnd, browser = ChartWindow.show()

    let ctx = System.Threading.SynchronizationContext.Current

    let agent =
        MailboxProcessor<ChartConfig>.Start(fun inbox ->
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
        gc.SetChartConfig  x
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

    member internal __.SetChartConfig chartData = 
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