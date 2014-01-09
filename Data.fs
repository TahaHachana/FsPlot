module FsPlot.Data

open System

type key = IConvertible
type value = IConvertible

[<ReflectedDefinition>]
type ChartType =
    | Area
    | Areaspline
    | Arearange
    | Bar
    | Bubble
    | Column
    | Combination
    | Donut
    | Funnel
    | Line
    | PercentArea
    | PercentBar
    | PercentColumn
    | Pie
    | Radar
    | Scatter
    | Spline
    | StackedArea
    | StackedBar
    | StackedColumn

module private Utils =

    let inline utc (x:#key) =
        Convert.ToDateTime x
        |> fun x -> x.Subtract(DateTime(1970, 1, 1)).TotalMilliseconds
        |> int64 :> key

    let utcIfDatetime typecode (x:#key) =
        match typecode with
        | TypeCode.DateTime -> utc x
        | _ -> x :> key

    let upcastKeyValue xTypeCode (values:seq<#key*#value>) =
        values
        |> Seq.map (fun (k, v) ->
            let k' = utcIfDatetime xTypeCode k
            box [|k'; v :> value|])
        |> Seq.toArray

    let upcastKeyValueValue xTypeCode (values:seq<#key*#value*#value>) =
        values
        |> Seq.map (fun (k, v, v') ->
            let k' = utcIfDatetime xTypeCode k
            box [|k'; v :> value; v' :> value|])
        |> Seq.toArray

[<ReflectedDefinition>]
type Series =
    {
        Name : string
        Type : ChartType
        Values : obj []
        XType : TypeCode
    }
    
    static member SetName name series = { series with Name = name }

    static member internal New(chartType, values:seq<#value>) =
        {
            Name = ""
            Values =
                Seq.map box values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
        }

    static member internal New(name, chartType, values:seq<#value>) =
        {
            Name = name
            Values =
                Seq.map box values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
        }

    static member internal New(chartType, values:seq<#key*#value>) =
        let k, _ = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = ""
            Values = Utils.upcastKeyValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
        }

    static member internal New(chartType, values:seq<#key*#value*#value>) =
        let k, _, _ = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = ""
            Values = Utils.upcastKeyValueValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
        }

    static member internal New(name, chartType, values:seq<#key*#value>) =
        let k, _ = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = Utils.upcastKeyValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
        }

    static member internal New(name, chartType, values:seq<#key*#value*#value>) =
        let k, _, _ = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = Utils.upcastKeyValueValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
        }

    static member internal New(values:seq<#value*#value>, chartType) =
        let k, _ = Seq.head values
        {
            Name = ""
            Values =
                Seq.map (fun (v, v') -> box [|v :> value; v' :> value|]) values
                |> Seq.toArray
            Type = chartType
            XType = k.GetTypeCode()
        }

// support series with null values
//    static member Area(values:seq<#key*obj>) =
//        let k, _ = Seq.head values
//        {
//            Name = ""
//            Values =
//                values
//                |> Seq.map (fun (k, v) -> box [|box k; v|])
//                |> Seq.toArray
//            Type = Area
//            XType = k.GetTypeCode()
//        }

// support Deedle series
//    static member Area(values:Deedle.Series<'K, 'V>) =
//        let values' = Deedle.Series.observations values
//        let k, _ = Seq.head values'
//        let xTypeCode = (k :> key).GetTypeCode()
//        {
//            Name = ""
//            Values = upcastKeyValue xTypeCode values'             
//            Type = Area
//            XType = xTypeCode
//        }

    static member Area(values:seq<#value>) =
        Series.New(Area, values)

    static member Area(name, values:seq<#value>) =
        Series.New(name, Area, values)

    static member Area(values:seq<#key*#value>) =
        Series.New(Area, values)

    static member Area(name, values:seq<#key*#value>) =
        Series.New(name, Area, values)

    static member Areaspline(values:seq<#value>) =
        Series.New(Areaspline, values)

    static member Areaspline(name, values:seq<#value>) =
        Series.New(name, Areaspline, values)

    static member Areaspline(values:seq<#key*#value>) =
        Series.New(Areaspline, values)

    static member Areaspline(name, values:seq<#key*#value>) =
        Series.New(name, Areaspline, values)

    static member Arearange(values:seq<#key*#value*#value>) =
        Series.New(Arearange, values)

    static member Arearange(name, values:seq<#key*#value*#value>) =
        Series.New(name, Arearange, values)
        
    static member Bar(values:seq<#value>) =
        Series.New(Bar, values)

    static member Bar(name, values:seq<#value>) =
        Series.New(name, Bar, values)

    static member Bar(values:seq<#key*#value>) =
        Series.New(Bar, values)

    static member Bar(name, values:seq<#key*#value>) =
        Series.New(name, Bar, values)

    static member Bubble(values:seq<#key*#value*#value>) =
        Series.New(Bubble, values)

    static member Bubble(values:seq<#value*#value>) =
        Series.New(Bubble, values)

    static member Bubble(name, values:seq<#key*#value*#value>) =
        Series.New(name, Bubble, values)

    static member Bubble(name, values:seq<#value*#value>) =
        Series.New(name, Bubble, values)

    static member Column(values:seq<#value>) =
        Series.New(Column, values)

    static member Column(name, values:seq<#value>) =
        Series.New(name, Column, values)

    static member Column(values:seq<#key*#value>) =
        Series.New(Column, values)

    static member Column(name, values:seq<#key*#value>) =
        Series.New(name, Column, values)

    static member Donut(values:seq<#value>) =
        Series.New(Donut, values)

    static member Donut(name, values:seq<#value>) =
        Series.New(name, Donut, values)

    static member Donut(values:seq<#key*#value>) =
        Series.New(Donut, values)

    static member Donut(name, values:seq<#key*#value>) =
        Series.New(name, Donut, values)

    static member Funnel(values:seq<#value>) =
        Series.New(Funnel, values)

    static member Funnel(name, values:seq<#value>) =
        Series.New(name, Funnel, values)

    static member Funnel(values:seq<#key*#value>) =
        Series.New(Funnel, values)

    static member Funnel(name, values:seq<#key*#value>) =
        Series.New(name, Funnel, values)

    static member Line(values:seq<#value>) =
        Series.New(Line, values)

    static member Line(name, values:seq<#value>) =
        Series.New(name, Line, values)

    static member Line(values:seq<#key*#value>) =
        Series.New(Line, values)

    static member Line(name, values:seq<#key*#value>) =
        Series.New(name, Line, values)

    static member PercentArea(values:seq<#value>) =
        Series.New(PercentArea, values)

    static member PercentArea(name, values:seq<#value>) =
        Series.New(name, PercentArea, values)

    static member PercentArea(values:seq<#key*#value>) =
        Series.New(PercentArea, values)

    static member PercentArea(name, values:seq<#key*#value>) =
        Series.New(name, PercentArea, values)

    static member PercentBar(values:seq<#value>) =
        Series.New(PercentBar, values)

    static member PercentBar(name, values:seq<#value>) =
        Series.New(name, PercentBar, values)

    static member PercentBar(values:seq<#key*#value>) =
        Series.New(PercentBar, values)

    static member PercentBar(name, values:seq<#key*#value>) =
        Series.New(name, PercentBar, values)

    static member PercentColumn(values:seq<#value>) =
        Series.New(PercentColumn, values)

    static member PercentColumn(name, values:seq<#value>) =
        Series.New(name, PercentColumn, values)

    static member PercentColumn(values:seq<#key*#value>) =
        Series.New(PercentColumn, values)

    static member PercentColumn(name, values:seq<#key*#value>) =
        Series.New(name, PercentColumn, values)

    static member Pie(values:seq<#value>) =
        Series.New(Pie, values)

    static member Pie(name, values:seq<#value>) =
        Series.New(name, Pie, values)

    static member Pie(values:seq<#key*#value>) =
        Series.New(Pie, values)

    static member Pie(name, values:seq<#key*#value>) =
        Series.New(name, Pie, values)

    static member Radar(values:seq<#value>) =
        Series.New(Radar, values)

    static member Radar(name, values:seq<#value>) =
        Series.New(name, Radar, values)

    static member Radar(values:seq<#key*#value>) =
        Series.New(Radar, values)

    static member Radar(name, values:seq<#key*#value>) =
        Series.New(name, Radar, values)

    static member Scatter(values:seq<#value>) =
        Series.New(Scatter, values)

    static member Scatter(name, values:seq<#value>) =
        Series.New(name, Scatter, values)

    static member Scatter(values:seq<#key*#value>) =
        Series.New(Scatter, values)

    static member Scatter(name, values:seq<#key*#value>) =
        Series.New(name, Scatter, values)

    static member Spline(values:seq<#value>) =
        Series.New(Spline, values)

    static member Spline(name, values:seq<#value>) =
        Series.New(name, Spline, values)

    static member Spline(values:seq<#key*#value>) =
        Series.New(Spline, values)

    static member Spline(name, values:seq<#key*#value>) =
        Series.New(name, Spline, values)

    static member StackedArea(values:seq<#value>) =
        Series.New(StackedArea, values)

    static member StackedArea(name, values:seq<#value>) =
        Series.New(name, StackedArea, values)

    static member StackedArea(values:seq<#key*#value>) =
        Series.New(StackedArea, values)

    static member StackedArea(name, values:seq<#key*#value>) =
        Series.New(name, StackedArea, values)

    static member StackedBar(values:seq<#value>) =
        Series.New(StackedBar, values)

    static member StackedBar(name, values:seq<#value>) =
        Series.New(name, StackedBar, values)

    static member StackedBar(values:seq<#key*#value>) =
        Series.New(StackedBar, values)

    static member StackedBar(name, values:seq<#key*#value>) =
        Series.New(name, StackedBar, values)

    static member StackedColumn(values:seq<#value>) =
        Series.New(StackedColumn, values)

    static member StackedColumn(name, values:seq<#value>) =
        Series.New(name, StackedColumn, values)

    static member StackedColumn(values:seq<#key*#value>) =
        Series.New(StackedColumn, values)

    static member StackedColumn(name, values:seq<#key*#value>) =
        Series.New(name, StackedColumn, values)
