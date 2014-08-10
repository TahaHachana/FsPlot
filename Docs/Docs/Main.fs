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
            Sitelet.Content "/chart/google-bar-chart" (Chart ("Google Bar Chart", "aab4fdc7360e039e0bba")) (View.chart "Google Bar Chart" "aab4fdc7360e039e0bba")
            Sitelet.Content "/chart/google-column-chart" (Chart ("Google Column Chart", "766f29a5400e9892cc51")) (View.chart "Google Column Chart" "766f29a5400e9892cc51")
            Sitelet.Content "/chart/google-geo-chart" (Chart ("Google Geo Chart", "1dfb083d10e37d106ff3")) (View.chart "Google Geo Chart" "1dfb083d10e37d106ff3")
            Sitelet.Content "/chart/google-line-chart" (Chart ("Google Line Chart", "3c70c83956be38e2e990")) (View.chart "Google Geo Chart" "3c70c83956be38e2e990")
            Sitelet.Content "/chart/google-spline-chart" (Chart ("Google Spline Chart", "149fea9707dd8ebf7b22")) (View.chart "Google Spline Chart" "149fea9707dd8ebf7b22")
            Sitelet.Content "/chart/google-stacked-bar-chart" (Chart ("Google Stacked Bar Chart", "b74bfed5807709fa3bf0")) (View.chart "Google Stacked Bar Chart" "b74bfed5807709fa3bf0")
            Sitelet.Content "/chart/google-stacked-column-chart" (Chart ("Google Stacked Column Chart", "0094ce931e590f5d4636")) (View.chart "Google Stacked Column Chart" "0094ce931e590f5d4636")

//            Sitelet.Content "/chart/google-geo-chart" (Chart ("Google Column Chart", "")) (View.chart "Google Geo Chart" "")
        ]
//        |> Sitelet.Shift "FsPlot"

[<Sealed>]
type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Site.Main
        member this.Actions =
            [
                Home
                GoogleCharts
                Highcharts
                Chart ("Google Bar Chart", "aab4fdc7360e039e0bba")
                Chart ("Google Column Chart", "766f29a5400e9892cc51")
                Chart ("Google Geo Chart", "1dfb083d10e37d106ff3")
                Chart ("Google Line Chart", "3c70c83956be38e2e990")
                Chart ("Google Spline Chart", "149fea9707dd8ebf7b22")            
                Chart ("Google Stacked Bar Chart", "b74bfed5807709fa3bf0")
                Chart ("Google Stacked Column Chart", "0094ce931e590f5d4636")            
            ]

[<assembly: Website(typeof<Website>)>]
do ()
