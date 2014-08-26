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
    | Geo
    | Map

module internal Utils =

    let inline utc (x:#key) =
        Convert.ToDateTime x
        |> fun x -> x.Subtract(DateTime(1970, 1, 1)).TotalMilliseconds
        |> int64 
        :> key

    let inline utcIfDatetime typecode (x:#key) =
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
        // TODO: store values as DataPoint []
        Values : obj []
        XType : TypeCode
        YType : TypeCode
    }

    static member WithName name series = {series with Name = name}

    static member New(chartType, values:seq<#key*#value>, name) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = match name with None -> "" | Some x -> x
            Values = Utils.upcastKeyValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member New(chartType, values:seq<#value>, name) =
        {
            Name = match name with None -> "" | Some x -> x
            Values =
                Seq.map box values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
            YType = (Seq.head values).GetTypeCode()
        }

    static member New(chartType, values:seq<#key*#value*#value>, name) =
        let k, v, _ = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = match name with None -> "" | Some x -> x
            Values = Utils.upcastKeyValueValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member New(values:seq<#value*#value>, chartType) =
        let v, _ = Seq.head values
        {
            Name = ""
            Values =
                Seq.map (fun (v, v') -> box [|v :> value; v' :> value|]) values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member Area(values:seq<#key*#value>, ?name) =
        Series.New(Area, values, name)

    static member Area(values:seq<#value>, ?name) =
        Series.New(Area, values, name)

    static member Arearange(values:seq<#key*#value*#value>, ?name) =
        Series.New(Arearange, values, name)

    static member Arearange(values:seq<#value * #value>, ?name) =
        Series.New(Arearange, values, name)

    static member Areaspline(values:seq<#key*#value>, ?name) =
        Series.New(Areaspline, values, name)

    static member Areaspline(values:seq<#value>, ?name) =
        Series.New(Areaspline, values, name)

    static member Bar(values:seq<#key*#value>, ?name) =
        Series.New(Bar, values, name)

    static member Bar(values:seq<#value>, ?name) =
        Series.New(Bar, values, name)

    static member Bubble(values:seq<#key*#value*#value>, ?name) =
        Series.New(Bubble, values, name)

    static member Bubble(values:seq<#value * #value>, ?name) =
        Series.New(Bubble, values, name)

    static member Column(values:seq<#key*#value>, ?name) =
        Series.New(Column, values, name)

    static member Column(values:seq<#value>, ?name) =
        Series.New(Column, values, name)

    static member Donut(values:seq<#key*#value>, ?name) =
        Series.New(Donut, values, name)

    static member Donut(values:seq<#value>, ?name) =
        Series.New(Donut, values, name)

    static member Funnel(values:seq<#key*#value>, ?name) =
        Series.New(Funnel, values, name)

    static member Funnel(values:seq<#value>, ?name) =
        Series.New(Funnel, values, name)

    static member Line(values:seq<#key*#value>, ?name) =
        Series.New(Line, values, name)

    static member Line(values:seq<#value>, ?name) =
        Series.New(Line, values, name)

    static member PercentArea(values:seq<#key*#value>, ?name) =
        Series.New(PercentArea, values, name)

    static member PercentArea(values:seq<#value>, ?name) =
        Series.New(PercentArea, values, name)

    static member PercentBar(values:seq<#key*#value>, ?name) =
        Series.New(PercentBar, values, name)

    static member PercentBar(values:seq<#value>, ?name) =
        Series.New(PercentBar, values, name)

    static member PercentColumn(values:seq<#key*#value>, ?name) =
        Series.New(PercentColumn, values, name)

    static member PercentColumn(values:seq<#value>, ?name) =
        Series.New(PercentColumn, values, name)

    static member Pie(values:seq<#key*#value>, ?name) =
        Series.New(Pie, values, name)

    static member Pie(values:seq<#value>, ?name) =
        Series.New(Pie, values, name)

    static member Radar(values:seq<#key*#value>, ?name) =
        Series.New(Radar, values, name)

    static member Radar(values:seq<#value>, ?name) =
        Series.New(Radar, values, name)

    static member Scatter(values:seq<#key*#value>, ?name) =
        Series.New(Scatter, values, name)

    static member Scatter(values:seq<#value>, ?name) =
        Series.New(Scatter, values, name)

    static member Spline(values:seq<#key*#value>, ?name) =
        Series.New(Spline, values, name)

    static member Spline(values:seq<#value>, ?name) =
        Series.New(Spline, values, name)

    static member StackedArea(values:seq<#key*#value>, ?name) =
        Series.New(StackedArea, values, name)

    static member StackedArea(values:seq<#value>, ?name) =
        Series.New(StackedArea, values, name)

    static member StackedBar(values:seq<#key*#value>, ?name) =
        Series.New(StackedBar, values, name)

    static member StackedBar(values:seq<#value>, ?name) =
        Series.New(StackedBar, values, name)

    static member StackedColumn(values:seq<#key*#value>, ?name) =
        Series.New(StackedColumn, values, name)

    static member StackedColumn(values:seq<#value>, ?name) =
        Series.New(StackedColumn, values, name)
    
    static member Geo(values:seq<string * #value>, ?name) =
        Series.New(Geo, values, name)

    static member Map(values:seq<#key * #value>, ?name) =
        Series.New(Map, values, name)
