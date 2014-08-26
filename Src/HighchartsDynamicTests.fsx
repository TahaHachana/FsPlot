#r """.\packages\FunScript.1.1.54\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.54\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """.\packages\FunScript.TypeScript.Binding.gapi.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.gapi.dll"""
#r """.\packages\FunScript.TypeScript.Binding.google_visualization.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.google_visualization.dll"""
#r """.\packages\Microsoft.AspNet.Cors.5.1.0\lib\net45\System.Web.Cors.dll"""
#r """.\packages\Microsoft.Owin.2.1.0\lib\net45\Microsoft.Owin.dll"""
#r """.\packages\Microsoft.Owin.Hosting.2.1.0\lib\net45\Microsoft.Owin.Hosting.dll"""
#r """.\packages\Microsoft.Owin.Security.2.1.0\lib\net45\Microsoft.Owin.Security.dll"""
#r """.\packages\Owin.1.0\lib\net40\Owin.dll"""
#r """.\bin\release\FsPlot.dll"""

open FsPlot.Data
open FsPlot.Highcharts.Charting
open FunScript.TypeScript
open System
open System.IO

FunScript.Compiler.compile
    <@
        createEmpty<Date>() |> ignore
        Globals.Dollar.now() |> ignore
        createEmpty<HighchartsChartOptions>() |> ignore
        createEmpty<HubProxy>() |> ignore
        createEmpty<google.visualization.BarChartOptions>() |> ignore
    @>
|> ignore

module Area =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Area
        
    let fileName =
        Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "chart.png")

    DynamicChart.SavePng fileName chart1

    chart1.Push("2014", 577)
    chart1.Close()

module Areaspline =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Areaspline
        
    chart1.Push("2014", 577)
    chart1.Close()

module Arearange =

    let chart1 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> DynamicChart.Arearange
        
    chart1.Push(DateTime.Now.AddDays 3., 4., 10.)
    chart1.Close()

module Bar =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Bar
        
    chart1.Push("2014", 577)
    chart1.Close()

module Bubble =
    
    let chart1 =
        [97,36,79; 94,74,60; 68,76,58]
        |> DynamicChart.Bubble

    chart1.Push(50,72,48)
    chart1.Close()

module Column =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Column

    chart1.Push("2014", 1400)
    chart1.Close()

module Donut =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Donut

    chart1.Push ("2014", 1400)
    chart1.Close()

module Funnel =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Funnel

    chart1.Push ("2014", 1400)
    chart1.Close()

module Line =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Line
    
    chart1.Push ("2014", 1400)
    chart1.Close()

module Pie =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Pie

    chart1.Push("2014", 1440)
    chart1.Close()

module Radar =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Radar
    
    chart1.Push ("2014", 1400)
    chart1.Close()

module Scatter =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Scatter
    
    chart1.Push ("2014", 1400)
    chart1.Close()

module Spline =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicChart.Spline

    chart1.Push ("2014", 1400)
    chart1.Close()
