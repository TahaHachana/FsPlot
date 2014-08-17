module HtmlApp.View

open HtmlApp.Model
open HtmlApp.Skin
open IntelliFactory.Html

let home =
    Skin.withHomeTemplate "FsPlot" <| fun ctx ->
        [

        ]

let googleCharts =
    Skin.withGoogleTemplate "FsPlot · Google Charts Support" <| fun ctx ->
        [
        ]


let highcharts =
    Skin.withHighchartsTemplate "FsPlot · Highcharts Support" <| fun ctx ->
        [

        ]

type Demo =
    {
        Id : string
        Heading : string
    }

    static member New id heading =
        {
            Id = id
            Heading = heading
            
        }

let chart title (demos:Demo list) = //gistId =
     Skin.withChartTemplate ("FsPlot · " + title) <| fun ctx ->
        [
//            yield Div [Class "page-header"] -< [
//                H1 [Text title]
//            ]
            for demo in demos do
                let demoId = demo.Id
                yield Div [Id demoId] -< [
                    H2 [Class "page-header"] -< [Text demo.Heading]
                    Div [Id "gist"] -< [
                        Script [Src <| "https://gist.github.com/TahaHachana/" + demoId + ".js"]
                    ]
                    IFrame [Src <| "../iframe/" + demoId + ".html"; Class "chart-iframe"]
                ]
        ]