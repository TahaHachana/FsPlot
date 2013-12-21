Highcharts Basic Area Range
===========================

Code
----

```fsharp
#load "FsPlot.fsx"

open System
open FsPlot.Charting
open FsPlot.DataSeries

let basicArearange =
    let data =
        let rnd = Random()
        [0. .. 6.]
        |> List.map(fun x ->
            DateTime.Now.AddDays x, rnd.Next(-5, -1), rnd.Next(4, 8))
        |> Series.Arearange
        |> Series.SetName "Tempratures"
    Highcharts.Arearange(data, title = "Temprature Variation")
```
Chart
-----

![Highcharts Area](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsAreaRange.PNG)