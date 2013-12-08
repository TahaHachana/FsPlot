module FsPlot.Highcharts

open JS

type Pie(data, ?windowTitle, ?chartTitle) as chart =

    let mutable dataField = data
    let mutable windowTitleField = windowTitle
    let mutable chartTitleField = chartTitle
    [<DefaultValue>] val mutable private jsField : string
    [<DefaultValue>] val mutable private htmlField : string

    let data' = Seq.toArray dataField
    let js = Highcharts.Pie.js data' chartTitleField
    let html = Html.highcharts js
    let wnd, browser = ChartWindow.show windowTitleField html
    
    do chart.jsField <- js
    do chart.htmlField <- html

    member chart.Data
        with get() = dataField
        and set(data') =
            let data'' = Seq.toArray data'
            let js = Highcharts.Pie.js data'' chartTitle
            let html = Html.highcharts js
            browser.NavigateToString html
            dataField <- data'
            chart.jsField <- js
            chart.htmlField <- html

    member chart.Html = chart.htmlField

    member chart.Title
        with get() = chartTitleField
        and set(title) =
            let data = Seq.toArray dataField
            let js = Highcharts.Pie.js data title
            let html = Html.highcharts js
            browser.NavigateToString html
            chartTitleField <- title
            chart.jsField <- js
            chart.htmlField <- html

    member chart.Js = chart.jsField

    member chart.WindowTitle
        with get() = windowTitleField
        and set(title) =
            match title with
            | None -> ()
            | Some value ->
                wnd.Title <- value
                windowTitleField <- title