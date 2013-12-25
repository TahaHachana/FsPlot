module FsPlot.Options

[<ReflectedDefinition>]
type Stacking =
    | Disabled
    | Normal
    | Percent

[<ReflectedDefinition>]
type PieOptions =
    {
        Center : int []
        Size : obj
    }