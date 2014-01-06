Highcharts Inverted Axes Area
=============================

```fsharp
#load "FsPlot.fsx"

let jane =
    ["Monday", 4; "Tuesday", 3; "Wednesday", 5; "Thursday", 4; "Friday", 3; "Saturday", 12; "Sunday", 9]
    |> Series.Area
    |> Series.SetName "Jane"

let john =
    ["Monday", 3; "Tuesday", 4; "Wednesday", 3; "Thursday", 5; "Friday", 7; "Saturday", 10; "Sunday", 12]
    |> Series.Area
    |> Series.SetName "John"

let invertedAxesArea =
    Highcharts.plot [john; jane]
    |> Highcharts.showLegend
    |> Highcharts.title "Average Fruit Consumption"
    |> Highcharts.yTitle "Number of Units"
    :?> HighchartsArea
    |> fun x -> x.SetInverted true
```
![Highcharts Inverted Axes Area](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsInvertedAxesArea.PNG)