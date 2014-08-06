module FsPlot.Google.Charting

open FsPlot.Config
open FsPlot.Data
open FsPlot.GenericChart
open FsPlot.GenericDynamicChart
open FsPlot.Highcharts.Options

type GoogleBar() =
    inherit GoogleChart()

type GoogleColumn() =
    inherit GoogleChart()

type GoogleLine() =
    inherit GoogleChart()

type GoogleSpline() =
    inherit GoogleChart()

type GoogleStackedBar() =
    inherit GoogleChart()

type GoogleStackedColumn() =
    inherit GoogleChart()

type Google =

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categorie">The data categorie displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:Series, ?categorie, ?title) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|data|] None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categorie">The data categorie displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:seq<#key * #value>, ?categorie, ?title) =
        let series = Series.Bar data
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|series|] None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:seq<(#key * #value) list>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="categories">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:seq<(#key * #value) []>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:Series seq, ?categories, ?title) =
        let chartData = ChartConfig.Google categories (Seq.toArray data) None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())


    /// <summary>Creates a bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Bar(data:seq<seq<#key * #value>>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Bar None None
        GoogleChart.Create chartData (fun () -> GoogleBar())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:Series, ?categorie, ?title) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|data|] None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:seq<#key * #value>, ?categorie, ?title) =
        let series = Series.Bar data
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|series|] None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:seq<(#key * #value) list>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Column
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:seq<(#key * #value) []>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Column
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:seq<seq<#key * #value>>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Bar
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Column(data:Series seq, ?categories, ?title) =
        let chartData = ChartConfig.Google categories (Seq.toArray data) None None title None Column None None
        GoogleChart.Create chartData (fun () -> GoogleColumn())

    /// <summary>Creates a geo chart.</summary>
    /// <param name="data">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="region">The area to display on the geochart.</param>
    /// <param name="mode">The geochart type: auto, regions, markers or text.</param>
    /// <param name="sizeAxis">The range used to display proportional marker values.</param>
    static member Geo(data:Series, ?categorie, ?region, ?mode, ?sizeAxis) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|data|] None None None None Geo None None
        GoogleGeochart.Create chartData region mode sizeAxis

    /// <summary>Creates a geo chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="region">The area to display on the geochart.</param>
    /// <param name="mode">The geochart type: auto, regions, markers or text.</param>
    /// <param name="sizeAxis">The range used to display proportional marker values.</param>
    static member Geo(data:seq<string * #value>, ?categorie, ?region, ?mode, ?sizeAxis) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let data = Series.Geo data
        let chartData = ChartConfig.Google categories [|data|] None None None None Geo None None
        GoogleGeochart.Create chartData region mode sizeAxis

    /// <summary>Creates a geo chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="region">The area to display on the geochart.</param>
    /// <param name="mode">The geochart type: auto, regions, markers or text.</param>
    /// <param name="sizeAxis">The range used to display proportional marker values.</param>
    static member Geo(data:seq<string * #value * #value>, ?categories, ?region, ?mode, ?sizeAxis) =
        let s1 =
            data
            |> Seq.map (fun (k, v, _) -> k, v)
            |> Series.Geo
        let s2 =
            data
            |> Seq.map (fun (k, _, v) -> k, v)
            |> Series.Geo
        let chartData = ChartConfig.Google categories [|s1; s2|] None None None None Geo None None
        GoogleGeochart.Create chartData region mode sizeAxis

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:Series, ?categorie, ?title) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|data|] None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:seq<#key * #value>, ?categorie, ?title) =
        let series = Series.Line data
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|series|] None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:seq<(#key * #value) list>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Line
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:seq<(#key * #value) []>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Line
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:seq<seq<#key * #value>>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Line
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())

    /// <summary>Creates a line chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Line(data:seq<Series>, ?categories, ?title) =
        let chartData = ChartConfig.Google categories (Seq.toArray data) None None title None Line None None
        GoogleChart.Create chartData (fun () -> GoogleLine())



    /// <summary>Creates a sline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Spline(data:Series, ?categorie, ?title) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|data|] None None title None Spline None None
        GoogleChart.Create chartData (fun () -> GoogleSpline())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Spline(data:seq<#key * #value>, ?categorie, ?title) =
        let series = Series.Spline data
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|series|] None None title None Spline None None
        GoogleChart.Create chartData (fun () -> GoogleSpline())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Spline(data:seq<(#key * #value) list>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Spline
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Spline None None
        GoogleChart.Create chartData (fun () -> GoogleSpline())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Spline(data:seq<(#key * #value) []>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Spline
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Spline None None
        GoogleChart.Create chartData (fun () -> GoogleSpline())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Spline(data:seq<seq<#key * #value>>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.Spline
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None Spline None None
        GoogleChart.Create chartData (fun () -> GoogleSpline())

    /// <summary>Creates a spline chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member Spline(data:seq<Series>, ?categories, ?title) =
        let chartData = ChartConfig.Google categories (Seq.toArray data) None None title None Spline None None
        GoogleChart.Create chartData (fun () -> GoogleSpline())



    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:Series, ?categorie, ?title) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|data|] None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:seq<#key * #value>, ?categorie, ?title) =
        let series = Series.StackedBar data
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|series|] None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:seq<(#key * #value) list>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.StackedBar
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:seq<(#key * #value) []>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.StackedBar
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())

    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:seq<seq<#key * #value>>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.StackedBar
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())











    /// <summary>Creates a stacked bar chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedBar(data:seq<Series>, ?categories, ?title) =
        let chartData = ChartConfig.Google categories (Seq.toArray data) None None title None StackedBar None None
        GoogleChart.Create chartData (fun () -> GoogleStackedBar())



    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:Series, ?categorie, ?title) =
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|data|] None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="label">The data label displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:seq<#key * #value>, ?categorie, ?title) =
        let series = Series.StackedColumn data
        let categories =
            match categorie with
            | None -> None
            | Some x -> Some [x]
        let chartData = ChartConfig.Google categories [|series|] None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:seq<(#key * #value) list>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.StackedColumn
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:seq<(#key * #value) []>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.StackedColumn
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:seq<seq<#key * #value>>, ?categories, ?title) =
        let data =
            data
            |> Seq.map Series.StackedColumn
            |> Seq.toArray
        let chartData = ChartConfig.Google categories data None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

    /// <summary>Creates a stacked column chart.</summary>
    /// <param name="series">The chart's data.</param>
    /// <param name="labels">The data categories displayed in the legend.</param>
    /// <param name="title">The chart's title.</param>
    static member StackedColumn(data:seq<Series>, ?categories, ?title) =
        let chartData = ChartConfig.Google categories (Seq.toArray data) None None title None StackedColumn None None
        GoogleChart.Create chartData (fun () -> GoogleStackedColumn())

type Chart =

    /// <summary>Sets the categories of a chart's X-axis.</summary>
    static member categories categories (chart:#GoogleChart) =
        chart.SetCategories categories
        chart

    /// <summary>Closes the chart's window.</summary>
    static member close (chart:#GoogleChart) = chart.Close()

    /// <summary>Sets the chart's data.</summary>
    static member data (series:seq<Series>) (chart:#GoogleChart) = chart.SetData series

    /// <summary>Hides the legend of the chart.</summary>
    static member hideLegend (chart:#GoogleChart) =
        chart.HideLegend()
        chart

    static member plot (series:Series) =
        match series.Type with
        | Bar -> Google.Bar series :> GoogleChart
        | Column -> Google.Column series :> GoogleChart
        | Line -> Google.Line series :> GoogleChart
        | Spline -> Google.Spline series :> GoogleChart
        | StackedBar -> Google.StackedBar series :> GoogleChart
        | _ -> Google.StackedColumn series :> GoogleChart

    static member plot (series:seq<Series>) =
        let chartType =
            series
            |> Seq.nth 0
            |> fun x -> x.Type
        match chartType with
        | Bar -> Google.Bar series :> GoogleChart
        | Column -> Google.Column series :> GoogleChart
        | Line -> Google.Line series :> GoogleChart
        | Spline -> Google.Spline series :> GoogleChart
        | StackedBar -> Google.StackedBar series :> GoogleChart
        | _ -> Google.StackedColumn series :> GoogleChart

    /// <summary>Displays the chart's legend.</summary>
    static member showLegend (chart:#GoogleChart) =
        chart.ShowLegend()
        chart

    /// <summary>Sets the chart's title.</summary>
    static member title title (chart:#GoogleChart) =
        chart.SetTitle title
        chart

    /// <summary>Sets the chart's X-axis title.</summary>
    static member xTitle xTitle (chart:#GoogleChart) =
        chart.SetXTitle xTitle
        chart

    /// <summary>Sets the chart's Y-axis title.</summary>
    static member yTitle yTitle (chart:#GoogleChart) =
        chart.SetYTitle yTitle
        chart

type Geochart =

    /// <summary>Sets the categories of a chart's X-axis.</summary>
    static member categories categories (chart:#GoogleGeochart) =
        chart.SetCategories categories
        chart

    /// <summary>Closes the chart's window.</summary>
    static member close (chart:#GoogleGeochart) = chart.Close()





    /// <summary>Sets the chart's data.</summary>
    static member data (data:Series) (chart:#GoogleGeochart) = chart.SetData data

//    /// <summary>Sets the chart's data.</summary>
//    static member data (data:seq<string * #value * #value>) (chart:#GoogleGeochart) = chart.SetData data

//    /// <summary>Hides the legend of the chart.</summary>
//    static member hideLegend (chart:#GoogleChart) =
//        chart.HideLegend()
//        chart

    static member plot (series:Series) = Google.Geo series