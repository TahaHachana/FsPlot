module HtmlApp.Content

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets

module Home =

    let body : Content.HtmlElement list =
        [
            
        ]

module GoogleCharts =

    let body ctx : Content.HtmlElement list =
        [
            
        ]

module Highcharts =

    let body ctx : Content.HtmlElement list =
        [
            
        ]

module Chart =

    let body title gistId ctx : Content.HtmlElement list =
        [
            Div [Class "page-header"] -< [
                H1 [Text title]
            ]
            H2 [Text "Code"]
            Script [Src <| "https://gist.github.com/TahaHachana/" + gistId + ".js"]
            H2 [Text "Chart"]
            IFrame [Src <| "../iframe/" + gistId + ".html"; Id "chart-iframe"]
        ]