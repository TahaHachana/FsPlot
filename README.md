FsPlot
======

About
-----

FsPlot is an HTML5 data visualization library designed for F# interactive data scripting and exploratory analysis. [FunScript](http://funscript.info/) is used to compile F# code into JavaScript.

Supported Chart Types
---------------------
* Highcharts
    * Area: [Basic Area](https://github.com/TahaHachana/FsPlot/blob/master/examples/HighchartsBasicArea.md), [Area with Negative Values](https://github.com/TahaHachana/FsPlot/blob/master/examples/NegativeValuesArea.md), [Stacked Area](https://github.com/TahaHachana/FsPlot/blob/master/examples/StackedArea.md), [Percent Area](https://github.com/TahaHachana/FsPlot/blob/master/examples/PercentArea.md)

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
|> Series.Pie "Browser Share"
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