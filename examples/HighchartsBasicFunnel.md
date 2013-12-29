Highcharts Basic Funnel
=======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicFunnel =
    [
        "Website visits", 15654
        "Downloads", 4064
        "Requested price list", 1987
        "Invoice sent", 976
        "Finalized", 846
    ]
    |> Series.Funnel
    |> Series.SetName "Unique users"
    |> Chart.plot
    |> Chart.title "Sales Funnel"
```
Chart
-----

![Highcharts Basic Funnel](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicFunnel.PNG)