Highcharts Dynamic Spline
=========================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let spline =
    Series.Spline("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
    |> DynamicChart.plot
    |> DynamicChart.showLegend
    |> DynamicChart.title "Company Performance"

spline.Push ("2014", 756)
//spline.Close()
```