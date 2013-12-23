Highcharts Percent Bar
======================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries
open FsPlot.Options

let percentBar =
    let joe = Series.Bar("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Bar("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Bar("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])
    Highcharts.Bar [joe; jane; john]

percentBar.ShowLegend()
percentBar.SetYTitle "% of Fruit Consumption"
percentBar.SetStacking Stacking.Percent
percentBar.SetPointFormat "<span style='color:{series.color}'>{series.name}</span>: <b>{point.percentage:.1f}%<br/>"
```
Chart
-----

![Highcharts Percent Bar](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsPercentBar.PNG)