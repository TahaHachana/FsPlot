Highcharts Percent Column
=========================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries
open FsPlot.Options

let joe = Series.Column("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
let jane = Series.Column("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
let john = Series.Column("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

let percentColumn =
    Chart.plot [joe; jane; john]
    |> Chart.showLegend
    |> Chart.yTitle "% of Fruit Consumption"
    |> Chart.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%<br/>"""
    :?> HighchartsColumn
    |> fun x -> x.SetStacking Stacking.Percent
```
Chart
-----

![Highcharts Percent Column](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsPercentColumn.PNG)