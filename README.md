FsPlot
======

About
-----

FsPlot is an interactive data visualization library for F# using HTML5/JavaScript.

Static Charts Demos
-------------------
* Highcharts
    * Area: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicArea.md), [Negative Values](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsNegativeValuesArea.md), [Inverted Axes](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsInvertedAxesArea.md)
    * [Areaspline](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicAreaspline.md)
    * [Arearange](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicArearange.md)
    * [Bar](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicBar.md)
    * [Bubble](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicBubble.md)
    * [Column](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicColumn.md)
    * Combination: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicComb.md), [Column + Spline + Pie](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsColumnSplinePie.md)
    * [Donut](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicDonut.md)
    * [Funnel](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicFunnel.md)
    * [Line](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicLine.md)
    * [PercentArea](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentArea.md)
    * [PercentBar](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentBar.md)
    * [PercentColumn](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentColumn.md)
    * [Pie](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicPie.md)
    * [Radar](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicRadar.md)
    * [Scatter](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicScatter.md)
    * [Spline](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicSpline.md)
    * [StackedArea](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedArea.md)
    * [StackedBar](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedBar.md)
    * [StackedColumn] (https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedColumn.md)

Dynamic Charts Demos
--------------------
* Highcharts
    * [Area](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsDynamicArea.md)
    * [Areaspline](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsDynamicAreaspline.md)
    * [Arearange](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsDynamicArearange.md)
    * [Bar](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsDynamicBar.md)
    * [Bubble](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsDynamicBubble.md)
    * [Column](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsDynamicColumn.md)

NuGet
-----

	PM> Install-Package FsPlot

Usage
-----

```fsharp
#load "FsPlotInit.fsx"

open FsPlot.Data
open FsPlot.Highcharts.Charting

// Functional style.
let pie = 
    Series.Pie ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
    |> Series.SetName "Browser Share"
    |> Chart.plot
    |> Chart.title "Website Visitors By Browser"
    |> Chart.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%<br/>"""
    |> Chart.showLegend

// Object-oriented style.
let data =
    Series.Pie(
        "Browser Share",
        ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.])

let pie' = Highcharts.Pie(data, legend = true, title = "Website Visitors By Browser")

pie'.SetTooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%<br/>"""
```
Contact
-------

* Email: tahahachana@gmail.com
* [Website](http://taha-hachana.apphb.com/)
* [Blog](http://fsharp-code.blogspot.com/)
* [+Taha](https://plus.google.com/103826666258148033768/ "Google+")
* [@TahaHachana](https://twitter.com/TahaHachana "Twitter")