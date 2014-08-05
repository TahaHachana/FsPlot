Highcharts Dynamic Funnel
=========================

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let funnel =
    [
        "Website visits", 15654
        "Downloads", 4064
        "Requested price list", 1987
        "Invoice sent", 976
    ]
    |> Series.Funnel
    |> Series.SetName "Unique users"
    |> DynamicChart.plot
    |> DynamicChart.title "Sales Funnel"

funnel.Push ("Finalized", 846)
//funnel.Close()
```