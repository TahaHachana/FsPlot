Highcharts Basic Bubble
=======================

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicBubble =
    [
        [(97,36,79); (94,74,60); (68,76,58); (64,87,56)]
        [(68,27,73); (74,99,42); (7,93,87); (51,69,40)]
    ]
    |> Highcharts.Bubble
```
![Highcharts Basic Bubble](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicBubble.PNG)