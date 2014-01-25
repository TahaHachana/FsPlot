Highcharts Dynamic Areaspline
===========================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let sales =
    ["2010", 1300; "2011", 1470; "2012", 840; "2013", 1330]
    |> Series.Areaspline
    |> Series.SetName "Sales"

let chart =
    DynamicChart.plot sales
    |> DynamicChart.showLegend
    |> DynamicChart.title "Company Performance"

chart.Push ("2014", 879)
//chart.Close()
```