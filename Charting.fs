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
    | Line -> Highcharts.Line.js data title legend
    | Pie -> Highcharts.Pie.js data title legend


type GenericChart() as chart =

    [<DefaultValue>] val mutable private chartData : ChartData
    [<DefaultValue>] val mutable private jsField : string
    [<DefaultValue>] val mutable private htmlField : string

    let wnd, browser = ChartWindow.show()

    let navigate() =
        let js = compileJs chart.chartData.Type chart.chartData.Data chart.chartData.Title chart.chartData.Legend
        let html = Html.highcharts js
        browser.NavigateToString html
        do chart.jsField <- js
        do chart.htmlField <- html
    
    member __.Refresh() = navigate()

    member __.SetData data =
        let series = Series.New "" chart.chartData.Type data
        chart.chartData.Data <- [|series|] 
        navigate()

    member __.SetData series =
        chart.chartData.Data <- [|series|] 
        navigate()

    static member internal Create(chartData:ChartData, f: unit -> #GenericChart ) =
        let t = f()
        t.chartData <- chartData
        t.Refresh()
        t

    member __.Title = chart.chartData.Title

    member __.SetTitle title =
        chart.chartData.Title <- Some title
        navigate()

    member __.HideLegend() =
        chart.chartData.Legend <- false
        navigate()

    member __.ShowLegend() =
        chart.chartData.Legend <- true
        navigate()

type HighchartsPie() =
    inherit GenericChart()

type HighchartsArea() =
    inherit GenericChart()

type HighchartsLine() =
    inherit GenericChart()

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
        GenericChart.Create(chartData, (fun () -> HighchartsPie()))

    static member Pie(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = [|series|]
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Pie
            }
        GenericChart.Create(chartData, (fun () -> HighchartsPie()))

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
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

    static member Area(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = [|series|]
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

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
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

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
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

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
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

    static member Area(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = series |> Seq.toArray
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Area
            }
        GenericChart.Create(chartData, (fun () -> HighchartsArea()))

    static member Line(data:seq<#key*#value>, ?chartTitle, ?legend) =
        let series = Series.New "" ChartType.Line data
        let ty = Seq.head data |> snd
        let ty = ty.GetTypeCode()
        let chartData =
            {
                Data = [|series|]
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Line
            }
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    static member Line(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = [|series|]
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Line
            }
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    static member Line(data:seq<seq<#key*#value>>, ?chartTitle, ?legend) =
        let dataSeries =
            data
            |> Seq.map (fun x -> Series.New "" ChartType.Line x)
            |> Seq.toArray
        let chartData =
            {
                Data = dataSeries
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Line
            }
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    static member Line(data:seq<(#key*#value) list>, ?chartTitle, ?legend) =
        let dataSeries =
            data
            |> Seq.map (fun x -> Series.New "" ChartType.Line x)
            |> Seq.toArray
        let chartData =
            {
                Data = dataSeries
                Title=chartTitle
                Legend = defaultArg legend false
                Type = Line
            }
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    static member Line(data:seq<(#key*#value) []>, ?chartTitle, ?legend) =
        let dataSeries =
            data
            |> Seq.map (fun x -> Series.New "" ChartType.Line x)
            |> Seq.toArray
        let chartData =
            {
                Data = dataSeries
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Line
            }
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))

    static member Line(series, ?chartTitle, ?legend) =
        let chartData =
            {
                Data = series |> Seq.toArray
                Title = chartTitle
                Legend = defaultArg legend false
                Type = Line
            }
        GenericChart.Create(chartData, (fun () -> HighchartsLine()))