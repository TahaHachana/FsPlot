Highcharts Basic Radar
======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicRadar =
    let allocated = Series.Radar("Allocated Budget", [43000; 19000; 60000; 35000; 17000; 10000])
    let actual = Series.Radar("Actual Spending", [50000; 39000; 42000; 31000; 26000; 14000])
    Highcharts.Radar [allocated; actual]

basicRadar.Categories <- ["Sales"; "Marketing"; "Development"; "Customer Support"; "Information Technology"; "Administration"]
basicRadar.SetPointFormat """<span style="color:{series.color}">{series.name}: <b>${point.y:,.0f}</b><br/>"""
basicRadar.ShowLegend()
basicRadar.SetTitle "Budget VS Spending"
```
Chart
-----

![Highcharts Basic Radar](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicRadar.PNG)