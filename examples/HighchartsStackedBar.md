Highcharts Stacked Bar
======================

Code
----

```fsharp
#load "FsPlot.fsx"

let joe = Series.StackedBar("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
let jane = Series.StackedBar("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
let john = Series.StackedBar("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

let stackedBar =
    Highcharts.plot [joe; jane; john]
    |> Highcharts.showLegend
    |> Highcharts.yTitle "Total Fruit Consumption"
```
Chart
-----

![Highcharts Stacked Bar](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsStackedBar.PNG)