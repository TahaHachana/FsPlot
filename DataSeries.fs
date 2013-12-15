module FsPlot.DataSeries

open System
// 1275696000000
// 1273014000000

let date = DateTime(2010, 5, 5).ToUniversalTime().Subtract(DateTime(1970, 1,1)).TotalMilliseconds |> int64 |> string |> float

//let date' = DateTime(2010, 2, 5).ToShortDateString()
//let date'' = 

type key = IConvertible
type value = IConvertible

let private upcastKeyValue xTypeCode (k:#key, v:#value) =
    let k' =
        match xTypeCode with
        | TypeCode.DateTime ->
            (Convert.ToDateTime k)
                .Subtract(DateTime(1970, 1,1))
                .TotalMilliseconds
                |> int64
                :> key
        | _ -> k :> key
    k', v :> value

type ChartType = Area | Bar | Column | Line | Pie | Scatter

type Series =
    {
        Name : string
        Values : (key*value) []
        Type : ChartType
        XType : TypeCode
        YType : TypeCode
    }

    static member New(name, chartType, values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (upcastKeyValue xTypeCode) values
                |> Seq.toArray
            Type = chartType
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member New(name, chartType, values:seq<#value>) =
        let v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (fun v -> null, v :> value) values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member Area name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (upcastKeyValue xTypeCode) values
                |> Seq.toArray
            Type = ChartType.Area
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member Bar name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (upcastKeyValue xTypeCode) values
                |> Seq.toArray
            Type = ChartType.Bar
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member Column name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (upcastKeyValue xTypeCode) values
                |> Seq.toArray
            Type = ChartType.Column
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member Line name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (upcastKeyValue xTypeCode) values
                |> Seq.toArray
            Type = ChartType.Line
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member Pie name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (upcastKeyValue xTypeCode) values
                |> Seq.toArray
            Type = ChartType.Pie
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

//    static member Pie(name, values:seq<#value>) =
//        let v = Seq.head values
////        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values =
//                Seq.map (fun v -> null, v :> value) values
//                |> Seq.toArray
//            Type = ChartType.Pie
//            XType = TypeCode.Empty
//            YType = v.GetTypeCode()
//        }

    static member Scatter name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (upcastKeyValue xTypeCode) values
                |> Seq.toArray
            Type = ChartType.Scatter
            XType = xTypeCode
            YType = v.GetTypeCode()
        }