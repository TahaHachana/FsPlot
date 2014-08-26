namespace HtmlApp

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets
open Model
open View

module Site =

//    let ( => ) text url =
//        A [HRef url] -< [Text text]

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

    let dynamicChartSitelet url id title demos =
        Sitelet.Content
            url
            (Chart id)
            <| View.dynamicChart title demos

    let mapchartSitelet url id title demos =
        Sitelet.Content
            url
            (Chart id)
            <| View.mapchart title demos

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
                "Google Geochart"
                [
                    Demo.New "1dfb083d10e37d106ff3" "Region Geochart"
                    Demo.New "054b9373c210329ae682" "Marker Geochart"                
                    Demo.New "044838f15db66c60f140" "Marker Geochart - Displaying Proportional Markers"
                    Demo.New "d80698abecb0098e41cb" "Text Geochart"
                ]
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
                [
                    Demo.New "4c4f49ed1a44383be329" "Highcharts Area Chart"
                    Demo.New "b27962b275a1e4736e66" "Area Chart with Negative Values"
                ]
            chartSitelet
                "/chart/highcharts-areaspline-chart"
                9
                "Highcharts Areaspline Chart"
                [
                    Demo.New "61b4dada35c17bb0513a" "Highcharts Areaspline Chart"
                ]
            chartSitelet
                "/chart/highcharts-arearange-chart"
                10
                "Highcharts Arearange Chart"
                [
                    Demo.New "5d854fb7fc2ef097a95e" "Highcharts Arearange Chart"
                ]
            chartSitelet
                "/chart/highcharts-bar-chart"
                11
                "Highcharts Bar Chart"
                [
                    Demo.New "10a101fc9c3469ddb80f" "Highcharts Bar Chart"
                ]
            chartSitelet
                "/chart/highcharts-bubble-chart"
                12
                "Highcharts Bubble Chart"
                [
                    Demo.New "b80547bb1918793651d0" "Highcharts Bubble Chart"
                ]
            chartSitelet
                "/chart/highcharts-column-chart"
                13
                "Highcharts Column Chart"
                [
                    Demo.New "acad8d538edeac22a7d3" "Highcharts Column Chart"
                ]
            chartSitelet
                "/chart/highcharts-combination-chart"
                14
                "Highcharts Combination Chart"
                [
                    Demo.New "a2ea2861f3dbdbb955ff" "Highcharts Combination Chart"
                ]
            chartSitelet
                "/chart/highcharts-donut-chart"
                15
                "Highcharts Donut Chart"
                [
                    Demo.New "3b5c60d148f3aae425ce" "Highcharts Donut Chart"
                ]
            chartSitelet
                "/chart/highcharts-funnel-chart"
                16
                "Highcharts Funnel Chart"
                [
                    Demo.New "bd95d7e5ac0a40b6c704" "Highcharts Funnel Chart"
                ]
            chartSitelet
                "/chart/highcharts-line-chart"
                17
                "Highcharts Line Chart"
                [
                    Demo.New "38eaf4529f1b8d9d5e5e" "Highcharts Line Chart"
                ]
            chartSitelet
                "/chart/highcharts-percent-area-chart"
                18
                "Highcharts Percent Area Chart"
                [
                    Demo.New "6d1f469e424c1582381c" "Highcharts Percent Area Chart"
                ]
            chartSitelet
                "/chart/highcharts-percent-bar-chart"
                19
                "Highcharts Percent Bar Chart"
                [
                    Demo.New "e3f40b9b326d4f63655e" "Highcharts Percent Bar Chart"
                ]
            chartSitelet
                "/chart/highcharts-percent-column-chart"
                20
                "Highcharts Percent Column Chart"
                [
                    Demo.New "3032649453f3098de0ab" "Highcharts Percent Column Chart"
                ]
            chartSitelet
                "/chart/highcharts-pie-chart"
                21
                "Highcharts Pie Chart"
                [
                    Demo.New "4776e466bf44202728ec" "Highcharts Pie Chart"
                ]
            chartSitelet
                "/chart/highcharts-radar-chart"
                22
                "Highcharts Radar Chart"
                [
                    Demo.New "6defb9874b198729d29e" "Highcharts Radar Chart"
                ]
            chartSitelet
                "/chart/highcharts-scatter-chart"
                23
                "Highcharts Scatter Chart"
                [
                    Demo.New "b802a18f6c4516de0d95" "Highcharts Scatter Chart"
                ]
            chartSitelet
                "/chart/highcharts-spline-chart"
                24
                "Highcharts Spline Chart"
                [
                    Demo.New "3975f3755ce136d05a7c" "Highcharts Spline Chart"
                ]
            chartSitelet
                "/chart/highcharts-stacked-area-chart"
                25
                "Highcharts Stacked Area Chart"
                [
                    Demo.New "d559870885471ea3bf9e" "Highcharts Stacked Area Chart"
                ]
            chartSitelet
                "/chart/highcharts-stacked-bar-chart"
                26
                "Highcharts Stacked Bar Chart"
                [
                    Demo.New "75f7614553b51fd6ba0a" "Highcharts Stacked Bar Chart"
                ]
            chartSitelet
                "/chart/highcharts-stacked-column-chart"
                27
                "Highcharts Stacked Column Chart"
                [
                    Demo.New "a64105b3ebbe7fb6ef67" "Highcharts Stacked Column Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-area-chart"
                28
                "Highcharts Dynamic Area Chart"
                [
                    Demo.New "8940092eaae7bea7d5a4" "Highcharts Dynamic Area Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-areaspline-chart"
                29
                "Highcharts Dynamic Areaspline Chart"
                [
                    Demo.New "04b3cb5780a2de4a45c2" "Highcharts Dynamic Areaspline Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-arearange-chart"
                30
                "Highcharts Dynamic Arearange Chart"
                [
                    Demo.New "268c224ae9cc463e8be5" "Highcharts Dynamic Arearange Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-bar-chart"
                31
                "Highcharts Dynamic Bar Chart"
                [
                    Demo.New "1568fcf37eb65b0148d2" "Highcharts Dynamic Bar Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-bubble-chart"
                32
                "Highcharts Dynamic Bubble Chart"
                [
                    Demo.New "bdcd194bf82f14b6f1f6" "Highcharts Dynamic Bubble Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-column-chart"
                33
                "Highcharts Dynamic Column Chart"
                [
                    Demo.New "48a4ab349ad65add1005" "Highcharts Dynamic Column Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-donut-chart"
                34
                "Highcharts Dynamic Donut Chart"
                [
                    Demo.New "fd02b8f39dede4b9e4b8" "Highcharts Dynamic Donut Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-funnel-chart"
                35
                "Highcharts Dynamic Funnel Chart"
                [
                    Demo.New "777c823139d925e974b8" "Highcharts Dynamic Funnel Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-line-chart"
                36
                "Highcharts Dynamic Line Chart"
                [
                    Demo.New "2438fb921eeb6a33a21e" "Highcharts Dynamic Line Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-pie-chart"
                37
                "Highcharts Dynamic Pie Chart"
                [
                    Demo.New "713272fa3fe2d4c9ce17" "Highcharts Dynamic Pie Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-radar-chart"
                38
                "Highcharts Dynamic Radar Chart"
                [
                    Demo.New "66b69b8f3852b00aea01" "Highcharts Dynamic Radar Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-scatter-chart"
                39
                "Highcharts Dynamic Scatter Chart"
                [
                    Demo.New "2ee74eeefe75c446a94d" "Highcharts Dynamic Scatter Chart"
                ]
            dynamicChartSitelet
                "/chart/highcharts-dynamic-spline-chart"
                40
                "Highcharts Dynamic Spline Chart"
                [
                    Demo.New "77d10bd3194a2309e7fe" "Highcharts Dynamic Spline Chart"
                ]
            mapchartSitelet
                "/chart/google-maps-chart"
                41
                "Google Maps Chart"
                [
                    Demo.New "d77a110ea4ecc14382a1" "Maps Chart - Named Locations"
                    Demo.New "cae86d93e8914a7ddb15" "Maps Chart - Geocoded Locations"                    
                ]

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
                Chart 9
                Chart 10
                Chart 11
                Chart 12
                Chart 13
                Chart 14
                Chart 15
                Chart 16
                Chart 17
                Chart 18
                Chart 19
                Chart 20
                Chart 21
                Chart 22
                Chart 23
                Chart 24
                Chart 25
                Chart 26
                Chart 27
                Chart 28
                Chart 29
                Chart 30
                Chart 31
                Chart 32
                Chart 33
                Chart 34
                Chart 35
                Chart 36
                Chart 37
                Chart 38
                Chart 39
                Chart 40
                Chart 41
            ]

[<assembly: Website(typeof<Website>)>]
do ()
