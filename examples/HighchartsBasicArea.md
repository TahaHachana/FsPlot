Highcharts Basic Area
=====================

Code
----

```fsharp
#load "FsPlot.fsx"

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
    |> Highcharts.plot
    |> Highcharts.showLegend
    |> Highcharts.title "Company Performance"
```
Chart
-----

![Highcharts Basic Area](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicArea.PNG)