Highcharts Basic Area Range
===========================

Code
----

```fsharp
#load "FsPlotInit.fsx"

open System
open FsPlot.Data
open FsPlot.Highcharts.Charting

let tempratures =
    let rnd = Random()
    [0. .. 6.]
    |> List.map(fun x ->
        DateTime.Now.AddDays x, rnd.Next(-5, -1), rnd.Next(4, 8))
    |> Series.Arearange
    |> Series.SetName "Tempratures"

let basicArearange =
    Chart.plot tempratures
    |> Chart.title "Temprature Variation"
```
Chart
-----

![Highcharts Basic Arearange](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicArearange.PNG)