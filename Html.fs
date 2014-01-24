module internal FsPlot.Html

open FsPlot.Data
open FsPlot.Highcharts

let highcharts chartType =
    match chartType with
    | Arearange | Bubble | Radar -> Html.more
    | Combination -> Html.combine
    | Funnel -> Html.funnel
    | _ -> Html.common

let dynamicHighcharts chartType address js =
    match chartType with
    | Arearange | Bubble | Radar -> Html.dynamicMore address js
    | Funnel -> Html.dynamicFunnel address js
    | _ -> Html.dynamicCommon address js