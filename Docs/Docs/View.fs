module HtmlApp.View

open HtmlApp.Model
open HtmlApp.Skin
open IntelliFactory.Html

let home =
    Skin.withHomeTemplate "FsPlot" <| fun ctx ->
        [

        ]



let googleCharts =
    Skin.withGoogleTemplate "Google Charts" <| fun ctx ->
        [
        ]


let highcharts =
    Skin.withHighchartsTemplate "Highcharts" <| fun ctx ->
        [

        ]

let chart title gistId =
     Skin.withChartTemplate title <| fun ctx ->
        [
            Div [Class "page-header"] -< [
                H1 [Text title]
            ]
            H2 [Text "Code"]
            Script [Src <| "https://gist.github.com/TahaHachana/" + gistId + ".js"]
            H2 [Text "Chart"]
            IFrame [Src <| "iframe/" + gistId + ".html"; Id "chart-iframe"]
        ]