Highcharts Inverted Axes Area
=============================

```fsharp
#load "FsPlot.fsx"

open FsPlot.Options
open FsPlot.Charting
open FsPlot.DataSeries

let invertedAxesArea =
    let jane =
        ["Monday", 4; "Tuesday", 3; "Wednesday", 5; "Thursday", 4; "Friday", 3; "Saturday", 12; "Sunday", 9]
        |> Series.Area
        |> Series.SetName "Jane"
    let john =
        ["Monday", 3; "Tuesday", 4; "Wednesday", 3; "Thursday", 5; "Friday", 7; "Saturday", 10; "Sunday", 12]
        |> Series.Area
        |> Series.SetName "John"
    Highcharts.Area(
        [john; jane],
        legend = true,
        title = "Average Fruit Consumption",
        yTitle = "Number of Units")

invertedAxesArea.Inverted <- true
```
![Highcharts Inverted Axes Area](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/InvertedAxesArea.PNG)