Highcharts Percent Column
=========================

Code
----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries
open FsPlot.Options

let percentColumn =
    let joe = Series.Column("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Column("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Column("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])
    Highcharts.Column [joe; jane; john]

percentColumn.ShowLegend()
percentColumn.SetYTitle "% of Fruit Consumption"
percentColumn.SetStacking Stacking.Percent
percentColumn.SetPointFormat "<span style='color:{series.color}'>{series.name}</span>: <b>{point.percentage:.1f}%<br/>"
```
Chart
-----

![Highcharts Percent Column](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsPercentColumn.PNG)