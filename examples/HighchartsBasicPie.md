Highcharts Basic Pie
====================

Code
----

```fsharp
#load "FsPlot.fsx"

let basicPie =
    Series.Pie ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
    |> Series.SetName "Browser Share"
    |> Highcharts.plot
    |> Highcharts.showLegend
    |> Highcharts.title "Website Visitors By Browser"
    |> Highcharts.tooltip """<span style="color:{series.color}">{series.name}: <b>{point.percentage:.1f}%</b><br/>"""
```
Chart
-----

![Highcharts Basic Pie](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicPie.PNG)