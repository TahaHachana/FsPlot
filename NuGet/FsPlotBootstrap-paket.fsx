#I __SOURCE_DIRECTORY__
#r """../../packages/FunScript/lib/net40/FunScript.dll"""
#r """../../packages/FunScript/lib/net40/FunScript.Interop.dll"""
#r """../../packages/FunScript.TypeScript.Binding.lib/lib/net40/FunScript.TypeScript.Binding.lib.dll"""
#r """../../packages/FunScript.TypeScript.Binding.jquery/lib/net40/FunScript.TypeScript.Binding.jquery.dll"""
#r """../../packages/FunScript.TypeScript.Binding.highcharts/lib/net40/FunScript.TypeScript.Binding.highcharts.dll"""
#r """../../packages/FunScript.TypeScript.Binding.signalr/lib/net40/FunScript.TypeScript.Binding.signalr.dll"""
#r """../../packages/FunScript.TypeScript.Binding.gapi/lib/net40/FunScript.TypeScript.Binding.gapi.dll"""
#r """../../packages/FunScript.TypeScript.Binding.google_visualization/lib/net40/FunScript.TypeScript.Binding.google_visualization.dll"""
#r """../../packages/Microsoft.Owin/lib/net45/Microsoft.Owin.dll"""
#r """../../packages/Microsoft.Owin.Hosting/lib/net45/Microsoft.Owin.Hosting.dll"""
#r """../../packages/Microsoft.Owin.Host.HttpListener/lib/net45/Microsoft.Owin.Host.HttpListener.dll"""
#r """../../packages/Microsoft.Owin.Security/lib/net45/Microsoft.Owin.Security.dll"""
#r """../../packages/Microsoft.Owin.Cors/lib/net45/Microsoft.Owin.Cors.dll"""
#r """../../packages/Microsoft.AspNet.Cors/lib/net45/System.Web.Cors.dll"""
#r """../../packages/Owin/lib/net40/Owin.dll"""
#r """../../packages/Selenium.WebDriver/lib/net40/WebDriver.dll"""
#r """../../packages/Microsoft.AspNet.SignalR.Core/lib/net45/Microsoft.AspNet.SignalR.Core.dll"""
#r """../../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"""
#r """../../packages/FsPlot/Lib/net45/FsPlot.dll"""

open FunScript.TypeScript

// Warm up FunScript's compiler.
FunScript.Compiler.compile
    <@
        createEmpty<Date>() |> ignore
        Globals.Dollar.now() |> ignore
        createEmpty<HighchartsChartOptions>() |> ignore
        createEmpty<HubProxy>() |> ignore
        createEmpty<google.visualization.BarChartOptions>() |> ignore
    @>
|> ignore

// set ChromeDriver.exe executable location
FsPlot.Settings.FSPlotSettings.chromeDriverDirectory <- System.IO.Path.Combine(__SOURCE_DIRECTORY__, "./tools/")