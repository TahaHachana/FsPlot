open System.Diagnostics
open System.IO
open System.Text.RegularExpressions

let nuget = Path.Combine(__SOURCE_DIRECTORY__, "nuget.exe")
let nuspec = Path.Combine(__SOURCE_DIRECTORY__, "FsPlot.nuspec")
let packArgs = sprintf "pack %s" nuspec

let version =
    File.ReadAllText nuspec
    |> fun x ->
        Regex("<version>(.+?)</version>")
            .Match(x)
            .Groups
            .[1]
            .Value

let nupkg =
    Path.Combine
        (
            System.Environment.CurrentDirectory,
            "FsPlot." + version + ".nupkg"
        )

let pushArgs = sprintf "push %s" nupkg

let startProcess args =
    let ``process`` = Process.Start(nuget, args)
    ``process``.WaitForExit()

startProcess packArgs

startProcess pushArgs
