#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
#r """.\bin\release\FsPlot.dll"""

open System
open FsPlot.Options
open FsPlot.Charting
open FsPlot.DataSeries

module Area =
    
    let area1 =
        Series.Area [1000; 1170; 560; 1030]
        |> Highcharts.Area

    let area2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Area

    let area3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Area

    let area4 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Seq.ofList
        |> Highcharts.Area

    let area5 =
        [
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        ]
        |> Seq.ofList
        |> Highcharts.Area

    let area6 =
        [
            Series.Area ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Series.Area ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Seq.ofList
        |> Highcharts.Area

    let area7 =
        [
            Seq.ofList ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Seq.ofList ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Seq.ofList
        |> Highcharts.Area

module Areaspline =
    
    let areaspline1 =
        Series.Areaspline [1000; 1170; 560; 1030]
        |> Highcharts.Areaspline

    let areaspline2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Areaspline

    let areaspline3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Areaspline

    let areaspline4 =
        [
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Seq.ofList
        |> Highcharts.Areaspline

    let areaspline5 =
        [
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
        ]
        |> Seq.ofList
        |> Highcharts.Areaspline

    let areaspline6 =
        [
            Series.Area ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Series.Area ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Seq.ofList
        |> Highcharts.Areaspline

    let areaspline7 =
        [
            Seq.ofList ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
            Seq.ofList ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        ]
        |> Seq.ofList
        |> Highcharts.Areaspline

module Bar =
    
    let bar1 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Bar

    let bar2 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Bar

    let bar3 =
        Series.Bar [1000; 1170; 560; 1030]
        |> Highcharts.Bar

    let area4 =
        Series.Bar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Area

    let bar5 =
        [
            Seq.ofList ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Seq.ofList ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Seq.ofList
        |> Highcharts.Bar

    let bar6 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Seq.ofList
        |> Highcharts.Bar

    let bar7 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Seq.ofList
        |> Highcharts.Bar

    let bar8 =
        let sales =
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            |> Series.Bar
        let expenses =
            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
            |> Series.Bar
        Highcharts.Bar [sales; expenses]


    // datetime x axis
    let bar9 =
        [
            DateTime.Now, 1000
            DateTime.Now.AddDays(1.), 1170
            DateTime.Now.AddDays(4.), 560
            DateTime.Now.AddDays(8.), 1030
        ]
        |> Highcharts.Bar


    // linear x axis
    let bar10 =
        [
            1950, 1000
            1964, 1170
            1975, 560
            1982, 1030
        ]
        |> Highcharts.Bar

module Bubble =
    
    let bubble1 =
        Series.Bubble [97,36,79; 94,74,60; 68,76,58]
        |> Highcharts.Bubble












//let data = ["Chrome", 40.4; "Firefox", 36.5; "IE", 23.1]
//    
//// Create a pie chart
//let chart = Highcharts.Pie data
//
//// Display a legend
//chart.ShowLegend()
//
//// Update the chart's data
//chart.SetData ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 24.2]
//
//// Update the chart's data in a more structured way
//["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
//|> Series.Pie "Browser Share"
//|> chart.SetData
//
//// Add a title
//chart.SetTitle "Website Visitors By Browser"
//
//open System
//
//
//module Bar =
//
//    let bar1 =
//        [1000; 1170; 560; 1030]
//        |> Highcharts.Bar
//
//    let bar2 =
//        Series.New("Sales", Bar, [1000; 1170; 560; 1030])
//        |> Highcharts.Bar
//    
//    let bar3 =
//        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//        Highcharts.Bar(salesData, "Company Sales", true)
//
//    let bar4 =
//        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//        |> Series.Bar "Sales"
//        |> fun x -> Highcharts.Bar(x, "Company Sales", true)
//
//    let bar5 =
//        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]        
//        let expensesData = ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
//        Highcharts.Bar([salesData; expensesData], "Company Performance", true)
//
//    let bar6 =
//        let sales =
//            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//            |> Series.Bar "Sales"
//        let expenses =
//            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
//            |> Series.Bar "Expenses"
//        Highcharts.Bar([sales; expenses], "Company Performance", true)
//
//    // datetime x axis
//    let bar7 =
//        [
//            DateTime.Now, 1000
//            DateTime.Now.AddDays(1.), 1170
//            DateTime.Now.AddDays(4.), 560
//            DateTime.Now.AddDays(8.), 1030
//        ]
//        |> Highcharts.Bar
//
//    // linear x axis
//    let bar8 =
//        [
//            950, 1000
//            964, 1170
//            975, 560
//            982, 1030
//        ]
//        |> Highcharts.Bar
//
//module Combination =
//
////['Apples', 'Oranges', 'Pears', 'Bananas', 'Plums']
//
//    let jane =
//        ["Apples", 5; "Oranges", 6; "Pears", 4]
//        |> Series.Column "Jane"
//
//    let joe =
//        ["Apples", 4; "Oranges", 3; "Pears", 6]
//        |> Series.Column "Joe"
//
//    let average =
//        ["Apples", 4.5; "Oranges", 4.5; "Pears", 5.]
//        |> Series.Line "Average"
//
//    let c = Highcharts.Combine [jane; joe; average]
//    c.Categories <- ["Apples"; "Oranges"; "Bananas"]
//        
//module Column =
//
//    let column1 =
//        [1000; 1170; 560; 1030]
//        |> Highcharts.Column
//
//    let column2 =
//        Series.New("Sales", Column, [1000; 1170; 560; 1030])
//        |> Highcharts.Column
//    
//    let column3 =
//        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//        Highcharts.Column(salesData, "Company Sales", true)
//
//    let column4 =
//        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//        |> Series.Column "Sales"
//        |> fun x -> Highcharts.Column(x, "Company Sales", true)
//
//    let column5 =
//        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]        
//        let expensesData = ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
//        Highcharts.Column([salesData; expensesData], "Company Performance", true)
//
//    let column6 =
//        let sales =
//            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//            |> Series.Column "Sales"
//        let expenses =
//            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
//            |> Series.Column "Expenses"
//        Highcharts.Column([sales; expenses], "Company Performance", true)
//
//    // datetime x axis
//    let column8 =
//        [
//            DateTime.Now, 1000
//            DateTime.Now.AddDays(1.), 1170
//            DateTime.Now.AddDays(4.), 560
//            DateTime.Now.AddDays(8.), 1030
//        ]
//        |> Highcharts.Column
//
//    // linear x axis
//    let column9 =
//        [
//            950, 1000
//            964, 1170
//            975, 560
//            982, 1030
//        ]
//        |> Highcharts.Bar
//
//module Pie =
//    
//    let pie1 =
//        [30.3; 26.5; 18.8; 15.2 ; 8.2]
//        |> fun x -> Highcharts.Pie(x, categories=["Chrome"; "Firefox"; "IE"; "Safari"; "Others"])
//
//    pie1.Categories <- ["Chrome"; "Firefox"; "IE"; "Safari"; "Others"]
//
//    let pie2 =
//        Series.New("Browser Share", Pie, [30.3; 26.5; 18.8; 15.2 ; 8.2])
//        |> Highcharts.Pie
//
//    let pie3 =
//        ["Chrome", 30.3; "Firefox", 26.5; "IE", 18.8; "Safari", 15.2; "Others", 8.2]
//        |> Series.Pie "Browser Share"
//        |> fun x -> Highcharts.Pie(x, "Website Visitors By Browser", true)
//
//module Line =
//    
//    let line1 =
//        [1000; 1170; 560; 1030]
//        |> Highcharts.Line
//
//    let line2 =
//        Series.New("Sales", Line, [1000; 1170; 560; 1030])
//        |> Highcharts.Line
//
//    let line3 =
//        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//        Highcharts.Line(salesData, "Company Sales", true)
//
//    let line4 =
//        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//        |> Series.Line "Sales"
//        |> fun x -> Highcharts.Line(x, "Company Sales", true)
//
//    let line5 =
//        let salesData = ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]        
//        let expensesData = ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
//        Highcharts.Line([salesData; expensesData], "Company Performance", true)
//
//    let line6 =
//        let sales =
//            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
//            |> Series.Line "Sales"
//        let expenses =
//            ["2010", 600; "2011", 760; "2012", 420; "2013", 540]
//            |> Series.Line "Expenses"
//        Highcharts.Line([sales; expenses], "Company Performance", true)
//
//    // datetime x axis
//    let line7 =
//        [
//            DateTime.Now, 1000
//            DateTime.Now.AddDays(1.), 1170
//            DateTime.Now.AddDays(4.), 560
//            DateTime.Now.AddDays(8.), 1030
//        ]
//        |> Highcharts.Line
//
//    // linear x axis
//    let line8 =
//        [
//            950, 1000
//            964, 1170
//            975, 560
//            982, 1030
//        ]
//        |> Highcharts.Line
//
//module Scatter =
//
//    let scatter1 =
//        [12; 5; 14; 6; 4; 7]
//        |> Highcharts.Scatter
//
//    let scatter2 =
//        Series.New("Male", Scatter, [12; 5; 14; 6; 4; 7])
//        |> Highcharts.Scatter
//
//    let scatter3 =
//        [8, 12; 4, 5; 11, 14; 4, 6; 3, 4; 6, 7]
//        |> Highcharts.Scatter
//
//    let scatter4 =
//        let male =
//            [8, 12; 4, 5; 11, 14; 5, 6; 3, 4; 6, 7]
//            |> Series.Scatter "Male"
//        let female =
//            [7, 11; 5, 7; 11, 13; 4, 7; 3, 3; 6, 9]
//            |> Series.Scatter "Female"
//        Highcharts.Scatter [male; female]
//
//module Bubble =
//    
//    let bubble1 =
//        [97,36,79; 94,74,60; 68,76,58]
//        |> Highcharts.Bubble
//
//    let bubble2 =
//        [36,79; 74,60; 76,58]
//        |> Highcharts.Bubble
//
//    let bubble3 =
//        Series.NewBubble("", [36,79; 74,60; 76,58])
//        |> Highcharts.Bubble
//
//    let bubble4 =
//        let series1 = Series.Bubble "Series 1" [97,36,79; 94,74,60; 68,76,58; 64,87,56; 68,27,73]
//        let series2 = Series.Bubble "Series 1" [25,10,87; 2,75,59; 11,54,8; 86,55,93; 5,3,58]
//        Highcharts.Bubble [series1; series2]
