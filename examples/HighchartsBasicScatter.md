Highcharts Basic Scatter
========================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicScatter =
    [8., 12.; 4., 5.5; 11., 14.; 4., 5.; 3., 3.5; 6.5, 7.]
    |> Series.Scatter
    |> Series.SetName "AgeVsWeight"
    |> Chart.plot
    |> Chart.title "Age vs. Weight comparison"
    |> Chart.xTitle "Age"
    |> Chart.yTitle "Weight"
```
Chart
-----

![Highcharts Basic Scatter](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicScatter.PNG)