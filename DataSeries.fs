module FsPlot.DataSeries

open System
open Microsoft.FSharp.Quotations

type key = IConvertible
type value = IConvertible

[<ReflectedDefinition>]
type B =
    static member Box (x:obj) = box x

let boxInfo = typeof<B>.GetMethod("Box")

let utc (x:#key) =
    (Convert.ToDateTime x)
        .Subtract(DateTime(1970, 1,1))
        .TotalMilliseconds
        |> int64
        :> key

let private upcastKeyValue xTypeCode (values:seq<#key*#value>) =
    values
    |> Seq.map (fun (k, v) ->
        let k' =
            match xTypeCode with
            | TypeCode.DateTime -> utc k
            | _ -> k :> key
        box [|k'; v :> value|])
    |> Seq.toArray

let private upcastKeyValueValue xTypeCode (values:seq<#key*#value*#value>) =
    values
    |> Seq.map (fun (k, v, v') ->
        let k' =
            match xTypeCode with
            | TypeCode.DateTime -> utc k
            | _ -> k :> key
        box [|k'; v :> value; v' :> value|])
    |> Seq.toArray

[<ReflectedDefinition>]
type ChartType =
    | Area
    | Bar
    | Bubble
    | Column
    | Combination
    | Line
    | Pie
    | Scatter

type Series =
    {
        Name : string
        Type : ChartType
        Values : obj []
        XType : TypeCode
        YType : TypeCode
    }
    
    static member SetName name series = { series with Name = name }

    static member New(chartType, values:seq<#value>) =
        let v = Seq.head values
        {
            Name = ""
            Values =
                Seq.map (fun v -> box v) values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member New(name, chartType, values:seq<#value>) =
        let v = Seq.head values
        {
            Name = name
            Values =
                Seq.map (fun v -> box v) values
                |> Seq.toArray
            Type = chartType
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member New(chartType, values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = ""
            Values = upcastKeyValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member New(name, chartType, values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values               
            Type = chartType
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member Area(values:seq<#value>) =
        let v = Seq.head values
        {
            Name = ""
            Values =
                Seq.map (fun v -> box v) values
                |> Seq.toArray
            Type = Area
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member Area(name, values:seq<#value>) =
        let v = Seq.head values
        {
            Name = name
            Values =
                Seq.map (fun v -> box v) values
                |> Seq.toArray
            Type = Area
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member Area(values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = ""
            Values = upcastKeyValue xTypeCode values               
            Type = Area
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member Area(name, values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values               
            Type = Area
            XType = xTypeCode
            YType = v.GetTypeCode()
        }
        
    static member Bar(values:seq<#value>) =
        let v = Seq.head values
        {
            Name = ""
            Values =
                Seq.map (fun v -> box v) values
                |> Seq.toArray
            Type = Bar
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member Bar(name, values:seq<#value>) =
        let v = Seq.head values
        {
            Name = name
            Values =
                Seq.map (fun v -> box v) values
                |> Seq.toArray
            Type = Bar
            XType = TypeCode.Empty
            YType = v.GetTypeCode()
        }

    static member Bar(values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = ""
            Values = upcastKeyValue xTypeCode values               
            Type = Bar
            XType = xTypeCode
            YType = v.GetTypeCode()
        }

    static member Bar(name, values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xTypeCode = k.GetTypeCode()
        {
            Name = name
            Values = upcastKeyValue xTypeCode values               
            Type = Bar
            XType = xTypeCode
            YType = v.GetTypeCode()
        }


//    static member New(name, chartType, values:seq<#value>) =
//        let v = Seq.head values
////        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values =
//                Seq.map (fun v -> box v) values
//                |> Seq.toArray
//            Type = chartType
//            XType = TypeCode.Empty
//            YType = v.GetTypeCode()
//            Size = TypeCode.Empty
//        }
//
//    static member NewBubble(name, values:seq<#value*#value>) =
//        let v, v' = Seq.head values
////        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values =
//                Seq.map (fun (v, v') -> box [|v :> value; v' :> value|]) values
//                |> Seq.toArray
//            Type = Bubble
//            XType = TypeCode.Empty
//            YType = v.GetTypeCode()
//            Size = v'.GetTypeCode()
//        }
//
//    static member Area name (values:seq<#key*#value>) =
//        let k, v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values = upcastKeyValue xTypeCode values
////                Seq.map (upcastKeyValue xTypeCode) values
////                |> Seq.toArray
//            Type = ChartType.Area
//            XType = xTypeCode
//            YType = v.GetTypeCode()
//            Size = TypeCode.Empty
//        }
//
////    static member Area name (values:seq<#value>) =
////        let v = Seq.head values
//////        let xTypeCode = k.GetTypeCode()
////        {
////            Name = name
////            Values =
////                Seq.map (fun v -> box v) values
////                |> Seq.toArray
////            Type = Area
////            XType = TypeCode.Empty
////            YType = v.GetTypeCode()
////            Size = TypeCode.Empty
////        }
//
//    static member Bar name (values:seq<#key*#value>) =
//        let k, v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values = upcastKeyValue xTypeCode values
////                Seq.map (upcastKeyValue xTypeCode) values
////                |> Seq.toArray
//            Type = ChartType.Bar
//            XType = xTypeCode
//            YType = v.GetTypeCode()
//            Size = TypeCode.Empty
//        }
//
//    static member Bubble name (values:seq<#key*#value*#value>) =
//        let k, v, v' = Seq.head values
//        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values = upcastKeyValueValue xTypeCode values
////                Seq.map (upcastKeyValue xTypeCode) values
////                |> Seq.toArray
//            Type = ChartType.Bubble
//            XType = xTypeCode
//            YType = v.GetTypeCode()
//            Size = v'.GetTypeCode()
//        }
//
//    static member Column name (values:seq<#key*#value>) =
//        let k, v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values = upcastKeyValue xTypeCode values
////                Seq.map (upcastKeyValue xTypeCode) values
////                |> Seq.toArray
//            Type = ChartType.Column
//            XType = xTypeCode
//            YType = v.GetTypeCode()
//            Size = TypeCode.Empty
//        }
//
//    static member Line name (values:seq<#key*#value>) =
//        let k, v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values = upcastKeyValue xTypeCode values
////                Seq.map (upcastKeyValue xTypeCode) values
////                |> Seq.toArray
//            Type = ChartType.Line
//            XType = xTypeCode
//            YType = v.GetTypeCode()
//            Size = TypeCode.Empty
//        }
//
//    static member Pie name (values:seq<#key*#value>) =
//        let k, v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values = upcastKeyValue xTypeCode values
////                Seq.map (upcastKeyValue xTypeCode) values
////                |> Seq.toArray
//            Type = ChartType.Pie
//            XType = xTypeCode
//            YType = v.GetTypeCode()
//            Size = TypeCode.Empty
//        }
//
////    static member Pie(name, values:seq<#value>) =
////        let v = Seq.head values
//////        let xTypeCode = k.GetTypeCode()
////        {
////            Name = name
////            Values =
////                Seq.map (fun v -> null, v :> value) values
////                |> Seq.toArray
////            Type = ChartType.Pie
////            XType = TypeCode.Empty
////            YType = v.GetTypeCode()
////        }
//
//    static member Scatter name (values:seq<#key*#value>) =
//        let k, v = Seq.head values
//        let xTypeCode = k.GetTypeCode()
//        {
//            Name = name
//            Values = upcastKeyValue xTypeCode values
////                Seq.map (upcastKeyValue xTypeCode) values
////                |> Seq.toArray
//            Type = ChartType.Scatter
//            XType = xTypeCode
//            YType = v.GetTypeCode()
//            Size = TypeCode.Empty
//        }
//
////let s =
////    {
////        Name = "Test"
//////        Values : (key*value) []
////        Values = [|box "Chrome"|]
////        Type = ChartType.Area
////        XType = TypeCode.String
////        YType = TypeCode.Int32
////    }
////
////
////let e' =
////    Expr.NewArray(
////        typeof<obj>,
////        [
////            for e in [|box "Chrome"|] do
////                yield Expr.Call(boxInfo, [Expr.Value (unbox e)])
////////                                        yield Expr.NewTuple(
////////                                            [
////////                                                Expr.Value(k)
////////                                                Expr.Value (v)
////////                                            ])
////        ])
////
////quoteDataSeriesArr [|s|]
////
////let e =
////    <@
////        {
////            Name = "Test"
////    //        Values : (key*value) []
////            Values = [|box "Chrome"|]
////            Type = ChartType.Area
////            XType = TypeCode.String
////            YType = TypeCode.Int32
////        }
////
////    @>
////
