Highcharts Basic Spline
=======================

Code
----

```fsharp
#load "FsPlot.fsx"

let basicSpline =
    [
        Series.Spline("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
        Series.Spline("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
    ]
    |> Highcharts.plot
    |> Highcharts.showLegend
    |> Highcharts.title "Company Performance"
```
Chart
-----

![Highcharts Basic Spline](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicSpline.PNG)