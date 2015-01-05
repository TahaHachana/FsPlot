#I __SOURCE_DIRECTORY__
#r """../../packages/FunScript.1.1.86/lib/net40/FunScript.dll"""
#r """../../packages/FunScript.1.1.86/lib/net40/FunScript.Interop.dll"""
#r """../../packages/FunScript.TypeScript.Binding.lib.1.1.0.37/lib/net40/FunScript.TypeScript.Binding.lib.dll"""
#r """../../packages/FunScript.TypeScript.Binding.jquery.1.1.0.37/lib/net40/FunScript.TypeScript.Binding.jquery.dll"""
#r """../../packages/FunScript.TypeScript.Binding.highcharts.1.1.0.37/lib/net40/FunScript.TypeScript.Binding.highcharts.dll"""
#r """../../packages/FunScript.TypeScript.Binding.signalr.1.1.0.37/lib/net40/FunScript.TypeScript.Binding.signalr.dll"""
#r """../../packages/FunScript.TypeScript.Binding.gapi.1.1.0.37/lib/net40/FunScript.TypeScript.Binding.gapi.dll"""
#r """../../packages/FunScript.TypeScript.Binding.google_visualization.1.1.0.37/lib/net40/FunScript.TypeScript.Binding.google_visualization.dll"""
#r """../../packages/Microsoft.Owin.3.0.0/lib/net45/Microsoft.Owin.dll"""
#r """../../packages/Microsoft.Owin.Hosting.3.0.0/lib/net45/Microsoft.Owin.Hosting.dll"""
#r """../../packages/Microsoft.Owin.Host.HttpListener.3.0.0/lib/net45/Microsoft.Owin.Host.HttpListener.dll"""
#r """../../packages/Microsoft.Owin.Security.3.0.0/lib/net45/Microsoft.Owin.Security.dll"""
#r """../../packages/Microsoft.Owin.Cors.3.0.0/lib/net45/Microsoft.Owin.Cors.dll"""
#r """../../packages/Microsoft.AspNet.Cors.5.2.2/lib/net45/System.Web.Cors.dll"""
#r """../../packages/Owin.1.0/lib/net40/Owin.dll"""
#r """../../packages/Selenium.WebDriver.2.44.0/lib/net40/WebDriver.dll"""
#r """../../packages/Microsoft.AspNet.SignalR.Core.2.1.2/lib/net45/Microsoft.AspNet.SignalR.Core.dll"""
#r """../../packages/Newtonsoft.Json.6.0.7/lib/net45/Newtonsoft.Json.dll"""
#r """../../packages/FsPlot.0.6.6/Lib/net45/FsPlot.dll"""

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