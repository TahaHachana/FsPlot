#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\bin\debug\FsPlot.dll"""

open FsPlot

let data = [|"IE", 200; "Chrome", 253; "Firefox", 158|]

let chart = Highcharts.Pie(data, "Window Title", "Chart Title")

chart.Data <- [|"IE", 200; "Chrome", 253; "Firefox", 158; "Opera", 59|]

chart.Title <- Some "New Title"

let js = chart.Js
let html = chart.Html
