Highcharts Basic Scatter
========================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.DataSeries
open FsPlot.Highcharts

let basicScatter =
    [8., 12.; 4., 5.5; 11., 14.; 4., 5.; 3., 3.5; 6.5, 7.]
    |> Series.Scatter
    |> Series.SetName "AgeVsWeight"
    |> Highcharts.plot
    |> Highcharts.title "Age vs. Weight comparison"
    |> Highcharts.xTitle "Age"
    |> Highcharts.yTitle "Weight"
```
Chart
-----

![Highcharts Basic Scatter](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicScatter.PNG)