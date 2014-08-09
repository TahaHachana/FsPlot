module HtmlApp.Model


type Action =
    | Home
    | GoogleCharts
    | Highcharts
    | Chart of title * gistId

and title = string

and gistId = string
