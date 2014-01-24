module FsPlot.GenericDynamicChart

open FsPlot.Dynamic
open FsPlot.Config
open FsPlot.Data
open Owin
open Microsoft.AspNet.SignalR
open Microsoft.Owin.Hosting 
open System
open System.IO
open System.Net

module private Utils =

    let random = Random()
  
    let freePort() =
        let properties = NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()

        let tcpEndPoints =
            properties.GetActiveTcpListeners()
            |> Array.map (fun x -> x.Port)

        let rec port() =
            let rnd = random.Next(1000, 50000)    
            let isActive = Array.exists (fun x -> x = rnd) tcpEndPoints
            match isActive with
            | false -> string rnd
            | true -> port()
    
        port()

    let guidsList = System.Collections.Generic.List<string*string>()

    let remove guid =
        guidsList.RemoveAll(fun (chartGuid, _) -> chartGuid = guid)
        |> ignore

open Utils

type Startup() =
    
    member __.Configuration(app:IAppBuilder) =
        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll) |> ignore
        app.MapSignalR() |> ignore

type DataHub() =
    inherit Hub()

    member x.StoreGuids(chartGuid, proxyGuid) : unit =
        guidsList.Add(chartGuid, proxyGuid)

let dataHub = GlobalHost.ConnectionManager.GetHubContext<DataHub>()

type GenericDynamicChart() as chart =

    [<DefaultValue>] val mutable private chartData : ChartConfig    
    let mutable shiftField = false
    let address = "http://localhost:" + freePort()
    let guid = Guid.NewGuid().ToString()
    let app = WebApp.Start<Startup> address
    let browser = Browser.start()
    let htmlFile = Path.GetTempPath() + guid + ".html"

    let agent =
        MailboxProcessor<ChartConfig>.Start(fun inbox ->
            let rec loop() =
                async {
                    let! msg = inbox.Receive()
                    match inbox.CurrentQueueLength with
                    | 0 ->
                        let js = Js.dynamicHighcharts address guid shiftField msg
                        match inbox.CurrentQueueLength with
                        | 0 ->
                            let html = Html.dynamicHighcharts msg.Type address js
                            match inbox.CurrentQueueLength with
                            | 0 ->
                                System.IO.File.WriteAllText(htmlFile, html)
                                browser.Url <- htmlFile
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
            browser.Quit()
            remove guid
            File.Delete htmlFile
        with _ -> ()

    static member internal Create config shift =
        let gdc = GenericDynamicChart()
        gdc.SetChartConfig  config
        match shift with true -> gdc.SetShift true | false -> ()
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
            dataHub.Clients.Client(id)?push value |> ignore
        with _ -> ()

    /// <summary>Adds a new data point to the chart.</summary>
    member __.Push(key, value) =
        let keyType = (key :> key).GetTypeCode()
        let key' = Utils.utcIfDatetime keyType key
        let value' = value :> value
        try
            let id = guidsList |> Seq.find (fun x -> fst x = guid) |> snd
            dataHub.Clients.Client(id)?push [|key'; value'|] |> ignore
        with _ -> ()

    /// <summary>Adds a new data point to the chart.</summary>
    member __.Push(key, value, value') =
        let keyType = (key :> key).GetTypeCode()
        let key' = Utils.utcIfDatetime keyType key
        let value1' = value :> value
        let value2' = value' :> value
        try
            let id = guidsList |> Seq.find (fun x -> fst x = guid) |> snd
            dataHub.Clients.Client(id)?push [|key'; value1'; value2'|] |> ignore
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

    /// <summary>Gets the shift property that determines whether one point is shifted off the start of the series as one is appended to the end.</summary>
    member __.Shift = shiftField

    /// <summary>Displays the legend of a chart.</summary>
    member __.ShowLegend() =
        remove guid
        chart.chartData <- { chart.chartData with Legend = true }
        agent.Post chart.chartData