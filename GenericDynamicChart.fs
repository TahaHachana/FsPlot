module FsPlot.GenericDynamicChart

//#r "System.Web.Http.dll"
//#r "System.Net.Http"
//#r "System.Web.Http.SelfHost.dll"

#if INTERACTIVE 
#r """.\packages\Newtonsoft.Json.5.0.8\lib\net45\Newtonsoft.Json.dll"""
#r "Microsoft.CSharp.dll"
#r """.\packages\Microsoft.Owin.2.0.2\lib\net45\Microsoft.Owin.dll"""
#r """.\packages\Microsoft.Owin.Hosting.2.0.2\lib\net45\Microsoft.Owin.Hosting.dll"""
#r """.\packages\Owin.1.0\lib\net40\Owin.dll"""
#r """.\packages\Microsoft.AspNet.SignalR.Core.2.0.1\lib\net45\Microsoft.AspNet.SignalR.Core.dll"""
#r """.\packages\Microsoft.Owin.Cors.2.0.2\lib\net45\Microsoft.Owin.Cors.dll"""
#r """.\packages\Microsoft.Owin.Host.HttpListener.2.0.2\lib\net45\Microsoft.Owin.Host.HttpListener.dll"""
#r """.\packages\Microsoft.Owin.Security.2.0.2\lib\net45\Microsoft.Owin.Security.dll"""
#r """.\packages\Microsoft.AspNet.Cors.5.0.0\lib\net45\System.Web.Cors.dll"""
#endif
 
open System
open System.Diagnostics
open Owin
open Microsoft.AspNet.SignalR
//open Microsoft.Owin.FileSystems
open Microsoft.Owin.Hosting

