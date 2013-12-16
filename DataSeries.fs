module FsPlot.DataSeries

open System
open Microsoft.FSharp.Quotations

// 1275696000000
// 1273014000000

//let date = DateTime(2010, 5, 5).ToUniversalTime().Subtract(DateTime(1970, 1,1)).TotalMilliseconds |> int64 |> string |> float

//let date' = DateTime(2010, 2, 5).ToShortDateString()
//let date'' = 

type key = IConvertible
type value = IConvertible

[<ReflectedDefinition>]
type B =
    static member Box (x:obj) = box x

let boxInfo = typeof<B>.GetMethod("Box")

let private upcastKeyValue xTypeCode (values:seq<#key*#value>) =
    values
    |> Seq.map (fun (k, v) ->
        let k' =
            match xTypeCode with
            | TypeCode.DateTime ->
                (Convert.ToDateTime k)
                    .Subtract(DateTime(1970, 1,1))
                    .TotalMilliseconds
                    |> int64
                    :> key
    //                |> box
            | _ -> k :> key
        box [|k'; v :> value|])
//        Expr.Call(boxInfo, [Expr.NewArray(typeof<obj>, [Expr.Value k'; Expr.Value v])]))
    |> Seq.toArray
//    k', v :> value

let private upcastKeyValueValue xTypeCode (values:seq<#key*#value*#value>) =
    values
    |> Seq.map (fun (k, v, v') ->
        let k' =
            match xTypeCode with
            | TypeCode.DateTime ->
                (Convert.ToDateTime k)
                    .Subtract(DateTime(1970, 1,1))
                    .TotalMilliseconds
                    |> int64
                    :> key
    //                |> box
            | _ -> k :> key
        box [|k'; v :> value; v' :> value|])
//        Expr.Call(boxInfo, [Expr.NewArray(typeof<obj>, [Expr.Value k'; Expr.Value v])]))
    |> Seq.toArray

//    box <| Expr.NewArray(typeof<obj>, [Expr.Value k'; Expr.Value (box v)])
//    box [|k'; v :> value|]
//    box k' //[|k'; box v|]

[<ReflectedDefinition>]
type ChartType = Area | Bar | Bubble | Column | Combination | Line | Pie | Scatter

type Series =
    {
        Name : string
//        Values : (key*value) []
        Values : obj []
        Type : ChartType
        XType : TypeCode
        YType : TypeCode
        Size : TypeCode
    }

    static member New(name, chartType, values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values               
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = chartType
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
        }

    static member New(name, chartType, values:seq<#value>) =
        let v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (fun v -> box v) values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
        }

    static member NewBubble(name, values:seq<#value*#value>) =
        let v, v' = Seq.head values
//        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values =
                Seq.map (fun (v, v') -> box [|v :> value; v' :> value|]) values
                |> Seq.toArray
            Type = Bubble
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
            Size = v'.GetTypeCode()
        }

    static member Area name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = ChartType.Area
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
        }

    static member Bar name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = ChartType.Bar
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
        }

    static member Bubble name (values:seq<#key*#value*#value>) =
        let k, v, v' = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValueValue xTypeCode values
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = ChartType.Bubble
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = v'.GetTypeCode()
        }

    static member Column name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = ChartType.Column
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
        }

    static member Line name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = ChartType.Line
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
        }

    static member Pie name (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = ChartType.Pie
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
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
            Values = upcastKeyValue xTypeCode values
//                Seq.map (upcastKeyValue xTypeCode) values
//                |> Seq.toArray
            Type = ChartType.Scatter
            XType = xTypeCode
            YType = v.GetTypeCode()
            Size = TypeCode.Empty
        }

//let s =
//    {
//        Name = "Test"
////        Values : (key*value) []
//        Values = [|box "Chrome"|]
//        Type = ChartType.Area
//        XType = TypeCode.String
//        YType = TypeCode.Int32
//    }
//
//
//let e' =
//    Expr.NewArray(
//        typeof<obj>,
//        [
//            for e in [|box "Chrome"|] do
//                yield Expr.Call(boxInfo, [Expr.Value (unbox e)])
//////                                        yield Expr.NewTuple(
//////                                            [
//////                                                Expr.Value(k)
//////                                                Expr.Value (v)
//////                                            ])
//        ])
//
//quoteDataSeriesArr [|s|]
//
//let e =
//    <@
//        {
//            Name = "Test"
//    //        Values : (key*value) []
//            Values = [|box "Chrome"|]
//            Type = ChartType.Area
//            XType = TypeCode.String
//            YType = TypeCode.Int32
//        }
//
//    @>
//
