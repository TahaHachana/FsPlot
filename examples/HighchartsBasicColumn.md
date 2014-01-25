Highcharts Basic Column
=======================

Code
----

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let basicColumn =
    [
        Series.Column("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
        Series.Column("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
    ]
    |> Chart.plot
    |> Chart.title "Company Performance"
    |> Chart.showLegend
```
Chart
-----

![Highcharts Basic Column](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicColumn.PNG)