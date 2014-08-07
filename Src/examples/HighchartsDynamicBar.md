Highcharts Dynamic Bar
======================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let bar =
    Series.Bar("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
    |> DynamicChart.plot
    |> DynamicChart.showLegend
    |> DynamicChart.title "Company Performance"

bar.Push ("2014", 785)
//bar.Close()
```