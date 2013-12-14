#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\bin\release\FsPlot.dll"""

open FsPlot.Charting
open FsPlot.DataSeries

let data = ["Chrome", 233; "Firefox", 141; "IE", 256]
    
// Create a pie chart
let chart = Highcharts.Pie data


// Display a legend
chart.ShowLegend()


// Update the chart's data
chart.SetData ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208]


// Update the chart's data in a more structured way
["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]
|> Series.New "Browser Share" ChartType.Pie
|> chart.SetData


// Add a title
chart.SetTitle "Website Visitors By Browser"

open System

module Area =
    
    let area1 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Highcharts.Area(salesData, "Company Sales", true)

    let area2 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Series.New "Sales" ChartType.Area
        |> fun x -> Highcharts.Area(x, "Company Sales", true)

    let area3 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]        
        let expensesData = ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
        Highcharts.Area([salesData; expensesData], "Company Performance", true)

    let area4 =
        let sales =
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            |> Series.New "Sales" ChartType.Area
        let expenses =
            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
            |> Series.New "Expenses" ChartType.Area
        Highcharts.Area([sales; expenses], "Company Performance", true)

    // datetime x axis
    let area5 =
        [
            DateTime.Now, 1000
            DateTime.Now.AddDays(1.), 1170
            DateTime.Now.AddDays(4.), 560
            DateTime.Now.AddDays(8.), 1030
        ]
        |> Highcharts.Area

    // linear x axis
    let area6 =
        [
            1950, 1000
            1964, 1170
            1975, 560
            1982, 1030
        ]
        |> Highcharts.Area

module Bar =
    
    let bar1 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Highcharts.Bar(salesData, "Company Sales", true)

    let bar2 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Series.New "Sales" ChartType.Bar
        |> fun x -> Highcharts.Bar(x, "Company Sales", true)

    let bar3 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]        
        let expensesData = ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
        Highcharts.Bar([salesData; expensesData], "Company Performance", true)

    let bar4 =
        let sales =
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            |> Series.New "Sales" ChartType.Bar
        let expenses =
            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
            |> Series.New "Expenses" ChartType.Bar
        Highcharts.Bar([sales; expenses], "Company Performance", true)

    // datetime x axis
    let bar5 =
        [
            DateTime.Now, 1000
            DateTime.Now.AddDays(1.), 1170
            DateTime.Now.AddDays(4.), 560
            DateTime.Now.AddDays(8.), 1030
        ]
        |> Highcharts.Bar

    // linear x axis
    let bar6 =
        [
            950, 1000
            964, 1170
            975, 560
            982, 1030
        ]
        |> Highcharts.Bar

module Column =
    
    let column1 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Highcharts.Column(salesData, "Company Sales", true)

    let column2 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Series.New "Sales" ChartType.Column
        |> fun x -> Highcharts.Column(x, "Company Sales", true)

    let column3 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]        
        let expensesData = ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
        Highcharts.Column([salesData; expensesData], "Company Performance", true)

    let column4 =
        let sales =
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            |> Series.New "Sales" ChartType.Column
        let expenses =
            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
            |> Series.New "Expenses" ChartType.Column
        Highcharts.Column([sales; expenses], "Company Performance", true)

    // datetime x axis
    let column5 =
        [
            DateTime.Now, 1000
            DateTime.Now.AddDays(1.), 1170
            DateTime.Now.AddDays(4.), 560
            DateTime.Now.AddDays(8.), 1030
        ]
        |> Highcharts.Column

    // linear x axis
    let column6 =
        [
            950, 1000
            964, 1170
            975, 560
            982, 1030
        ]
        |> Highcharts.Bar

module Pie =
    
    let pie1 =
        let data = ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]
        Highcharts.Pie(data, "Website Visitors By Browser", true)

    let pie2 =
        ["Chrome", 233; "Firefox", 141; "IE", 256; "Safari", 208; "Others", 75]
        |> Series.New "Browser Share" ChartType.Pie
        |> fun x -> Highcharts.Pie(x, "Website Visitors By Browser", true)

module Line =
    
    let line1 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        Highcharts.Line(salesData, "Company Sales", true)

    let line2 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Series.New "Sales" ChartType.Line
        |> fun x -> Highcharts.Line(x, "Company Sales", true)

    let line3 =
        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]        
        let expensesData = ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
        Highcharts.Line([salesData; expensesData], "Company Performance", true)

    let line4 =
        let sales =
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            |> Series.New "Sales" ChartType.Line
        let expenses =
            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
            |> Series.New "Expenses" ChartType.Line
        Highcharts.Line([sales; expenses], "Company Performance", true)

    // datetime x axis
    let line5 =
        [
            DateTime.Now, 1000
            DateTime.Now.AddDays(1.), 1170
            DateTime.Now.AddDays(4.), 560
            DateTime.Now.AddDays(8.), 1030
        ]
        |> Highcharts.Line

    // linear x axis
    let line6 =
        [
            950, 1000
            964, 1170
            975, 560
            982, 1030
        ]
        |> Highcharts.Line