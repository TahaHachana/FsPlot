module FsPlot.HighchartsOptions

/// Stacking settings for area, bar and column charts.
[<ReflectedDefinition>]
type Stacking =
    | Disabled
    | Normal
    | Percent

/// Center and size settings for pie chart in a combination chart.
[<ReflectedDefinition>]
type PieOptions =
    {
        Center : int []
        Size : obj
    }