module FsPlot.Charting

open DataSeries
open JS

type ChartData =
    {
        mutable Data : Series []
        mutable Title : string option
        mutable Legend : bool
        Type : ChartType
    }

let private compileJs chartType data title legend =
    match chartType with
    | Area -> Highcharts.Area.js data title legend
    | Pie -> Highcharts.Pie.js data title legend

type GenericChart(chartData:ChartData) as chart =

    [<DefaultValue>] val mutable private jsField : string
    [<DefaultValue>] val mutable private htmlField : string

    let wnd, browser = ChartWindow.show()

    let navigate() =
        let js = compileJs chartData.Type chartData.Data chartData.Title chartData.Legend
        let html = Html.highcharts js
        browser.NavigateToString html
        do chart.jsField <- js
        do chart.htmlField <- html
    
    do navigate()

    member __.SetData data =
        let series = Series.New "" chartData.Type data
        chartData.Data <- [|series|] 
        navigate()

    member __.SetData series =
        chartData.Data <- [|series|] 
        navigate()

    static member internal Create(data:ChartData, f: unit -> #GenericChart ) =
        let t = f()
        t

    member __.Title = chartData.Title

    member __.SetTitle title =
        chartData.Title <- Some title
        navigate()

    member __.HideLegend() =
        chartData.Legend <- false
        navigate()

    member __.ShowLegend() =
        chartData.Legend <- true
        navigate()

type HighchartsPie(chartData) =
    inherit GenericChart(chartData)

type HighchartsArea(chartData) =
    inherit GenericChart(chartData)

type Highcharts =
    
    static member Pie(data:seq<#key*#value>, ?chartTitle, ?legend) =
        let series = Series.New "" ChartType.Pie data
        let chartData =
            {
                Data = [|series|]
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Pie
            }
        GenericChart.Create(chartData, (fun () -> HighchartsPie(chartData)))

    static member Pie(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = [|series|]
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Pie
            }
        GenericChart.Create(chartData, (fun () -> HighchartsPie(chartData)))

    static member Area(data:seq<#key*#value>, ?chartTitle, ?legend) =
        let series = Series.New "" ChartType.Area data
        let ty = Seq.head data |> snd
        let ty = ty.GetTypeCode()
        let chartData =
            {
                Data = [|series|]
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea(chartData)))

    static member Area(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = [|series|]
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea(chartData)))

    static member Area(data:seq<seq<#key*#value>>, ?chartTitle, ?legend) =
        let dataSeries =
            data
            |> Seq.map (fun x -> Series.New "" ChartType.Area x)
            |> Seq.toArray
        let chartData =
            {
                Data = dataSeries
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea(chartData)))

    static member Area(data:seq<(#key*#value) list>, ?chartTitle, ?legend) =
        let dataSeries =
            data
            |> Seq.map (fun x -> Series.New "" ChartType.Area x)
            |> Seq.toArray
        let chartData =
            {
                Data = dataSeries
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea(chartData)))

    static member Area(data:seq<(#key*#value) []>, ?chartTitle, ?legend) =
        let dataSeries =
            data
            |> Seq.map (fun x -> Series.New "" ChartType.Area x)
            |> Seq.toArray
        let chartData =
            {
                Data = dataSeries
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea(chartData)))

    static member Area(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = series |> Seq.toArray
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea(chartData)))
