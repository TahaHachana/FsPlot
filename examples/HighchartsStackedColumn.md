Highcharts Stacked Column
=========================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries
open FsPlot.Options

let stackedColumn =
    let joe = Series.Column("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Column("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Column("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])
    Highcharts.Column [joe; jane; john]

stackedColumn.ShowLegend()
stackedColumn.SetYTitle "Total Fruit Consumption"
stackedColumn.SetStacking Stacking.Normal
```
Chart
-----

![Highcharts Stacked Column](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsStackedColumn.PNG)