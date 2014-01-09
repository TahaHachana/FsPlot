module FsPlot.Highcharts.Options

/// Stacking settings for area, bar and column charts.
[<ReflectedDefinition>]
type Stacking =
    | Disabled
    | Normal
    | Percent

/// Center and size settings for pie charts.
[<ReflectedDefinition>]
type PieOptions =
    {
        Center : int []
        Size : obj
    }