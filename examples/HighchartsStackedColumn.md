Highcharts Stacked Column
=========================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.DataSeries
open FsPlot.Highcharts

let joe = Series.StackedColumn("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
let jane = Series.StackedColumn("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
let john = Series.StackedColumn("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

let stackedColumn =
    Highcharts.plot [joe; jane; john]
    |> Highcharts.showLegend
    |> Highcharts.yTitle "Total Fruit Consumption"
```
Chart
-----

![Highcharts Stacked Column](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsStackedColumn.PNG)