Highcharts Basic Area
=====================

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let negativeValuesArea =
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
    Highcharts.Area [john; jane; joe]

negativeValuesArea.ShowLegend() 
    
negativeValuesArea.SetTitle "Area Chart with Negative Values"
```
![Highcharts Area](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/NegativeValuesArea.PNG)