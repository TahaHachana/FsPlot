module FsPlot.DataSeries

open System
// 1275696000000
// 1273014000000

let date = DateTime(2010, 5, 5).ToUniversalTime().Subtract(DateTime(1970, 1,1)).TotalMilliseconds |> int64 |> string |> float

//let date' = DateTime(2010, 2, 5).ToShortDateString()
//let date'' = 

type key = IConvertible
type value = IConvertible


let private upcastValue (k:#key, v:#value) = k :> key, v :> value

let private upcastValue' (k:#key, v:#value) =
    let date = (Convert.ToDateTime k).Subtract(DateTime(1970, 1,1)).TotalMilliseconds |> int64 |> string
    date :> key, v :> value
 
type ChartType = Area | Pie

type Series =
    {
        Name : string
        Values : (key*value) []
        Type : ChartType
        XType : TypeCode
        YType : TypeCode
    }

    static member New name chartType (values:seq<#key*#value>) =
        let k, v = Seq.head values
        let xType, yType = k.GetTypeCode(), v.GetTypeCode()
        let valuesArr =
            match xType with
            | TypeCode.DateTime ->
                Seq.map upcastValue' values
                |> Seq.toArray
            | _ ->
                Seq.map upcastValue values
                |> Seq.toArray

        {
            Name = name
            Values = valuesArr
            Type = chartType
            XType = xType
            YType = yType
        }
