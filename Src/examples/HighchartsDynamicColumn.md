Highcharts Dynamic Column
=======================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let column =
    Series.Column("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
    |> DynamicChart.plot
    |> DynamicChart.title "Company Performance"
    |> DynamicChart.showLegend

column.Push ("2014", 862)
//column.Close()
```