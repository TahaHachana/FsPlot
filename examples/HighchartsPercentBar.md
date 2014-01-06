Highcharts Percent Bar
======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.DataSeries
open FsPlot.Highcharts

let joe = Series.PercentBar("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
let jane = Series.PercentBar("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
let john = Series.PercentBar("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

let percentdBar =
    Highcharts.plot [joe; jane; john]
    |> Highcharts.showLegend
    |> Highcharts.yTitle "Total Fruit Consumption"
    |> Highcharts.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%</b><br/>"""
```
Chart
-----

![Highcharts Percent Bar](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsPercentBar.PNG)