#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """.\packages\Microsoft.Owin.2.0.2\lib\net45\Microsoft.Owin.dll"""
#r """.\packages\Microsoft.Owin.Hosting.2.0.2\lib\net45\Microsoft.Owin.Hosting.dll"""
#r """.\packages\Owin.1.0\lib\net40\Owin.dll"""
#r """.\packages\Microsoft.AspNet.SignalR.Core.2.0.1\lib\net45\Microsoft.AspNet.SignalR.Core.dll"""
#r """.\packages\Microsoft.Owin.Cors.2.0.2\lib\net45\Microsoft.Owin.Cors.dll"""
#r """.\packages\Microsoft.Owin.Host.HttpListener.2.0.2\lib\net45\Microsoft.Owin.Host.HttpListener.dll"""
#r """.\packages\Microsoft.Owin.Security.2.0.2\lib\net45\Microsoft.Owin.Security.dll"""
#r """.\packages\Microsoft.AspNet.Cors.5.0.0\lib\net45\System.Web.Cors.dll"""
#r """.\bin\release\FsPlot.dll"""

open System
open FsPlot.Data
open FsPlot.Highcharts.Charting
open FsPlot.Highcharts.Options
open FsPlot.GenericDynamicChart

let series = Series.Area [1. .. 5.]
let area = DynamicHighcharts.Area series
area.SetTitle "Title"

#r "System.ComponentModel.Composition.dll"
#r @".\packages\Rx-Interfaces.2.2.2\lib\net45\System.Reactive.Interfaces.dll"
#r @".\packages\Rx-Core.2.2.2\lib\net45\System.Reactive.Core.dll"
#r @".\packages\Rx-Linq.2.2.2\lib\net45\System.Reactive.Linq.dll"

open System.Runtime
open System.Reactive.Concurrency
open System.Reactive.Linq

let rnd = Random()
let updates = Observable.Interval(TimeSpan.FromSeconds(1.0), Scheduler.Default)
updates.Subscribe(fun _ ->
    let dp = rnd.Next(1, 5)
    area.Push dp) |> ignore

// Warm up FunScript's compiler.
FunScript.Compiler.compile
    <@
        createEmpty<HighchartsChartOptions>() |> ignore
        Globals.Dollar.now()
    @>
|> ignore

module Area =
    
    let area1 =
        Series.Area [1000; 1170; 560; 1030]
        |> Highcharts.Area

