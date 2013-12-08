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

open FsPlot

let data = ["IE", 256; "Chrome", 233]

// Create a pie chart
let chart = Highcharts.Pie(data, "Visitors Breakdown", "Visitors by Browser")

// Update the data source
chart.Data <- ["IE", 256; "Chrome", 233; "Firefox", 123; "Opera", 54]

// Change the chart title
chart.Title <- Some "Website Visitors by Browser"
```
![Pie Chart](https://lh3.googleusercontent.com/-JwZ2yq1fPbY/UqRbPDkpuhI/AAAAAAAAAMc/6zZkq6odmto/w575-h410-no/pie.PNG)

Contact
-------

* Email: tahahachana@gmail.com
* [Website](http://taha-hachana.apphb.com/)
* [Blog](http://fsharp-code.blogspot.com/)
* [+Taha](https://plus.google.com/103826666258148033768/ "Google+")
* [@TahaHachana](https://twitter.com/TahaHachana "Twitter")