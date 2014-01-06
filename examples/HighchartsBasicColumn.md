Highcharts Basic Column
=======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.DataSeries
open FsPlot.Highcharts

let basicColumn =
    [
        Series.Column("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
        Series.Column("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
    ]
    |> Highcharts.plot
    |> Highcharts.title "Company Performance"
    |> Highcharts.showLegend
```
Chart
-----

![Highcharts Basic Column](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicColumn.PNG)