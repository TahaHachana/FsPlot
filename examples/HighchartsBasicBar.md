Highcharts Basic Scatter
========================

```fsharp
let basicScatter =
    [
        Series.Scatter("Female", [(161.2, 51.6); (167.5, 59.0); (159.5, 49.2); (157.0, 63.0)])
        Series.Scatter("Male", [(155.8, 53.6); (170.0, 59.0); (159.1, 47.6); (166.0, 69.8)])
    ]
    |> Highcharts.Scatter

basicScatter.SetXTitle "Height (cm)"
basicScatter.SetYTitle "Weight (kg)"
```
![Highcharts Basic Scatter](https://raw.github.com/TahaHachana/FsPlot/master/screenshots/HighchartsBasicScatter.PNG)