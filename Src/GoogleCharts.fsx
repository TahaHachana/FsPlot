#r """.\packages\FunScript.1.1.50\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.50\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """.\packages\FunScript.TypeScript.Binding.gapi.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.gapi.dll"""
#r """.\packages\FunScript.TypeScript.Binding.google_visualization.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.google_visualization.dll"""
#r """.\bin\release\FsPlot.dll"""

open System
open FsPlot.Data
open FsPlot.Google.Charting
open FsPlot.Google.Options
open FunScript
open FunScript.TypeScript

FunScript.Compiler.compile
    <@
        createEmpty<Date>() |> ignore
        Globals.Dollar.now() |> ignore
        createEmpty<HighchartsChartOptions>() |> ignore
        createEmpty<HubProxy>() |> ignore
        createEmpty<google.visualization.BarChartOptions>() |> ignore
    @>
|> ignore

let sales = Series.Bar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
let chart = Google.Bar sales
chart.SetCategories [|"Sales"|]
chart.SetTitle "Company Performance"
chart.SetXTitle "X Title"
chart.SetYTitle "Y Title"
chart.ShowLegend()
chart.HideLegend()
chart.Close()

module Bar =

    let bar1 =
        let sales = Series.Bar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Bar(sales, "Sales", "Company Performance")

    let bar2 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Bar(sales, "Sales", "Company Performance")


    let bar3 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Bar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")
    
    let bar4 =
        let sales = [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        let expenses = [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        Google.Bar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let bar5 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330] |> List.toSeq
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030] |> List.toSeq
        Google.Bar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let bar6 =
        let sales = Series.Bar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = Series.Bar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Bar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

module Column =

    let column1 =
        let sales = Series.Column ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Column(sales, "Sales", "Company Performance")

    let column2 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Column(sales, "Sales", "Company Performance")
    
    let column3 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Column([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let column4 =
        let sales = [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        let expenses = [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        Google.Column([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let column5 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330] |> List.toSeq
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030] |> List.toSeq
        Google.Column([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let column6 =
        let sales = Series.Column ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Column([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

module Geo =

    let geo1 =
        let data = ["Germany", 200; "United States", 300; "Brazil", 400]
        Google.Geo(data, "Popularity")

    let geo2 =
        let data = ["Rome", 2761477, 1285.31; "Milan", 1324110, 181.76]
        Google.Geo(data, ["Population"; "Area"], "IT", "markers")

    let geo3 =
        let data =
            [
                "France",  65700000, 50
                "Germany", 81890000, 27
                "Poland",  38540000, 23
            ]
        Google.Geo(data, ["Population"; "Area Percentage"], "155", "markers", {MinValue = 0.; MaxValue = 100.})

module Line =

    let line1 =
        let sales = Series.Line ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Line(sales, "Sales", "Company Performance")

    let line2 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Line(sales, "Sales", "Company Performance")


    let line3 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Line([sales; expenses], ["Sales"; "Expenses"], "Company Performance")
    
    let line4 =
        let sales = [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        let expenses = [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        Google.Line([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let line5 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330] |> List.toSeq
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030] |> List.toSeq
        Google.Line([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let line6 =
        let sales = Series.Line ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = Series.Line ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Line([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

module Spline =

    let spline1 =
        let sales = Series.Spline ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Spline(sales, "Sales", "Company Performance")

    let spline2 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.Spline(sales, "Sales", "Company Performance")


    let spline3 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Spline([sales; expenses], ["Sales"; "Expenses"], "Company Performance")
    
    let spline4 =
        let sales = [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        let expenses = [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        Google.Spline([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let spline5 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330] |> List.toSeq
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030] |> List.toSeq
        Google.Spline([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let spline6 =
        let sales = Series.Spline ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = Series.Spline ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.Spline([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

module StackedBar =

    let stackedBar1 =
        let sales = Series.StackedBar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.StackedBar(sales, "Sales", "Company Performance")

    let stackedBar2 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.StackedBar(sales, "Sales", "Company Performance")


    let stackedBar3 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.StackedBar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")
    
    let stackedBar4 =
        let sales = [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        let expenses = [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        Google.StackedBar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let stackedBar5 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330] |> List.toSeq
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030] |> List.toSeq
        Google.StackedBar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let stackedBar6 =
        let sales = Series.StackedBar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = Series.StackedBar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.StackedBar([sales; expenses], ["Sales"; "Expenses"], "Company Performance")
        
module StackedColumn =

    let stackedColumn1 =
        let sales = Series.StackedColumn ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.StackedColumn(sales, "Sales", "Company Performance")

    let stackedColumn2 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        Google.StackedColumn(sales, "Sales", "Company Performance")

    
    let stackedColumn3 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.StackedColumn([sales; expenses], ["Sales"; "Expenses"], "Company Performance")


    let stackedColumn4 =
        let sales = [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        let expenses = [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        Google.StackedColumn([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let stackedColumn5 =
        let sales = Series.StackedColumn ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        let expenses = Series.StackedColumn ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Google.StackedColumn([sales; expenses], ["Sales"; "Expenses"], "Company Performance")

    let stackedColumn6 =
        let sales = ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330] |> List.toSeq
        let expenses = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030] |> List.toSeq
        Google.StackedColumn([sales; expenses], ["Sales"; "Expenses"], "Company Performance")
