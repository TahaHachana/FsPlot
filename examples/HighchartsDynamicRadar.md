Highcharts Dynamic Radar
======================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let allocated = Series.Radar("Allocated Budget", [43000; 19000; 60000; 35000; 17000])

let radar =
    DynamicChart.plot allocated
    |> DynamicChart.categories ["Sales"; "Marketing"; "Development"; "Customer Support"; "Information Technology"]
    |> DynamicChart.tooltip """<span style="color:{series.color}">{series.name}: <b>${point.y:,.0f}</b><br/>"""
    |> DynamicChart.showLegend
    |> DynamicChart.title "Budget VS Spending"

radar.Push ("Administration", 10000)
//radar.Close()
```