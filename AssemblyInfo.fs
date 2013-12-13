namespace System
open System.Reflection

[<assembly: AssemblyTitle("FsPlot")>]
[<assembly: AssemblyProduct("FsPlot")>]
[<assembly: AssemblyDescription("A Data Visualization Library for F# Using HTML5")>]
[<assembly: AssemblyVersionAttribute("0.1.9")>]
[<assembly: AssemblyFileVersionAttribute("0.1.9")>]
()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1.9"