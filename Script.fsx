#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\bin\release\FsPlot.dll"""

open FsPlot.Charting

let data = ["Chrome", 233; "Firefox", 141; "IE", 256]
    
// Create a pie chart
let chart = Highcharts.Pie(data, "Visitors By Browser Breakdown")

// Display a legend
chart.ShowLegend()

// Update the chart's data
chart.SetData ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]

// Update the chart's title
chart.SetTitle "Website Visitors By Browser Breakdown"
