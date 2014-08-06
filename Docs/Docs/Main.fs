namespace HtmlApp

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets
open Model

module Site =

    let ( => ) text url =
        A [HRef url] -< [Text text]

//    let Links (ctx: Context<Action>) =
//        UL [
//            LI ["Home" => ctx.Link Home]
//            LI ["About" => ctx.Link About]
//        ]

//    let HomePage =
//        Skin.WithTemplate "HomePage" <| fun ctx ->
//            [
//                Div [Text "HOME"]
////                Div [new Controls.EntryPoint()]
////                Links ctx
//            ]
//
//    let AboutPage =
//        Skin.WithTemplate "AboutPage" <| fun ctx ->
//            [
//                Div [Text "ABOUT"]
////                Links ctx
//            ]

    let Main =
        Sitelet.Sum [
            Sitelet.Content "/" Home View.home
            Sitelet.Content "/google-charts" GoogleCharts View.googleCharts
            Sitelet.Content "/highcharts" Highcharts View.highcharts
            Sitelet.Content "/chart/google-bar-chart" (Chart ("Google Bar Chart", "3e3f26f5590ea4e29227")) (View.chart "Google Bar Chart" "3e3f26f5590ea4e29227")
        ]

[<Sealed>]
type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Site.Main
        member this.Actions =
            [
                Home
                GoogleCharts
                Highcharts
                Chart ("Google Bar Chart", "3e3f26f5590ea4e29227")
            ]

[<assembly: Website(typeof<Website>)>]
do ()
