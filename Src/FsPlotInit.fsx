#r """.\packages\FunScript.1.1.50\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.50\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """.\packages\FunScript.TypeScript.Binding.gapi.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.gapi.dll"""
#r """.\packages\FunScript.TypeScript.Binding.google_visualization.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.google_visualization.dll"""
#r """.\packages\Microsoft.Owin.2.1.0\lib\net45\Microsoft.Owin.dll"""
#r """.\packages\Microsoft.Owin.Hosting.2.1.0\lib\net45\Microsoft.Owin.Hosting.dll"""
#r """.\packages\Microsoft.Owin.Host.HttpListener.2.1.0\lib\net45\Microsoft.Owin.Host.HttpListener.dll"""
#r """.\packages\Microsoft.Owin.Security.2.1.0\lib\net45\Microsoft.Owin.Security.dll"""
#r """.\packages\Owin.1.0\lib\net40\Owin.dll"""
#r """.\bin\release\FsPlot.dll"""

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