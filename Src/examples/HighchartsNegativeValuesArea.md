Highcharts Area with Negative Values
====================================

Code
----

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

let john =
    ["Apples", 5; "Oranges", 3; "Pears", 4; "Grapes", 7; "Bananas", 2]
    |> Series.Area
    |> Series.SetName "John"

let jane =
    ["Apples", 2; "Oranges", -3; "Pears", -2; "Grapes", 2; "Bananas", 1]
    |> Series.Area
    |> Series.SetName "Jane"

let joe =
    ["Apples", 3; "Oranges", 3; "Pears", 4; "Grapes", -5; "Bananas", -2]
    |> Series.Area
    |> Series.SetName "Joe"

let negativeValuesArea =
    Chart.plot [john; jane; joe]
    |> Chart.showLegend
    |> Chart.title "Area Chart with Negative Values"
```
Chart
-----

![Highcharts Area with Negative Values](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsNegativeValuesArea.PNG)