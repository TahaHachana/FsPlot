#r """.\packages\FunScript.1.1.86\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.86\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """.\packages\FunScript.TypeScript.Binding.gapi.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.gapi.dll"""
#r """.\packages\FunScript.TypeScript.Binding.google_visualization.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.google_visualization.dll"""
#r """.\bin\release\FsPlot.dll"""

open System
//open FsPlot.Data
open FsPlot.Google.Charting
open FsPlot.Google.Options
open FunScript
open FunScript.TypeScript
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

FsPlot.Settings.FSPlotSettings.chromeDriverDirectory <- @"C:\Users\AHMED\Desktop\chromedriver_win32"

module Bar =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Bar
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    let fileName =
        Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "chart.png")

    Chart.SavePng fileName chart1

    Chart.Close chart1

    let chart2 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Bar
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart2

module Column =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Column
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Column
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart2

module Line =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Line
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Line
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart2

module Spline =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Spline
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Spline
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart2

module StackedBar =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.StackedBar
        |> Chart.WithName "Expenses"
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.StackedBar
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart2

module StackedColumn =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.StackedColumn
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.StackedColumn
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart2

module Geo =

    let geo1 =
        ["Germany", 200; "United States", 300; "Brazil", 400]
        |> Geochart.New
        |> Geochart.WithName "Popularity"

    let fileName =
        Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "chart.png")

    Geochart.SavePng fileName geo1

    Geochart.Close geo1

    let geo2 =
        ["Rome", 2761477, 1285.31; "Milan", 1324110, 181.76]
        |> Geochart.New
        |> Geochart.WithNames ["Population"; "Area"]
        |> Geochart.WithRegion "IT"
        |> Geochart.WithMode "markers"

    Geochart.Close geo2

    let geo3 =
        [
            "France",  65700000, 50
            "Germany", 81890000, 27
            "Poland",  38540000, 23
        ]
        |> Geochart.New
        |> Geochart.WithNames ["Population"; "Area Percentage"]
        |> Geochart.WithRegion "155"
        |> Geochart.WithMode "markers"
        |> Geochart.WithSizeAxis {MinValue = 0.; MaxValue = 100.}

    Geochart.Close geo3

module Map =

    let chart1 =
        [
            "China", "China: 1,363,800,000"
            "India", "India: 1,242,620,000"
            "US", "US: 317,842,000"
            "Indonesia", "Indonesia: 247,424,598"
            "Brazil", "Brazil: 201,032,714"
            "Pakistan", "Pakistan: 186,134,000"
            "Nigeria", "Nigeria: 173,615,000"
            "Bangladesh", "Bangladesh: 152,518,015"
            "Russia", "Russia: 146,019,512"
            "Japan", "Japan: 127,120,000"
        ]
        |> Mapchart.New 

    Mapchart.Close chart1

    let chart2 =
        [
            37.4232, -122.0853, "Work"
            37.4289, -122.1697, "University"
            37.6153, -122.3900, "Airport"
            37.4422, -122.1731, "Shopping"            
        ]
        |> Mapchart.New

    Mapchart.Close chart2