//    area1.GetCategories()
//    let t = area1.GetTitle()
//    let xt = area1.GetXTitle()
//    let yt = area1.GetYTitle()
    area1.HideLegend()
    area1.SetInverted false
    area1.SetCategories ["2010"; "2011"; "2012"; "2013"]
    area1.SetData (Series.Area [100; 117; 56; 103])
    area1.SetData [Series.Area [150; 157; 96; 153]]
    area1.SetTooltip "<strong>test</strong>"
    area1.SetSubtitle "subtitle"
    area1.SetTitle "title"
    area1.SetXTitle "x-title"
    area1.SetYTitle "y-title"
    area1.ShowLegend()
    area1.SetStacking Normal
    area1.Close()

    let area2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Area

    let area3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Area
    
    let area4 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.Area

    let area5 =
        [
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        ]
        |> Highcharts.Area

    let area6 =
        [
            Series.Area ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Series.Area ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.Area

    let area7 =
        [
            Seq.ofList ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Seq.ofList ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.Area

module Areaspline =
    
    let areaspline1 =
        Series.Areaspline [1000; 1170; 560; 1030]
        |> Highcharts.Areaspline

    let areaspline2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Areaspline

    let areaspline3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Areaspline

    let areaspline4 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.Areaspline

    let areaspline5 =
        [
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        ]
        |> Highcharts.Areaspline

    let areaspline6 =
        [
            Series.Areaspline ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Series.Areaspline ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.Areaspline

    let areaspline7 =
        [
            Seq.ofList ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Seq.ofList ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.Areaspline

module Arearange =
    
    let arearange1 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> Series.Arearange
        |> Highcharts.Arearange

    let arearange2 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> Highcharts.Arearange

    let arearange3 =
        [
            [
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            ]
            [
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            ]
        ]
        |> Highcharts.Arearange

    let arearange4 =
        [
            [|
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            |]
            [|
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            |]
        ]
        |> Highcharts.Arearange

    let arearange5 =
        [
            Seq.ofArray [|
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            |]
            Seq.ofArray [|
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            |]
        ]
        |> Highcharts.Arearange

    let arearange6 =
        [
            Series.Arearange [|
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            |]
            Series.Arearange [|
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            |]
        ]
        |> Highcharts.Arearange

module Bar =
    
    let bar1 =
        Series.Bar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Bar

    let bar2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Bar

    let bar3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Bar

    let bar4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Bar

    let bar5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Bar

    let bar6 =
        [
            Series.Bar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Bar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Bar

    let bar7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Bar

module Bubble =
    
    let bubble1 =
        Series.Bubble [97,36,79; 94,74,60; 68,76,58]
        |> Highcharts.Bubble

    let bubble2 =
        [97,36,79; 94,74,60; 68,76,58]
        |> Highcharts.Bubble

    let bubble3 =
        [
            [36,79; 74,60; 76,58]
        ]
        |> Highcharts.Bubble

    let bubble4 =
        [
            [54,26,59; 64,74,60; 70,26,58]
            [97,36,79; 94,74,60; 68,76,58]
        ]
        |> Highcharts.Bubble

    let bubble5 =
        [
            [26,59; 74,60; 26,58]
            [36,79; 74,60; 76,58]
        ]
        |> Highcharts.Bubble

    let bubble6 =
        [
            [|54,26,59; 64,74,60; 70,26,58|]
            [|97,36,79; 94,74,60; 68,76,58|]
        ]
        |> Highcharts.Bubble

    let bubble7 =
        [
            [|26,59; 74,60; 26,58|]
            [|36,79; 74,60; 76,58|]
        ]
        |> Highcharts.Bubble

    let bubble8 =
        [
            Series.Bubble [|26,59; 74,60; 26,58|]
            Series.Bubble [|36,79; 74,60; 76,58|]
        ]
        |> Highcharts.Bubble

    let bubble9 =
        [
            Seq.ofArray [|54,26,59; 64,74,60; 70,26,58|]
            Seq.ofArray [|97,36,79; 94,74,60; 68,76,58|]
        ]
        |> Highcharts.Bubble

    let bubble10 =
        [
            Seq.ofArray [|26,59; 74,60; 26,58|]
            Seq.ofArray [|36,79; 74,60; 76,58|]
        ]
        |> Highcharts.Bubble

module Column =

    let column1 =
        Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Column

    let column2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Column

    let column3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Column

    let column4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Column

    let column5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Column

    let column6 =
        [
            Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Column ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Column

    let column7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Column

module Combination =

    let comb1 =
        [
            Series.Column [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Series.Line [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|] 
        ]
        |> Highcharts.Combine

    let comb2 =
        [
            Series.Column("Jane", [3; 2; 1; 3; 4])
            Series.Column("John", [2; 3; 5; 7; 6])
            Series.Column("Joe", [4; 3; 3; 9; 0])
            Series.Spline("Average", [3.; 2.67; 3.; 6.33; 3.33])
            Series.Pie("Total Consumption", ["Jane", 13; "John", 23; "Joe", 19])
        ]
        |> Highcharts.Combine

    comb2.SetCategories ["Apples"; "Oranges"; "Pears"; "Bananas"; "Plums"]
    comb2.SetPieOptions {Center = [|100; 80|]; Size = 100}

module Donut =

    let donut1 =
        Series.Donut ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Donut

    let donut2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Donut

    let donut3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Donut

module Funnel =

    let funnel1 =
        Series.Funnel ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Funnel

    let funnel2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Funnel

    let funnel3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Funnel

module Line =

    let line1 =
        Series.Line ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Line

    let line2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Line

    let line3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Line

    let line4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Line

    let line5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Line

    let line6 =
        [
            Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Column ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Line

    let line7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Line

module PercentArea =
    
    let area1 =
        Series.PercentArea [1000; 1170; 560; 1030]
        |> Highcharts.PercentArea

    let area2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.PercentArea

    let area3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.PercentArea
    
    let area4 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.PercentArea

    let area5 =
        [
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        ]
        |> Highcharts.PercentArea

    let area6 =
        [
            Series.PercentArea ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Series.PercentArea ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.PercentArea

    let area7 =
        [
            Seq.ofList ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Seq.ofList ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.PercentArea

module PercentBar =
    
    let bar1 =
        Series.PercentBar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.PercentBar

    let bar2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.PercentBar

    let bar3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.PercentBar

    let bar4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.PercentBar

    let bar5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.PercentBar

    let bar6 =
        [
            Series.PercentBar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.PercentBar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.PercentBar

    let bar7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.PercentBar

module PercentColumn =

    let column1 =
        Series.PercentColumn ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.PercentColumn

    let column2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.PercentColumn

    let column3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.PercentColumn

    let column4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.PercentColumn

    let column5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.PercentColumn

    let column6 =
        [
            Series.PercentColumn ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.PercentColumn ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.PercentColumn

    let column7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.PercentColumn

module Pie =

    let pie1 =
        Series.Pie ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Pie

    let pie2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Pie

    let pie3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Pie

module Radar =

    let radar1 =
        Series.Radar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Radar

    let radar2 =
        [1000; 1170; 560; 1030; 1250]
        |> Highcharts.Radar

    let radar3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Radar

    let radar4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Radar

    let radar5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Radar

    let radar6 =
        [
            Series.Radar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Radar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Radar

    let radar7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Radar

module Scatter =

    let scatter1 =
        Series.Scatter ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Scatter

    let scatter2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Scatter

    let scatter3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Scatter

    let scatter4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Scatter

    let scatter5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Scatter

    let scatter6 =
        [
            Series.Scatter ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Scatter ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Scatter

    let scatter7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Scatter

module Spline =

    let spline1 =
        Series.Spline ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Spline

    let spline2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Spline

    let spline3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Spline

    let spline4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Spline

    let spline5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Spline

    let spline6 =
        [
            Series.Spline ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Spline ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Spline

    let spline7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Spline

module StackedArea =
    
    let area1 =
        Series.StackedArea [1000; 1170; 560; 1030]
        |> Highcharts.StackedArea

    let area2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.StackedArea

    let area3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.StackedArea
    
    let area4 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.StackedArea

    let area5 =
        [
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        ]
        |> Highcharts.StackedArea

    let area6 =
        [
            Series.StackedArea ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Series.StackedArea ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.StackedArea

    let area7 =
        [
            Seq.ofList ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Seq.ofList ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Highcharts.StackedArea

module StackedBar =
    
    let bar1 =
        Series.StackedBar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.StackedBar

    let bar2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.StackedBar

    let bar3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.StackedBar

    let bar4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.StackedBar

    let bar5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.StackedBar

    let bar6 =
        [
            Series.StackedBar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.StackedBar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.StackedBar

    let bar7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.StackedBar

module StackedColumn =

    let column1 =
        Series.StackedColumn ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.StackedColumn

    let column2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.StackedColumn

    let column3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.StackedColumn

    let column4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.StackedColumn

    let column5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.StackedColumn

    let column6 =
        [
            Series.StackedColumn ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.StackedColumn ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.StackedColumn

    let column7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.StackedColumn