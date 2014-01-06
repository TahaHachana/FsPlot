FsPlot
======

About
-----

FsPlot is an interactive data visualization library for F# using HTML5/JavaScript.

Demos
-----
* Highcharts
    * Area: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicArea.md), [Negative Values](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsNegativeValuesArea.md), [Inverted Axes](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsInvertedAxesArea.md)
    * Areaspline: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicAreaspline.md)
    * Arearange: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicArearange.md)
    * Bar: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicBar.md)
    * Bubble: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicBubble.md)
    * Column: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicColumn.md)
    * Combination: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicComb.md), [Column + Spline + Pie](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsColumnSplinePie.md)
    * Donut: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicDonut.md)
    * Funnel: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicFunnel.md)
    * Line: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicLine.md)
    * PercentArea: [Basic] (https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentArea.md)
    * PercentBar: [Basic] (https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentBar.md)
    * PercentColumn: [Basic] (https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentColumn.md)
    * Pie: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicPie.md)
    * Radar: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicRadar.md)
    * Scatter: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicScatter.md)
    * Spline: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicSpline.md)
    * StackedArea: [Basic] (https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedArea.md)
    * StackedBar: [Basic] (https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedBar.md)
    * StackedColumn: [Basic] (https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedColumn.md)

* Kendo UI DataViz

NuGet
-----

	PM> Install-Package FsPlot

Usage
-----

```fsharp
#load "FsPlot.fsx"

// Functional style.
let pie = 
    Series.Pie ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
    |> Series.SetName "Browser Share"
    |> Highcharts.plot
    |> Highcharts.title "Website Visitors By Browser"
    |> Highcharts.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%<br/>"""
    |> Highcharts.showLegend

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