﻿#r """.\packages\FunScript.1.1.50\lib\net40\FunScript.dll"""
#r @"C:\Users\AHMED\Documents\GitHub\FsPlot\packages\FunScript.1.1.50\lib\net40\FunScript.Interop.dll"
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """C:\Users\AHMED\Documents\GitHub\FsPlot\packages\FunScript.TypeScript.Binding.gapi.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.gapi.dll"""
#r """C:\Users\AHMED\Documents\GitHub\FsPlot\packages\FunScript.TypeScript.Binding.google_visualization.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.google_visualization.dll"""
#r """.\bin\release\FsPlot.dll"""

open System
open FsPlot.Data
open FsPlot.Google.Charting
open FunScript
open FunScript.TypeScript

FunScript.Compiler.compile
    <@
        createEmpty<Date>() |> ignore
        Globals.Dollar.now() |> ignore
        createEmpty<HighchartsChartOptions>() |> ignore
        createEmpty<HubProxy>() |> ignore
    @>
|> ignore

let sales = Series.Bar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
let chart = Google.Bar sales
chart.SetLabels [|"Sales"|]
chart.SetTitle "Company Performance"
chart.SetXTitle "X Title"
chart.SetYTitle "Y Title"
chart.Close()

module Bar =

    let bar1 =
        let sales = Series.Bar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Bar(sales, "Sales", "Company Performance")

    let bar2 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Bar(sales, "Sales", "Company Performance")

