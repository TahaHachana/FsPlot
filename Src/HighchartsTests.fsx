#r """.\packages\FunScript.1.1.86\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.86\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """.\packages\FunScript.TypeScript.Binding.gapi.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.gapi.dll"""
#r """.\packages\FunScript.TypeScript.Binding.google_visualization.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.google_visualization.dll"""
#r """.\bin\release\FsPlot.dll"""

open FsPlot.Data
open FsPlot.Highcharts.Charting
open FunScript
open FunScript.TypeScript
open System
open System.IO

Compiler.compile
    <@
        createEmpty<Date>() |> ignore
        Globals.Dollar.now() |> ignore
        createEmpty<HighchartsChartOptions>() |> ignore
        createEmpty<HubProxy>() |> ignore
        createEmpty<google.visualization.BarChartOptions>() |> ignore
    @>
|> ignore

FsPlot.Settings.FSPlotSettings.chromeDriverDirectory <- @"C:\Users\AHMED\Desktop\chromedriver_win32"

module Area =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Area
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    let fileName =
        Path.Combine
            (Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "chart.png")

    Chart.SavePng fileName chart1

    chart1.WithName "New Name"
    chart1.WithTitle "New Chart Title"
    chart1.WithLabels ["2012"; "2013"; "2014"; "2015"]
    chart1.WithXTitle "New X Title"
    chart1.WithYTitle "New Y Title"
    chart1.WithLegend false

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Area
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Area
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3
    
    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Area
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module Arearange =
    
    let chart1 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> Chart.Arearange

    Chart.Close chart1

    let chart2 =
        [
            -2.5, 5.
            -3., 2.
            3., 15.
        ]
        |> Chart.Arearange

    Chart.Close chart2

    let chart3 =
        [
            [
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            ]
            [
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            ]
        ]
        |> Chart.Arearange

    Chart.Close chart3
    
    let chart4 =
        [
            [
                -5., 15.
                -3., 2.
                3., 15.
            ]
            [
                -2.5, 5.
                -3., 2.
                3., 15.
            ]
        ]
        |> Chart.Arearange

    Chart.Close chart4

module Areaspline =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Areaspline
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Areaspline
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Areaspline
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Area
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module Bar =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Bar
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Bar
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Bar
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Bar
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4
                
module Bubble =
    
    let chart1 =
        [97,36,79; 94,74,60; 68,76,58]
        |> Chart.Bubble 

    Chart.Close chart1

    let chart2 =
        [36,79; 74,60; 76,58]
        |> Chart.Bubble

    Chart.Close chart2

    let chart3 =
        [
            [54,26,59; 64,74,60; 70,26,58]
            [97,36,79; 94,74,60; 68,76,58]
        ]
        |> Chart.Bubble

    Chart.Close chart3

    let chart4 =
        [
            [26,59; 74,60; 26,58]
            [36,79; 74,60; 76,58]
        ]
        |> Chart.Bubble

    Chart.Close chart4

module Column =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Column
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Column
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Column
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Column
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module Combination =

    let chart1 =
        [
            Series.Column [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Series.Line [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|] 
        ]
        |> Chart.Combine

    Chart.Close chart1

    let chart2 =
        [
            Series.Column([3; 2; 1; 3; 4], "Jane")
            Series.Column([2; 3; 5; 7; 6], "John")
            Series.Column([4; 3; 3; 9; 0], "Joe")
            Series.Spline([3.; 2.67; 3.; 6.33; 3.33], "Average")
        ]
        |> Chart.Combine

    Chart.Close chart2

module Donut =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Donut

    Chart.Close chart1

    let chart2 =
        [1000; 1170; 560; 1030]
        |> Chart.Donut

    Chart.Close chart2

module Funnel =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Funnel

    Chart.Close chart1

    let chart2 =
        [1000; 1170; 560; 1030]
        |> Chart.Funnel

    Chart.Close chart2

module Line =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Line
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Line
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Line
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Line
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module PercentArea =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.PercentArea
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.PercentArea
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.PercentArea
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.PercentArea
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module PercentBar =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.PercentBar
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.PercentBar
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.PercentBar
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.PercentBar
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module PercentColumn =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.PercentColumn
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.PercentColumn
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.PercentColumn
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.PercentColumn
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module Pie =

    let chart1 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Pie

    Chart.Close chart1

    let chart2 =
        [1000; 1170; 560; 1030]
        |> Chart.Pie

    Chart.Close chart2

module Radar =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Radar
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Radar
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Radar
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Radar
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module Scatter =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Scatter
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Scatter
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Scatter
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Scatter
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module Spline =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.Spline
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.Spline
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.Spline
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.Spline
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module StackedArea =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.StackedArea
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.StackedArea
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.StackedArea
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.StackedArea
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module StackedBar =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.StackedBar
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.StackedBar
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.StackedBar
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.StackedBar
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4

module StackedColumn =
    
    let chart1 =        
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Chart.StackedColumn
        |> Chart.WithName "Expenses"
        |> Chart.WithTitle "Chart Title"
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"]
        |> Chart.WithXTitle "X Title"
        |> Chart.WithYTitle "Y Title"
        |> Chart.WithLegend true

    Chart.Close chart1

    let chart2 =
        [500; 1000; 684; 792; 574]
        |> Chart.StackedColumn
        |> Chart.WithLabels ["2011"; "2012"; "2013"; "2014"; "2015"]

    Chart.Close chart2

    let chart3 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Chart.StackedColumn
        |> Chart.WithNames ["Sales"; "Expenses"]

    Chart.Close chart3

    let chart4 =
        [
            [1300; 1470; 740; 1330]
            [1000; 1170; 560; 1030]
        ]
        |> Chart.StackedColumn
        |> Chart.WithNames ["Sales"; "Expenses"]
        |> Chart.WithLabels ["2010"; "2011"; "2012"; "2013"]

    Chart.Close chart4
