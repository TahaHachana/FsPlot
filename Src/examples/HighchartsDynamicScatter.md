Highcharts Dynamic Scatter
==========================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let scatter =
    [8., 12.; 4., 5.5; 11., 14.; 4., 5.; 3., 3.5]
    |> Series.Scatter
    |> Series.SetName "AgeVsWeight"
    |> DynamicChart.plot
    |> DynamicChart.title "Age vs. Weight comparison"
    |> DynamicChart.xTitle "Age"
    |> DynamicChart.yTitle "Weight"

scatter.Push (6.5, 7.)
//scatter.Close()
```