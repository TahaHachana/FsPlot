module FsPlot.Config

open FsPlot.Data
open System

[<ReflectedDefinition>]
type AxisType =
    | Category
    | DateTime
    | Linear

[<ReflectedDefinition>]
type ChartConfig =
    {
        Categories : string []
        Data : Series []
        Legend : bool
        Subtitle : string option
        Title : string option
        Tooltip : string option
        Type : ChartType
        XAxis : AxisType
        XTitle : string option
        YTitle : string option
    }

    static member New (categories:string []) (data:Series []) legend subtitle title tooltip chartType xTitle yTitle =
        let xAxisType =
            match categories.Length with
            | 0 ->
                let fstSeries = Array.get data 0
                match fstSeries.XType with
                | TypeCode.DateTime -> DateTime
                | TypeCode.String -> Category
                | _ -> Linear
            | _ -> Category
        {
            Categories = categories
            Data = data
            Legend = legend
            Subtitle = subtitle
            Title = title
            Tooltip = tooltip
            Type = chartType
            XAxis = xAxisType
            XTitle = xTitle
            YTitle = yTitle
        }

//    member __.Fields =
//        __.Categories,
//        __.Data,
//        __.Legend,
//        __.Subtitle,
//        __.Title,
//        __.Tooltip,
//        __.Type,
//        __.XTitle,
//        __.YTitle
