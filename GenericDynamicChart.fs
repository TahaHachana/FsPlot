module FsPlot.GenericDynamicChart
 
open System
open System.Diagnostics
open Owin
open Microsoft.AspNet.SignalR
open Microsoft.Owin.Hosting 
open FsPlot.Dynamic
open FsPlot.Config
open FsPlot.Data
open FsPlot.Highcharts
  
let freePort() =
    let properties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()

    let tcpEndPoints =
        properties.GetActiveTcpListeners()
        |> Array.map (fun x -> x.Port)

    let random = Random()

    let rec port() =
        let rnd = random.Next(1000, 50000)    
        let isActive = Array.exists (fun x -> x = rnd) tcpEndPoints
        match isActive with
        | false -> string rnd
        | true -> port()
    
    port()

type Startup() =
    
    member __.Configuration(app:IAppBuilder) =
        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll) |> ignore
        app.MapSignalR() |> ignore

let guidsList = System.Collections.Generic.List<string*string>()

let remove guid =
    guidsList.RemoveAll(fun (chartGuid, _) -> chartGuid = guid)
    |> ignore

type DataHub() =
    inherit Hub()

    member x.StoreGuids(chartGuid, proxyGuid) : unit =
        guidsList.Add(chartGuid, proxyGuid)

let hub = GlobalHost.ConnectionManager.GetHubContext<DataHub>()

module Html =
    
    let highcharts chartType address js =
        match chartType with
        | Arearange | Bubble | Radar -> Html.dynamicMore address js
//        | Combination -> Html.combine
//        | Funnel -> Html.funnel
        | _ -> Html.dynamicCommon address js

module Js =

    let highcharts address guid shift (config:ChartConfig) =
        match config.Type with
        | Area -> Js.dynamicArea address guid shift config
        | Areaspline -> Js.dynamicAreaspline address guid shift config
        | Arearange-> Js.dynamicArearange address guid shift config
        | Bar -> Js.dynamicBar address guid shift config
        | Bubble -> Js.dynamicBubble address guid shift config
        | _ -> Js.dynamicColumn address guid shift config

type GenericDynamicChart() as chart =

    [<DefaultValue>] val mutable private chartData : ChartConfig    
    let mutable shiftField = true
    let address = "http://localhost:" + freePort()
    let guid = Guid.NewGuid().ToString()
    let app = WebApp.Start<Startup> address
    let browser = Browser.start()
    let fileName = System.IO.Path.GetTempPath() + guid + ".html"

    let agent =
        MailboxProcessor<ChartConfig>.Start(fun inbox ->
            let rec loop() =
                async {
                    let! msg = inbox.Receive()
                    match inbox.CurrentQueueLength with
                    | 0 ->
                        let js = Js.highcharts address guid shiftField msg
                        match inbox.CurrentQueueLength with
                        | 0 ->
                            let html = Html.highcharts msg.Type address js
                            match inbox.CurrentQueueLength with
                            | 0 ->
                                System.IO.File.WriteAllText(fileName, html)
                                browser.Url <- fileName
                                return! loop()
                            | _ -> return! loop()
                        | _ -> return! loop()
                    | _ -> return! loop()
                }
            loop())

    /// <summary>Closes the browser window.</summary>
    member __.Close() =
        try
            browser.ExecuteScript("$.connection.hub.stop()") |> ignore
//            app.Dispose()
            browser.Quit()
            remove guid
        with _ -> ()

    static member internal Create config shift =
        let gdc = GenericDynamicChart()
        gdc.SetChartConfig  config
        match shift with true -> () | false -> gdc.SetShift shift
        gdc

    /// <summary>Hides the legend of a chart.</summary>
    member __.HideLegend() =
        remove guid
        chart.chartData <- { chart.chartData with Legend = false }
        agent.Post chart.chartData

    /// <summary>Adds a new data point to the chart.</summary>
    member __.Push(value:#value) =
        try
            let id = guidsList |> Seq.find (fun x -> fst x = guid) |> snd
//            let hub = GlobalHost.ConnectionManager.GetHubContext<DataHub>()
            hub.Clients.Client(id)?push value |> ignore
        with _ -> ()

    /// <summary>Adds a new data point to the chart.</summary>
    member __.Push(key, value) =
        let keyType = (key :> key).GetTypeCode()
        let key' = Utils.utcIfDatetime keyType key
        let value' = value :> value
        try
            let id = guidsList |> Seq.find (fun x -> fst x = guid) |> snd
//            let hub = GlobalHost.ConnectionManager.GetHubContext<DataHub>()
            hub.Clients.Client(id)?push [|key'; value'|] |> ignore
        with _ -> ()

    /// <summary>Adds a new data point to the chart.</summary>
    member __.Push(key, value, value') =
        let keyType = (key :> key).GetTypeCode()
        let key' = Utils.utcIfDatetime keyType key
        let value1' = value :> value
        let value2' = value' :> value
        try
            let id = guidsList |> Seq.find (fun x -> fst x = guid) |> snd
//            let hub = GlobalHost.ConnectionManager.GetHubContext<DataHub>()
            hub.Clients.Client(id)?push [|key'; value1'; value2'|] |> ignore
        with _ -> ()

    member internal __.SetChartConfig chartData = 
        chart.chartData <- chartData
        agent.Post chart.chartData

    /// <summary>Sets the categories of a chart's X-axis.</summary>
    member __.SetCategories(categories) =
        remove guid
        chart.chartData <- { chart.chartData with Categories = Seq.toArray categories}
        agent.Post chart.chartData

    /// <summary>Sets the data series used by a chart.</summary>
    member __.SetData series =
        remove guid
        chart.chartData <- { chart.chartData with Data = [|series|] }
        agent.Post chart.chartData

    /// <summary>Sets the shift property that determines whether one point is shifted off the start of the series as one is appended to the end.</summary>
    member __.SetShift shift =
        remove guid
        shiftField <- shift
        agent.Post chart.chartData

    /// <summary>Modifies the tooltip format for each data point.</summary>
    member __.SetTooltip(tooltip) =
        remove guid
        chart.chartData <- { chart.chartData with Tooltip = Some tooltip }
        agent.Post chart.chartData

    /// <summary>Sets the chart's subtitle.</summary>
    member __.SetSubtitle subtitle =
        remove guid
        chart.chartData <- { chart.chartData with Subtitle = Some subtitle }
        agent.Post chart.chartData

    /// <summary>Sets the chart's title.</summary>
    member __.SetTitle title =
        remove guid
        chart.chartData <- { chart.chartData with Title = Some title }
        agent.Post chart.chartData

    /// <summary>Sets the chart's X-axis title.</summary>
    member __.SetXTitle(title) =
        remove guid
        chart.chartData <- { chart.chartData with XTitle = Some title }
        agent.Post chart.chartData

    /// <summary>Sets the chart's Y-axis title.</summary>
    member __.SetYTitle(title) =
        remove guid
        chart.chartData <- { chart.chartData with YTitle = Some title }
        agent.Post chart.chartData

    /// <summary>Displays the legend of a chart.</summary>
    member __.ShowLegend() =
        remove guid
        chart.chartData <- { chart.chartData with Legend = true }
        agent.Post chart.chartData