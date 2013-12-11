module FsPlot.Charting

open JS

type ChartType = Area | Pie

type ChartData =
    {
        mutable Values : System.Collections.IEnumerable
        mutable TypeCode : System.TypeCode
        mutable Title : string option
        mutable Legend : bool
        Type : ChartType
    }

let compileJs chartType data title legend =
    match chartType with
    | Area -> ""
    | Pie -> Highcharts.Pie.js data title legend

type GenericChart(chartData:ChartData) as chart =

    [<DefaultValue>] val mutable private jsField : string
    [<DefaultValue>] val mutable private htmlField : string

    let wnd, browser = ChartWindow.show()

    let navigate data title legend =
        let js = compileJs chartData.Type data title legend
        let html = Html.highcharts js
        browser.NavigateToString html
        do chart.jsField <- js
        do chart.htmlField <- html

    member __.SetData data =
        navigate data chartData.Title chartData.Legend
        let ty = Seq.head data |> snd
        let ty = ty.GetTypeCode()
        chartData.TypeCode <- ty
        chartData.Values <- data

    member __.Data = Seq.cast<string*System.IConvertible> chartData.Values

    member internal __.Init(data, title, legend) = navigate data title legend

    static member internal Create(data:ChartData, f: unit -> #GenericChart ) =
        let t = f()
        match data.TypeCode with
        | System.TypeCode.Int32 -> t.Init((data.Values |> Seq.cast<string*int>), data.Title, data.Legend)
        | _ -> t.Init((data.Values |> Seq.cast<string*float>), data.Title, data.Legend)
        t

    member __.Title = chartData.Title

    member __.SetTitle title =
        match chartData.TypeCode with
        | System.TypeCode.Int32 -> navigate (chartData.Values |> Seq.cast<string*int>) (Some title) chartData.Legend
        | _ -> navigate (chartData.Values |> Seq.cast<string*float>) (Some title) chartData.Legend

    member __.HideLegend() =
        chartData.Legend <- false
        match chartData.TypeCode with
        | System.TypeCode.Int32 -> navigate (chartData.Values |> Seq.cast<string*int>) chartData.Title chartData.Legend
        | _ -> navigate (chartData.Values |> Seq.cast<string*float>) chartData.Title chartData.Legend


    member __.ShowLegend() =
        chartData.Legend <- true
        match chartData.TypeCode with
        | System.TypeCode.Int32 -> navigate (chartData.Values |> Seq.cast<string*int>) chartData.Title chartData.Legend
        | _ -> navigate (chartData.Values |> Seq.cast<string*float>) chartData.Title chartData.Legend

type HighchartsPie(chartData) =
    inherit GenericChart(chartData)

type Highcharts =
    
    static member Pie(data:seq<string*#System.IConvertible>, ?chartTitle, ?legend) =
        let ty = Seq.head data |> snd
        let ty = ty.GetTypeCode()
        let chartData =
            {
                Values=data
                TypeCode=ty
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Pie
            }
        GenericChart.Create(chartData, (fun () -> HighchartsPie(chartData)))

