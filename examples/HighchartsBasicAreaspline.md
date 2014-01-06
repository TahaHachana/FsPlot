Highcharts Basic Areaspline
===========================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.DataSeries
open FsPlot.Highcharts

let sales =
    ["2010", 1300; "2011", 1470; "2012", 840; "2013", 1330]
    |> Series.Areaspline
    |> Series.SetName "Sales"

let expenses =
    ["2010", 1000; "2011", 1170; "2012", 580; "2013", 1030]
    |> Series.Areaspline
    |> Series.SetName "Expenses"

let basicAreaspline =
    [sales; expenses]
    |> Highcharts.plot
    |> Highcharts.showLegend
    |> Highcharts.title "Company Performance"
```
Chart
-----

![Highcharts Basic Areaspline](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicAreaspline.PNG)