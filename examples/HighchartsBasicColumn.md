Highcharts Basic Column
=======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicColumn =
    [
        Series.Column("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
        Series.Column("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
    ]
    |> Highcharts.Column

basicColumn.ShowLegend()
basicColumn.SetTitle "Company Performance"
```
Chart
-----

![Highcharts Basic Column](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicColumn.PNG)