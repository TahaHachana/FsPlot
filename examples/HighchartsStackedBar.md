Highcharts Stacked Bar
======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries
open FsPlot.Options

let stackedBar =
    let joe = Series.Bar("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Bar("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Bar("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])
    Highcharts.Bar [joe; jane; john]

stackedBar.ShowLegend()
stackedBar.SetYTitle "Total Fruit Consumption"
stackedBar.SetStacking Stacking.Normal
```
Chart
-----

![Highcharts Stacked Bar](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsStackedBar.PNG)