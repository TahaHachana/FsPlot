Highcharts Dynamic Line
=======================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let line =
    Series.Line("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
    |> DynamicChart.plot
    |> DynamicChart.showLegend
    |> DynamicChart.title "Company Performance"

line.Push ("2014", 874)
//line.Close()
```