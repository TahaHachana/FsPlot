Highcharts Basic Pie
====================

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

    let basicPie =
        Series.Pie("Browser Share", ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.])
        |> Highcharts.Pie

    basicPie.ShowLegend()        
    basicPie.SetTitle "Website Visitors By Browser"
```
![Highcharts Basic Pie](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicPie.PNG)