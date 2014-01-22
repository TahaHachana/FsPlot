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

let area = DynamicHighcharts.Area series

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

let rnd = Random()
let next() = rnd.Next(1, 10)

module Area =

    let area1 =
        Series.Area [next(); next(); next(); next()]
        |> DynamicHighcharts.Area

    let updates1 = Observable.Interval(TimeSpan.FromSeconds(1.0), Scheduler.Default)
    updates1.Subscribe(fun _ -> area1.Push <| next()) |> ignore

    area1.Close()
    
    let area2 =
        [next(); next(); next(); next()]
        |> DynamicHighcharts.Area

    let updates2 = Observable.Interval(TimeSpan.FromSeconds(1.0), Scheduler.Default)
    updates2.Subscribe(fun _ -> area2.Push <| next()) |> ignore
    area2.Close()

    let area3 =
        [
            5.
            3.
            10.
        ]
        |> DynamicHighcharts.Area

    area3.SetShift false

    let updates3 = Observable.Interval(TimeSpan.FromSeconds(2.0), Scheduler.Default)
    updates3.Subscribe(fun _ -> area3.Push (float <| next())) |> ignore
    area3.Close()

module Areaspline =

    let area1 =
        Series.Areaspline [next(); next(); next(); next()]
        |> DynamicHighcharts.Areaspline

    let updates1 = Observable.Interval(TimeSpan.FromSeconds(1.0), Scheduler.Default)
    updates1.Subscribe(fun _ -> area1.Push <| next()) |> ignore

    area1.Close()

    let area2 =
        [next(); next(); next(); next()]
        |> DynamicHighcharts.Areaspline

    let updates2 = Observable.Interval(TimeSpan.FromSeconds(1.0), Scheduler.Default)
    updates2.Subscribe(fun _ -> area2.Push <| next()) |> ignore

    area2.Close()

    let area3 =
        [
            5.
            3.
            10.
        ]
        |> DynamicHighcharts.Area

    area3.SetShift false

    let updates3 = Observable.Interval(TimeSpan.FromSeconds(2.0), Scheduler.Default)
    updates3.Subscribe(fun _ -> area3.Push (float <| next())) |> ignore
    area3.Close()
