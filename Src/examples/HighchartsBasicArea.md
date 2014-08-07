Highcharts Basic Area
=====================

Code
----

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let sales =
    ["2010", 1300; "2011", 1470; "2012", 840; "2013", 1330]
    |> Series.Area
    |> Series.SetName "Sales"

let expenses =
    ["2010", 1000; "2011", 1170; "2012", 580; "2013", 1030]
    |> Series.Area
    |> Series.SetName "Expenses"

let basicArea =
    [sales; expenses]
    |> Chart.plot
    |> Chart.showLegend
    |> Chart.title "Company Performance"
```
Chart
-----

![Highcharts Basic Area](https://raw.github.com/TahaHachana/FsPlot/master/Src/screenshots/HighchartsBasicArea.PNG)