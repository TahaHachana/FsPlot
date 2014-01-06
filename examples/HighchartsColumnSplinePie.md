Highcharts Column Spline Pie Combination
========================================

Code
----

```fsharp
#load "FsPlot.fsx"

let columnSplinePie =
    [
        Series.Column("Jane", [3; 2; 1; 3; 4])
        Series.Column("John", [2; 3; 5; 7; 6])
        Series.Column("Joe", [4; 3; 3; 9; 0])
        Series.Spline("Average", [3.; 2.67; 3.; 6.33; 3.33])
        Series.Pie("Total Consumption", ["Jane", 13; "John", 23; "Joe", 19])
    ]
    |> Highcharts.plot
    |> Highcharts.categories ["Apples"; "Oranges"; "Pears"; "Bananas"; "Plums"]
    :?> HighchartsCombination
    |> fun x -> x.SetPieOptions {Center = [|100; 80|]; Size = 100}
```
Chart
-----

![Highcharts Column Spline Pie Combination](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsColumnSplinePie.PNG)