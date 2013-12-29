Highcharts Basic Donut
======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicDonut =
    ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
    |> Series.Donut
    |> Series.SetName "Browser Share"
    |> Chart.plot
    |> Chart.title "Website Visitors By Browser"
    |> Chart.tooltip """<span style="color:{series.color}>{series.name}</span>: <b>{point.percentage:.1f}%</b><br/>"""
    |> Chart.showLegend
```
Chart
-----

![Highcharts Basic Donut](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicDonut.PNG)