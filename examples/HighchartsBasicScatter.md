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
    |> Highcharts.Scatter

basicScatter.SetTitle "Age vs. Weight comparison"
basicScatter.SetXTitle "Age"
basicScatter.SetYTitle "Weight"
```
Chart
-----

![Highcharts Basic Scatter](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicScatter.PNG)