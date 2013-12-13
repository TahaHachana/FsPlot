FsPlot
======

About
-----

F# data visualization library using HTML5.

Status
------

Experimental

NuGet
-----

	PM> Install-Package FsPlot

Usage
-----

```fsharp
#load "FsPlot.fsx"

open FsPlot.Charting
open FsPlot.DataSeries

let data = ["Chrome", 233; "Firefox", 141; "IE", 256]
    
// Create a pie chart
let chart = Highcharts.Pie data


// Display a legend
chart.ShowLegend()


// Update the chart's data
chart.SetData ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208]


// Update the chart's data in a more structured way
["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]
|> Series.New "Browser Share" ChartType.Pie
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