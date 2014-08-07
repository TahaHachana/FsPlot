Highcharts Basic Line
=====================

Code
----

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

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

![Highcharts Basic Line](https://raw.github.com/TahaHachana/FsPlot/master/Src/screenshots/HighchartsBasicLine.PNG)