Highcharts Dynamic Pie
======================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let pie =
    ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2]
    |> Series.Pie
    |> Series.SetName "Browser Share"
    |> DynamicChart.plot
    |> DynamicChart.title "Website Visitors By Browser"
    |> DynamicChart.tooltip """<span style="color:{series.color}>{series.name}</span>: <b>{point.percentage:.1f}%</b><br/>"""
    |> DynamicChart.showLegend

pie.Push ("Others", 9.)
//pie.Close()
```