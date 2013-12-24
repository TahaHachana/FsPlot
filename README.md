FsPlot
======

About
-----

FsPlot is an interactive data visualization library for F# using HTML5/JavaScript.

Demos
-----
* Highcharts
    * Area: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicArea.md), [Negative Values](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsNegativeValuesArea.md), [Stacked](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedArea.md), [Percent](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentArea.md), [Inverted Axes](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsInvertedAxesArea.md)
    * Areaspline: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicAreaspline.md)
    * Arearange: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicArearange.md)
    * Bar: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicBar.md), [Stacked](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedBar.md), [Percent](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentBar.md)
    * Bubble: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicBubble.md)
    * Column: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicColumn.md), [Stacked](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsStackedColumn.md), [Percent](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsPercentColumn.md)
    * Combination: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicComb.md)
    * Donut: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicDonut.md)
    * Pie: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicPie.md)
    * Scatter: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicScatter.md)
    * Spline: [Basic](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicSpline.md)

* Kendo UI DataViz

NuGet
-----

	PM> Install-Package FsPlot

Usage
-----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let data = ["Chrome", 40.4; "Firefox", 36.5; "IE", 23.1]
    
// Create a pie chart
let chart = Highcharts.Pie data

// Display a legend
chart.ShowLegend()

// Update the chart's data
chart.SetData ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 24.2]

// Update the chart's data in a more structured way
["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
|> Series.Pie
|> Series.SetName "Browser Share"
|> chart.SetData

// Add a title
chart.SetTitle "Website Visitors By Browser"
```
![Pie Chart](https://lh4.googleusercontent.com/-mKGde0NEjNg/UqhOZKp4uTI/AAAAAAAAANk/p2A_oW--4Gk/w698-h498-no/pie.PNG)

Contact
-------

* Email: tahahachana@gmail.com
* [Website](http://taha-hachana.apphb.com/)
* [Blog](http://fsharp-code.blogspot.com/)
* [+Taha](https://plus.google.com/103826666258148033768/ "Google+")
* [@TahaHachana](https://twitter.com/TahaHachana "Twitter")