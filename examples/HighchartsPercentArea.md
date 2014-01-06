Highcharts Percent Area
=======================

Code
----

```fsharp
#load "FsPlot.fsx"

let asia = Series.PercentArea("Asia", [502; 635; 809; 947; 1402; 3634; 5268])
let africa = Series.PercentArea("Africa", [106; 107; 111; 133; 221; 767; 1766])
let europe = Series.PercentArea("Europe", [163; 203; 276; 408; 547; 729; 628])
let america = Series.PercentArea("America", [18; 31; 54; 156; 339; 818; 1201])
let oceania = Series.PercentArea("Oceania", [2; 2; 2; 6; 13; 30; 46])

let percentArea =
    Highcharts.plot [asia; africa; europe; america; oceania]
    |> Highcharts.categories  ["1750"; "1800"; "1850"; "1900"; "1950"; "1999"; "2050"]
    |> Highcharts.showLegend
    |> Highcharts.title "Historic and Estimated Worldwide Population Growth"
    |> Highcharts.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%</b> ({point.y:,.0f} millions)<br/>"""
```
Chart
-----

![Highcharts Percent Area](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsPercentArea.PNG)