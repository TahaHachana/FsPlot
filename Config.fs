module FsPlot.Config

open FsPlot.Data
open System

[<ReflectedDefinition>]
type AxisType =
    | Category
    | DateTime
    | Linear

let inline private axisType (categories:string []) (data:Series []) =
    match categories.Length with
    | 0 ->
        let fstSeries = Array.get data 0
        match fstSeries.XType with
        | TypeCode.DateTime -> DateTime
        | TypeCode.String -> Category
        | _ -> Linear
    | _ -> Category

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

    static member New categories data legend subtitle title tooltip chartType xTitle yTitle =
        let xAxis = axisType categories data
        {
            Categories = categories
            Data = data
            Legend = legend
            Subtitle = subtitle
            Title = title
            Tooltip = tooltip
            Type = chartType
            XAxis = xAxis
            XTitle = xTitle
            YTitle = yTitle
        }

    static member New' categories data legend subtitle title tooltip chartType xTitle yTitle =
        let legend' = defaultArg legend false
        let categories' =
            match categories with 
            | None -> [||]
            | Some value -> Seq.toArray value
        ChartConfig.New categories' data legend' subtitle title tooltip chartType xTitle yTitle
