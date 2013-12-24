namespace System
open System.Reflection

[<assembly: AssemblyTitle("FsPlot")>]
[<assembly: AssemblyProduct("FsPlot")>]
[<assembly: AssemblyDescription("A Data Visualization Library for F# Using HTML5/JavaScript.")>]
[<assembly: AssemblyVersionAttribute("0.2.22")>]
[<assembly: AssemblyFileVersionAttribute("0.2.22")>]
()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.2.22"