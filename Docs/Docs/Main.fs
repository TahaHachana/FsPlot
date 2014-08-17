namespace HtmlApp

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets
open Model
open View

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

    let chartSitelet url id title demos =
        Sitelet.Content
            url
            (Chart id)
            <| View.chart title demos

    let Main =
        Sitelet.Sum [
            Sitelet.Content "/" Home View.home
            Sitelet.Content "/google-charts" GoogleCharts View.googleCharts
            Sitelet.Content "/highcharts" Highcharts View.highcharts
            chartSitelet
                "/chart/google-bar-chart"
                1
                "Google Bar Chart"
                [Demo.New "aab4fdc7360e039e0bba" "Google Bar Chart"]
            chartSitelet
                "/chart/google-column-chart"
                2
                "Google Column Chart"
                [Demo.New "766f29a5400e9892cc51" "Google Column Chart"]
            chartSitelet
                "/chart/google-geo-chart"
                3
                "Google Geo Chart"
                [Demo.New "1dfb083d10e37d106ff3" "Google Geo Chart"]
            chartSitelet
                "/chart/google-line-chart"
                4
                "Google Line Chart"
                [Demo.New "3c70c83956be38e2e990" "Google Line Chart"]
            chartSitelet
                "/chart/google-spline-chart"
                5
                "Google Spline Chart"
                [Demo.New "149fea9707dd8ebf7b22" "Google Spline Chart"]
            chartSitelet
                "/chart/google-stacked-bar-chart"
                6
                "Google Stacked Bar Chart"
                [Demo.New "b74bfed5807709fa3bf0" "Google Stacked Bar Chart"]
            chartSitelet
                "/chart/google-stacked-column-chart"
                7
                "Google Stacked Column Chart"
                [Demo.New "0094ce931e590f5d4636" "Google Stacked Column Chart"]
            chartSitelet
                "/chart/highcharts-area-chart"
                8
                "Highcharts Area Chart"
                [Demo.New "4c4f49ed1a44383be329" "Highcharts Area Chart"]
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
                Chart 1 //("Google Bar Chart", "aab4fdc7360e039e0bba")
                Chart 2 //("Google Column Chart", "766f29a5400e9892cc51")
                Chart 3 //("Google Geo Chart", "1dfb083d10e37d106ff3")
                Chart 4 //("Google Line Chart", "3c70c83956be38e2e990")
                Chart 5 //("Google Spline Chart", "149fea9707dd8ebf7b22")            
                Chart 6 //("Google Stacked Bar Chart", "b74bfed5807709fa3bf0")
                Chart 7 //("Google Stacked Column Chart", "0094ce931e590f5d4636")
                Chart 8 //("Highcharts Area Chart", "4c4f49ed1a44383be329")         
            ]

[<assembly: Website(typeof<Website>)>]
do ()
