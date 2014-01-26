Highcharts Dynamic Donut
========================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let donut =
    ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2]
    |> Series.Donut
    |> Series.SetName "Browser Share"
    |> DynamicChart.plot
    |> DynamicChart.title "Website Visitors By Browser"
    |> DynamicChart.tooltip """<span style="color:{series.color}>{series.name}</span>: <b>{point.percentage:.1f}%</b><br/>"""
    |> DynamicChart.showLegend

donut.Push ("Others", 9.)
//donut.Close()
```