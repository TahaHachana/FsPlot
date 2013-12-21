Highcharts Basic Combination
============================

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let basicComb =
    [
        Series.Column("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
        Series.Line("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
    ]
    |> Highcharts.Combine

basicComb.SetTitle "Company Performance"
```
![Highcharts Basic Combination](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicComb.PNG)