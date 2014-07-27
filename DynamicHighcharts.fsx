#r """.\packages\FunScript.1.1.50\lib\net40\FunScript.dll"""
#r @"C:\Users\AHMED\Documents\GitHub\FsPlot\packages\FunScript.1.1.50\lib\net40\FunScript.Interop.dll"
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\packages\FunScript.TypeScript.Binding.signalr.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.signalr.dll"""
#r """C:\Users\AHMED\Documents\GitHub\FsPlot\packages\FunScript.TypeScript.Binding.gapi.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.gapi.dll"""
#r """C:\Users\AHMED\Documents\GitHub\FsPlot\packages\FunScript.TypeScript.Binding.google_visualization.1.1.0.37\lib\net40\FunScript.TypeScript.Binding.google_visualization.dll"""
#r """.\packages\Microsoft.AspNet.Cors.5.1.0\lib\net45\System.Web.Cors.dll"""
#r """.\packages\Microsoft.Owin.2.1.0\lib\net45\Microsoft.Owin.dll"""
#r """.\packages\Microsoft.Owin.Hosting.2.1.0\lib\net45\Microsoft.Owin.Hosting.dll"""
#r """.\packages\Microsoft.Owin.Security.2.1.0\lib\net45\Microsoft.Owin.Security.dll"""
#r """.\packages\Owin.1.0\lib\net40\Owin.dll"""
#r """.\bin\release\FsPlot.dll"""

// RX
#r "System.ComponentModel.Composition.dll"
#r @".\packages\Rx-Interfaces.2.2.2\lib\net45\System.Reactive.Interfaces.dll"
#r @".\packages\Rx-Core.2.2.2\lib\net45\System.Reactive.Core.dll"
#r @".\packages\Rx-Linq.2.2.2\lib\net45\System.Reactive.Linq.dll"

open System
open FsPlot.Data
open FsPlot.Highcharts.Charting
open FsPlot.Highcharts.Options
open FsPlot.GenericDynamicChart
open System.Runtime
open System.Reactive.Concurrency
open System.Reactive.Linq

let series = Series.Area [1. .. 5.]

let area = DynamicHighcharts.Area(series, shift = true)
area.Push 6.

area.ShowLegend()
area.HideLegend()
area.SetCategories ["2010"; "2011"; "2012"; "2013"]
area.SetData (Series.Area [100; 117; 56; 103])
area.SetTooltip "<strong>test</strong>"
area.SetSubtitle "subtitle"
area.SetTitle "title"
area.SetXTitle "x-title"
area.SetYTitle "y-title"
area.Close()

module Area =

    let area1 =
        Series.Area [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Area

    area1.Push 1423
    area1.Close()
    
    let area2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Area

    area2.Push 985
    area2.Close()

    let area3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Area

    area3.Push ("2014", 1200)
    area3.Close()

module Areaspline =

    let areaspline1 =
        Series.Areaspline [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Areaspline

    areaspline1.Push 1255
    areaspline1.Close()

    let areaspline2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Areaspline

    areaspline2.Push 678
    areaspline2.Close()

    let areaspline3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Areaspline

    areaspline3.Push ("2014", 943)
    areaspline3.Close()

module Arearange =

    let arearange1 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> Series.Arearange
        |> DynamicHighcharts.Arearange
    
    arearange1.Push(DateTime.Now.AddDays 3., 4., 10.)
    arearange1.Close()

    let arearange2 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> DynamicHighcharts.Arearange

    arearange2.Push(DateTime.Now.AddDays 3., 4., 10.)
    arearange2.Close()

module Bar =
    
    let bar1 =
        Series.Bar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Bar
    
    bar1.Push("2014", 1200)
    bar1.Close()

    let bar2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Bar

    bar2.Push 854
    bar2.Close()

    let bar3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Bar

    bar3.Push ("2014", 752)
    bar3.Close()

module Bubble =
    
    let bubble1 =
        Series.Bubble [97,36,79; 94,74,60; 68,76,58]
        |> DynamicHighcharts.Bubble

    bubble1.Push(50,72,48)
    bubble1.Close()

    let bubble2 =
        [97,36,79; 94,74,60; 68,76,58]
        |> DynamicHighcharts.Bubble

    bubble2.Push(50,72,48)
    bubble2.Close()

    let bubble3 =
        [36,79; 74,60; 76,58]
        |> DynamicHighcharts.Bubble

    bubble3.Push(50,72)
    bubble3.Close()


module Column =

    let column1 =
        Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Column

    column1.Push("2014", 1400)
    column1.Close()

    let column2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Column

    column2.Push 649
    column2.Close()

    let column3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Column

    column3.Push("2014", 1320)
    column3.Close()

module Donut =

    let donut1 =
        Series.Donut ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Donut

    donut1.Push ("2014", 1400)
    donut1.Close()

    let donut2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Donut

    donut2.Push 547
    donut2.Close()

    let donut3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Donut

    donut3.Push ("2014", 1400)
    donut3.Close()

module Funnel =

    let funnel1 =
        Series.Funnel ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Funnel

    funnel1.Push ("2014", 1400)
    funnel1.Close()

    let funnel2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Funnel

    funnel2.Push 841
    funnel2.Close()

    let funnel3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Funnel

    funnel3.Push ("2014", 1400)
    funnel3.Close()

module Line =

    let line1 =
        Series.Line ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Line
    
    line1.Push ("2014", 1400)
    line1.Close()

    let line2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Line

    line2.Push 456
    line2.Close()

    let line3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Line

    line3.Push ("2014", 1400)
    line3.Close()

module Pie =

    let pie1 =
        Series.Pie ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Pie

    pie1.Push("2014", 1440)
    pie1.Close()

    let pie2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Pie

    pie2.Push 578
    pie2.Close()

    let pie3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Pie

    pie3.Push("2014", 1440)
    pie3.Close()

module Radar =

    let radar1 =
        Series.Radar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Radar
    
    radar1.Push ("2014", 1400)
    radar1.Close()

    let radar2 =
        [1000; 1170; 560; 1030; 1250]
        |> DynamicHighcharts.Radar

    radar2.Push 547
    radar2.Close()

    let radar3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Radar

    radar3.Push ("2014", 1400)
    radar3.Close()

module Scatter =

    let scatter1 =
        Series.Scatter ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Scatter
    
    scatter1.Push ("2014", 1400)
    scatter1.Close()

    let scatter2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Scatter

    scatter2.Push 521
    scatter2.Close()

    let scatter3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Scatter

    scatter3.Push ("2014", 1400)
    scatter3.Close()

module Spline =

    let spline1 =
        Series.Spline ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Spline

    spline1.Push ("2014", 1400)
    spline1.Close()

    let spline2 =
        [1000; 1170; 560; 1030]
        |> DynamicHighcharts.Spline

    spline2.Push 675
    spline2.Close()

    let spline3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> DynamicHighcharts.Spline

    spline3.Push ("2014", 1400)
    spline3.Close()
