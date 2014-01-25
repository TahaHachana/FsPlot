Highcharts Dynamic Area Range
=============================

```fsharp
#load "FsPlotInit.fsx"

open System
open FsPlot.Data
open FsPlot.Highcharts.Charting

let rnd = Random()

let tempratures =
    [0. .. 6.]
    |> List.map(fun x ->
        DateTime.Now.AddDays x, rnd.Next(-5, -1), rnd.Next(4, 8))
    |> Series.Arearange
    |> Series.SetName "Tempratures"

let arearange =
    DynamicChart.plot tempratures
    |> DynamicChart.title "Temprature Variation"

arearange.Push (DateTime.Now.AddDays 7., rnd.Next(-5, -1), rnd.Next(4, 8))
//arearange.Close()
```