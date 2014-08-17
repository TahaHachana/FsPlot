module FsPlot.HighchartsDynamic

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

type HighchartsDynamicChart() as chart =

    [<DefaultValue>] val mutable private config : ChartConfig    
    let mutable shiftField = true
    let address = "http://localhost:" + freePort()
    let guid = Guid.NewGuid().ToString()
    let app = WebApp.Start<Startup> address
    let htmlFile = Path.GetTempPath() + guid + ".html"
    do File.WriteAllText(htmlFile, "")
    let browser = Browser.start htmlFile

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
                                browser.Navigate().Refresh()
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

    static member internal Create config =
        let gdc = HighchartsDynamicChart()
        gdc.config <- config
        gdc.Refresh()
        gdc

    /// <summary>Hides the legend of a chart.</summary>
    member __.WithLegend enabled =
        remove guid
        chart.config <- { chart.config with Legend = enabled }
        agent.Post chart.config

    /// <summary>Adds a new data point to the chart.</summary>
    member __.Push(key:#key, value:#value) =
        let keyType = (key :> key).GetTypeCode()
        let key' = Utils.utcIfDatetime keyType key
        let value' = value :> value
        try
            let id = guidsList |> Seq.find (fun x -> fst x = guid) |> snd
            dataHub.Clients.Client(id)?push [|key'; value'|] |> ignore
        with _ -> ()

    /// <summary>Adds a new data point to the chart.</summary>
    member __.Push(key:#key, value:#value, value':#value) =
        let keyType = (key :> key).GetTypeCode()
        let key' = Utils.utcIfDatetime keyType key
        let value1' = value :> value
        let value2' = value' :> value
        try
            let id = guidsList |> Seq.find (fun x -> fst x = guid) |> snd
            dataHub.Clients.Client(id)?push [|key'; value1'; value2'|] |> ignore
        with _ -> ()

    member internal __.Refresh() = agent.Post chart.config

    /// <summary>Sets the chart's data set name.</summary>
    member __.WithName name =
        chart.config <-
            {
                chart.config with
                    Data = [|chart.config.Data.[0] |> Series.WithName name |]
            }
        chart.Refresh()

    /// <summary>Sets the shift property that determines whether
    /// one point is shifted off the start of the series as one
    /// is appended to the end.</summary>
    member __.WithShift enabled =
        remove guid
        shiftField <- enabled
        agent.Post chart.config

    /// <summary>Sets the chart's title.</summary>
    member __.WithTitle title =
        remove guid
        chart.config <- { chart.config with Title = Some title }
        agent.Post chart.config

    /// <summary>Sets the chart's X-axis title.</summary>
    member __.WithXTitle(title) =
        remove guid
        chart.config <- { chart.config with XTitle = Some title }
        agent.Post chart.config

    /// <summary>Sets the chart's Y-axis title.</summary>
    member __.WithYTitle(title) =
        remove guid
        chart.config <- { chart.config with YTitle = Some title }
        agent.Post chart.config
