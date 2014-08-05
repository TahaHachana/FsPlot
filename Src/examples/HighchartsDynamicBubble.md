Highcharts Dynamic Bubble
=========================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let bubble =
    [(97, 36, 79); (94, 74, 60); (68, 76, 58); (64, 87, 56); (68, 27, 73);
    (74, 99, 42); (7, 93, 87); (51, 69, 40); (38, 23, 33); (57, 86, 31)]
    |> Series.Bubble
    |> DynamicChart.plot

bubble.Push (54, 74, 64)
//bubble.Close()
```