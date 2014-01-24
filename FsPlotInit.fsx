#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """.\packages\Microsoft.Owin.2.0.2\lib\net45\Microsoft.Owin.dll"""
#r """.\packages\Microsoft.Owin.Hosting.2.0.2\lib\net45\Microsoft.Owin.Hosting.dll"""
#r """.\packages\Microsoft.Owin.Host.HttpListener.2.0.2\lib\net45\Microsoft.Owin.Host.HttpListener.dll"""
#r """.\packages\Microsoft.Owin.Security.2.0.2\lib\net45\Microsoft.Owin.Security.dll"""
#r """.\packages\Owin.1.0\lib\net40\Owin.dll"""
#r """.\bin\release\FsPlot.dll"""

open Owin
open Microsoft.Owin.Hosting
open System
open System.Net

// Warm up FunScript's compiler.
FunScript.Compiler.compile
    <@
        createEmpty<HighchartsChartOptions>() |> ignore
        createEmpty<HubProxy>() |> ignore
        Globals.Dollar.now()
    @>
|> ignore

// Warm up Owin
type WarmupStartup() =
    
    member __.Configuration(app:IAppBuilder) = ()

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

let address = "http://localhost:" + freePort()
let app = WebApp.Start<WarmupStartup> address
app.Dispose()


