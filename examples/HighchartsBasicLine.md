Highcharts Basic Line
=====================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicLine =
    [
        Series.Line("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
        Series.Line("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
    ]
    |> Chart.plot
    |> Chart.showLegend
    |> Chart.title "Company Performance"
```
Chart
-----

![Highcharts Basic Line](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicLine.PNG)