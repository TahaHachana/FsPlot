module FsPlot.Google.Options

/// Configuration for how values are associated with bubble size.
[<ReflectedDefinition>]
type SizeAxis =
    {
        MinValue : float
        MaxValue : float
    }