//open Microsoft.Owin.StaticFiles

 
module Dynamic =
    open System
    open System.Runtime.CompilerServices
    open Microsoft.CSharp.RuntimeBinder
 
    // Simple implementation of ? operator that works for instance
    // method calls that take a single argument and return some result
    let (?) (inst:obj) name (arg:'T) : 'R =
      // TODO: For efficient implementation, consider caching of call sites 
      // Create dynamic call site for converting result to type 'R
      let convertSite = 
        CallSite<Func<CallSite, Object, 'R>>.Create
          (Binder.Convert(CSharpBinderFlags.None, typeof<'R>, null))
 
      // Create call site for performing call to method with the given
      // name and a single parameter of type 'T
      let callSite = 
        CallSite<Func<CallSite, Object, 'T, Object>>.Create
          (Binder.InvokeMember
            ( CSharpBinderFlags.None, name, null, null, 
              [| CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) |]))
 
      // Run the method call using second call site and then 
      // convert the result to the specified type using first call site
      convertSite.Target.Invoke
        (convertSite, callSite.Target.Invoke(callSite, inst, arg))
 
open Dynamic
 
//let attachHub (app: IAppBuilder) =
//        app.MapSignalR()
//
//let hostFiles filePath (app: IAppBuilder) =
//    let options = StaticFileOptions(FileSystem = PhysicalFileSystem(filePath))
//    app.UseStaticFiles(options)
 
//        app.

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

 
//System.Diagnostics.Process.Start("chrome", @"C:\Users\AHMED\Documents\T\index.html")
//    let hub = GlobalHost.ConnectionManager.GetHubContext<ChartHub>()
//
//    let utc (x:DateTime) =
//        x.Subtract(DateTime(1970, 1, 1)).TotalMilliseconds
//        |> int64
//
//    type DataPoint = {X : int64; Y : float}
//    let rnd = Random()
//    let dp() = {X = utc DateTime.Now; Y = rnd.NextDouble()}
//
//    hub.Clients.All?parse(dp()) |> ignore

type Startup() =
    
    member __.Configuration(app:IAppBuilder) =
        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll) |> ignore
        app.MapSignalR() |> ignore

open FsPlot.Config


let guidsList = System.Collections.Generic.List<string*string>()

let remove guid =
    guidsList.RemoveAll(fun (chartGuid, _) -> chartGuid = guid)
    |> ignore

type DataHub() =
    inherit Hub()

    member x.StoreGuids(chartGuid, proxyGuid) : unit =
        guidsList.Add(chartGuid, proxyGuid)

module Html =
    
    let common address js =
        let hubsJs = sprintf """<script src="%s/signalr/hubs" type="text/javascript"></script>""" address
        String.concat
            ""
            [
                "<!DOCTYPE html><head><title>Highcharts Chart</title></head><body>"
                """<div id="chart" style="width:100%; height:100%"></div>"""
                """<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>"""
                """<script src="http://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.0.0.min.js"></script>"""
                hubsJs
                """<script src="http://code.highcharts.com/highcharts.js"></script>"""
                """<script src="http://code.highcharts.com/modules/exporting.js"></script>"""
                "<script>"
                js
                "</script></body></html>"
            ]

module Js =

    let highcharts address guid (config:ChartConfig) = FsPlot.Highcharts.Js.dynamicArea address guid config

type GenericDynamicChart() as chart =

    [<DefaultValue>] val mutable private chartData : ChartConfig    

    let address = "http://localhost:" + freePort()
    let guid = Guid.NewGuid().ToString()
    let app = WebApp.Start<Startup> address
    let fileName = System.IO.Path.GetTempPath() + guid + ".html"

    let agent =
        MailboxProcessor<ChartConfig>.Start(fun inbox ->
            let rec loop() =
                async {
                    let! msg = inbox.Receive()
                    match inbox.CurrentQueueLength with
                    | 0 ->
                        let js = Js.highcharts address guid msg
                        match inbox.CurrentQueueLength with
                        | 0 ->
                            let html = Html.common address js
                            match inbox.CurrentQueueLength with
                            | 0 ->
                                do System.IO.File.WriteAllText(fileName, html)
                                do System.Diagnostics.Process.Start("chrome", fileName) |> ignore                                
                                return! loop()
                            | _ -> return! loop()
                        | _ -> return! loop()
                    | _ -> return! loop()
                }
            loop())

    member __.Push(dataPoint) =
        let hub = GlobalHost.ConnectionManager.GetHubContext<DataHub>()
        let id = guidsList |> Seq.find(fun x -> fst x = guid) |> snd
        hub.Clients.Client(id)?push(dataPoint) |> ignore
     
    static member internal Create config =
        let gdc = GenericDynamicChart()
        gdc.SetChartConfig  config
        gdc

    member internal __.SetChartConfig chartData = 
        chart.chartData <- chartData
        agent.Post chart.chartData
        
    /// <summary>Sets the chart's title.</summary>
    member __.SetTitle title =
        remove guid
        chart.chartData <- { chart.chartData with Title = Some title }
        agent.Post chart.chartData

//    /// <summary>Closes the chart's window.</summary>
//    member __.Close() = wnd.Close()
//
//    static member internal Create x (f:unit -> #GenericChart) =
//        let gc = f()
//        gc.SetChartConfig  x
//        gc
//
//    /// <summary>Hides the legend of a chart.</summary>
//    member __.HideLegend() =
//        chart.chartData <- { chart.chartData with Legend = false }
//        agent.Post chart.chartData
//
//    member internal __.Navigate() = agent.Post chart.chartData
//
//    /// <summary>Sets the categories of a chart's X-axis.</summary>
//    member __.SetCategories(categories) =
//        chart.chartData <- { chart.chartData with Categories = Seq.toArray categories}
//        agent.Post chart.chartData
//
//    member internal __.SetChartConfig chartData = 
//        chart.chartData <- chartData
//        agent.Post chart.chartData
//
//    /// <summary>Sets the data series used by a chart.</summary>
//    member __.SetData series =
//        chart.chartData <- { chart.chartData with Data = [|series|] }
//        agent.Post chart.chartData
//
//    /// <summary>Sets the data series used by a chart.</summary>
//    member __.SetData (data:seq<Series>) =
//        let series = Seq.toArray data
//        chart.chartData <- { chart.chartData with Data = series }
//        agent.Post chart.chartData
//
//    member internal __.SetJsFun(f) = jsFun <- f
//
//    /// <summary>Modifies the tooltip format for each data point.</summary>
//    member __.SetTooltip(tooltip) =
//        chart.chartData <- { chart.chartData with Tooltip = Some tooltip }
//        agent.Post chart.chartData
//
//    /// <summary>Sets the chart's subtitle.</summary>
//    member __.SetSubtitle subtitle =
//        chart.chartData <- { chart.chartData with Subtitle = Some subtitle }
//        agent.Post chart.chartData
//
//    /// <summary>Sets the chart's title.</summary>
//    member __.SetTitle title =
//        chart.chartData <- { chart.chartData with Title = Some title }
//        agent.Post chart.chartData
//
//    /// <summary>Sets the chart's X-axis title.</summary>
//    member __.SetXTitle(title) =
//        chart.chartData <- { chart.chartData with XTitle = Some title }
//        agent.Post chart.chartData
//
//    /// <summary>Sets the chart's Y-axis title.</summary>
//    member __.SetYTitle(title) =
//        chart.chartData <- { chart.chartData with YTitle = Some title }
//        agent.Post chart.chartData
//
//    /// <summary>Displays the legend of a chart.</summary>
//    member __.ShowLegend() =
//        chart.chartData <- { chart.chartData with Legend = true }
//        agent.Post chart.chartData
//

let private newChartConfig categories data legend subtitle title tooltip chartType xTitle yTitle =
    let legend' = defaultArg legend false
    let categories' =
        match categories with 
        | None -> [||]
        | Some value -> Seq.toArray value
    ChartConfig.New categories' data legend' subtitle title tooltip chartType xTitle yTitle

open FsPlot.Data

type DynamicHighcharts =

    /// <summary>Creates an area chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The X-axis categories.</param>
    /// <param name="legend">Whether to display a legend or not.</param>
    /// <param name="title">The chart's title.</param>
    /// <param name="xTitle">The X-axis title.</param>
    /// <param name="yTitle">The Y-axis title.</param>
    static member Area(data:Series, ?categories, ?legend, ?title, ?xTitle, ?yTitle) =
        let chartData = newChartConfig categories [|data|] legend None title None Area xTitle yTitle
        GenericDynamicChart.Create chartData
