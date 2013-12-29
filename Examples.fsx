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

    let sales =
        ["2010", 1300; "2011", 1470; "2012", 840; "2013", 1330]
        |> Series.Area
        |> Series.SetName "Sales"

    let expenses =
        ["2010", 1000; "2011", 1170; "2012", 580; "2013", 1030]
        |> Series.Area
        |> Series.SetName "Expenses"

    let basicArea =
        [sales; expenses]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Company Performance"


//    let basicArea =
//        let usa =
//            [
//                (1945, 6); (1946, 11); (1947, 32); (1948, 110); (1949, 235); (1950, 369);
//                (1951, 640); (1952, 1005); (1953, 1436); (1954, 2063); (1955, 3057);
//                (1956, 4618); (1957, 6444); (1958, 9822); (1959, 15468); (1960, 20434);
//                (1961, 24126); (1962, 27387); (1963, 29459); (1964, 31056); (1965, 31982);
//                (1966, 32040); (1967, 31233); (1968, 29224); (1969, 27342); (1970, 26662);
//                (1971, 26956); (1972, 27912); (1973, 28999); (1974, 28965); (1975, 27826);
//                (1976, 25579); (1977, 25722); (1978, 24826); (1979, 24605); (1980, 24304);
//                (1981, 23464); (1982, 23708); (1983, 24099); (1984, 24357); (1985, 24237);
//                (1986, 24401); (1987, 24344); (1988, 23586); (1989, 22380); (1990, 21004);
//                (1991, 17287); (1992, 14747); (1993, 13076); (1994, 12555); (1995, 12144);
//                (1996, 11009); (1997, 10950); (1998, 10871); (1999, 10824); (2000, 10577);
//                (2001, 10527); (2002, 10475); (2003, 10421); (2004, 10358); (2005, 10295);
//                (2006, 10104)
//            ]
//            |> List.map (fun (x, y) -> DateTime(x, 12, 31), y)
//            |> Series.Area
//            |> Series.SetName "USA"
//
//        let ussrRussia =
//            [
//                (1950, 5); (1951, 25); (1952, 50); (1953, 120); (1954, 150); (1955, 200);
//                (1956, 426); (1957, 660); (1958, 869); (1959, 1060); (1960, 1605);
//                (1961, 2471); (1962, 3322); (1963, 4238); (1964, 5221); (1965, 6129);
//                (1966, 7089); (1967, 8339); (1968, 9399); (1969, 10538); (1970, 11643);
//                (1971, 13092); (1972, 14478); (1973, 15915); (1974, 17385); (1975, 19055);
//                (1976, 21205); (1977, 23044); (1978, 25393); (1979, 27935); (1980, 30062);
//                (1981, 32049); (1982, 33952); (1983, 35804); (1984, 37431); (1985, 39197);
//                (1986, 45000); (1987, 43000); (1988, 41000); (1989, 39000); (1990, 37000);
//                (1991, 35000); (1992, 33000); (1993, 31000); (1994, 29000); (1995, 27000);
//                (1996, 25000); (1997, 24000); (1998, 23000); (1999, 22000); (2000, 21000);
//                (2001, 20000); (2002, 19000); (2003, 18000); (2004, 18000); (2005, 17000);
//                (2006, 16000)
//            ]
//            |> List.map (fun (x, y) -> DateTime(x, 12, 31), y)
//            |> Series.Area
//            |> Series.SetName "USSR/Russia"
//
//        Highcharts.Area([usa; ussrRussia], legend=true, title="US and USSR nuclear stockpiles")
//
//    // Add a title to the Y-axis.
//    basicArea.SetYTitle "Nuclear warheads"
//
//    // Change the tooltip point format.
//    basicArea.SetPointFormat "{series.name} produced <b>{point.y:,.0f}</b><br/>warheads."

    let john =
        ["Apples", 5; "Oranges", 3; "Pears", 4; "Grapes", 7; "Bananas", 2]
        |> Series.Area
        |> Series.SetName "John"

    let jane =
        ["Apples", 2; "Oranges", -3; "Pears", -2; "Grapes", 2; "Bananas", 1]
        |> Series.Area
        |> Series.SetName "Jane"

    let joe =
        ["Apples", 3; "Oranges", 3; "Pears", 4; "Grapes", -5; "Bananas", -2]
        |> Series.Area
        |> Series.SetName "Joe"

    let negativeValuesArea =
        [john; jane; joe]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Area Chart with Negative Values"

    let asia = Series.Area("Asia", [502; 635; 809; 947; 1402; 3634; 5268])
    let africa = Series.Area("Africa", [106; 107; 111; 133; 221; 767; 1766])
    let europe = Series.Area("Europe", [163; 203; 276; 408; 547; 729; 628])
    let america = Series.Area("America", [18; 31; 54; 156; 339; 818; 1201])
    let oceania = Series.Area("Oceania", [2; 2; 2; 6; 13; 30; 46])

    let stackedArea =
        [asia; africa; europe; america; oceania]
        |> Chart.plot
        |> Chart.categories ["1750"; "1800"; "1850"; "1900"; "1950"; "1999"; "2050"]
        |> Chart.showLegend
        |> Chart.title "Historic and Estimated Worldwide Population Growth"
        |> Chart.tooltip "{series.name} <b>{point.y}</b> millions"
        :?> HighchartsArea
        |> fun x -> x.SetStacking Normal


    let asia = Series.Area("Asia", [502; 635; 809; 947; 1402; 3634; 5268])
    let africa = Series.Area("Africa", [106; 107; 111; 133; 221; 767; 1766])
    let europe = Series.Area("Europe", [163; 203; 276; 408; 547; 729; 628])
    let america = Series.Area("America", [18; 31; 54; 156; 339; 818; 1201])
    let oceania = Series.Area("Oceania", [2; 2; 2; 6; 13; 30; 46])

    let percentArea =
        [asia; africa; europe; america; oceania]
        |> Chart.plot
        |> Chart.categories  ["1750"; "1800"; "1850"; "1900"; "1950"; "1999"; "2050"]
        |> Chart.showLegend
        |> Chart.title "Historic and Estimated Worldwide Population Growth"
        |> Chart.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%</b> ({point.y:,.0f} millions)<br/>"""
        :?> HighchartsArea
        |> fun x -> x.SetStacking Percent

    
//    let missingPointsArea =
//        let jane =
//            [("Apples", box 2); ("Pears", box 3); ("Oranges", box 2); ("Bananas", box null); ("Grapes", box 7);
//            ("Plums", box 4); ("Strawberries", box 2); ("Raspberries", box 4)]
//            |> Series.Area
//            |> Series.SetName "Jane"
//
//        let john =
//            [("Apples", box 0); ("Pears", box 1); ("Oranges", box 4); ("Bananas", box 4); ("Grapes", box 5);
//            ("Plums", box 3); ("Strawberries", box 3); ("Raspberries", box 5)]
//            |> Series.Area
//            |> Series.SetName "John"
//        Highcharts.Area [jane; john]

    let jane =
        ["Monday", 4; "Tuesday", 3; "Wednesday", 5; "Thursday", 4; "Friday", 3; "Saturday", 12; "Sunday", 9]
        |> Series.Area
        |> Series.SetName "Jane"

    let john =
        ["Monday", 3; "Tuesday", 4; "Wednesday", 3; "Thursday", 5; "Friday", 7; "Saturday", 10; "Sunday", 12]
        |> Series.Area
        |> Series.SetName "John"

    let invertedAxesArea =
        [john; jane]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Average Fruit Consumption"
        |> Chart.yTitle "Number of Units"
        :?> HighchartsArea
        |> fun x -> x.SetInverted true

module Areaspline =
    
    let sales =
        ["2010", 1300; "2011", 1470; "2012", 840; "2013", 1330]
        |> Series.Areaspline
        |> Series.SetName "Sales"

    let expenses =
        ["2010", 1000; "2011", 1170; "2012", 580; "2013", 1030]
        |> Series.Areaspline
        |> Series.SetName "Expenses"

    let basicAreaspline =
        [sales; expenses]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Company Performance"

module Arearange =

    let tempratures =
        let rnd = Random()
        [0. .. 6.]
        |> List.map(fun x ->
            DateTime.Now.AddDays x, rnd.Next(-5, -1), rnd.Next(4, 8))
        |> Series.Arearange
        |> Series.SetName "Tempratures"

    let basicArearange =
        Chart.plot tempratures
        |> Chart.title "Temprature Variation"

module Bar =
    
    let basicBar =
        [
            Series.Bar("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
            Series.Bar("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
        ]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Company Performance"

    let joe = Series.Bar("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Bar("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Bar("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

    let stackedBar =
        [joe; jane; john]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.yTitle "Total Fruit Consumption"
        :?> HighchartsBar
        |> fun x -> x.SetStacking Normal

    let joe = Series.Bar("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Bar("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Bar("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

    let percentBar =
        [joe; jane; john]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.yTitle "% of Fruit Consumption"
        |> Chart.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%<br/>"""
        :?> HighchartsBar
        |> fun x -> x.SetStacking Stacking.Percent

module Bubble =
    
    let basicBubble =
        [
            [(97,36,79); (94,74,60); (68,76,58); (64,87,56)]
            [(68,27,73); (74,99,42); (7,93,87); (51,69,40)]
        ]
        |> Highcharts.Bubble

module Column =

    let basicColumn =
        [
            Series.Column("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
            Series.Column("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
        ]
        |> Chart.plot
        |> Chart.title "Company Performance"
        |> Chart.showLegend

    let joe = Series.Column("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Column("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Column("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

    let stackedColumn =
        Chart.plot [joe; jane; john]
        |> Chart.showLegend
        |> Chart.yTitle "Total Fruit Consumption"
        :?> HighchartsColumn
        |> fun x -> x.SetStacking Normal

    let joe = Series.Column("Joe", ["Apples", 3; "Oranges", 5; "Pears", 2; "Bananas", 2])
    let jane = Series.Column("Jane", ["Apples", 2; "Oranges", 3; "Pears", 1; "Bananas", 3])
    let john = Series.Column("John", ["Apples", 1; "Oranges", 3; "Pears", 4; "Bananas", 4])

    let percentColumn =
        Chart.plot [joe; jane; john]
        |> Chart.showLegend
        |> Chart.yTitle "% of Fruit Consumption"
        |> Chart.tooltip """<span style="color:{series.color}">{series.name}</span>: <b>{point.percentage:.1f}%<br/>"""
        :?> HighchartsColumn
        |> fun x -> x.SetStacking Stacking.Percent
    

module Combination =

    let basicComb =
        [
            Series.Column("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
            Series.Line("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
        ]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Company Performance"


    let columnSplinePie =
        [
            Series.Column("Jane", [3; 2; 1; 3; 4])
            Series.Column("John", [2; 3; 5; 7; 6])
            Series.Column("Joe", [4; 3; 3; 9; 0])
            Series.Spline("Average", [3.; 2.67; 3.; 6.33; 3.33])
            Series.Pie("Total Consumption", ["Jane", 13; "John", 23; "Joe", 19])
        ]
        |> Chart.plot
        |> Chart.categories ["Apples"; "Oranges"; "Pears"; "Bananas"; "Plums"]
        :?> HighchartsCombination
        |> fun x -> x.SetPieOptions {Center = [|100; 80|]; Size = 100}

//    let colLinePieComb =
//        let jane = Series.Column("Jane", [3; 2; 1; 3; 4])
//        let john = Series.Column("John", [2; 3; 5; 7; 6])
//        let joe = Series.Column("Joe", [4; 3; 3; 9; 0])
//        let avg = Series.Line("Average", [3.; 2.67; 3.; 6.33; 3.33])
//        let total = Series.Pie("Total", ["Jane", 13; "John", 23; "Joe", 19])
//        Highcharts.Combine [jane; john; joe; avg; total]
//
//    colLinePieComb.Categories <- ["Apples"; "Oranges"; "Pears"; "Bananas"; "Plums"]

module Donut =
    
    let basicDonut =
        ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
        |> Series.Donut
        |> Series.SetName "Browser Share"
        |> Chart.plot
        |> Chart.title "Website Visitors By Browser"
        |> Chart.tooltip """<span style="color:{series.color}>{series.name}</span>: <b>{point.percentage:.1f}%</b><br/>"""
        |> Chart.showLegend

module Funnel =

    let basicFunnel =
        [
            "Website visits", 15654
            "Downloads", 4064
            "Requested price list", 1987
            "Invoice sent", 976
            "Finalized", 846
        ]
        |> Series.Funnel
        |> Series.SetName "Unique users"
        |> Chart.plot
        |> Chart.title "Sales Funnel"

module Line =

    let basicLine =
        [
            Series.Line("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
            Series.Line("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
        ]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Company Performance"

module Pie =
    
    let basicPie =
        Series.Pie ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.]
        |> Series.SetName "Browser Share"
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Website Visitors By Browser"
        |> Chart.tooltip """<span style="color:{series.color}">{series.name}: <b>{point.percentage:.1f}%</b><br/>"""


//        Series.Pie("Browser Share", ["Chrome", 30.4; "Firefox", 26.6; "IE", 18.8; "Safari", 15.2; "Others", 9.])
//        |> Highcharts.Pie
//
//    basicPie.ShowLegend()        
//    basicPie.SetTitle "Website Visitors By Browser"

module Radar =

    let allocated = Series.Radar("Allocated Budget", [43000; 19000; 60000; 35000; 17000; 10000])
    let actual = Series.Radar("Actual Spending", [50000; 39000; 42000; 31000; 26000; 14000])

    let basicRadar =
        Chart.plot [allocated; actual]
        |> Chart.categories ["Sales"; "Marketing"; "Development"; "Customer Support"; "Information Technology"; "Administration"]
        |> Chart.tooltip """<span style="color:{series.color}">{series.name}: <b>${point.y:,.0f}</b><br/>"""
        |> Chart.showLegend
        |> Chart.title "Budget VS Spending"


module Scatter =
    
    let basicScatter =
        [8., 12.; 4., 5.5; 11., 14.; 4., 5.; 3., 3.5; 6.5, 7.]
        |> Series.Scatter
        |> Series.SetName "AgeVsWeight"
        |> Chart.plot
        |> Chart.title "Age vs. Weight comparison"
        |> Chart.xTitle "Age"
        |> Chart.yTitle "Weight"

module Spline =

    let basicSpline =
        [
            Series.Spline("Expenses", ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030])
            Series.Spline("Sales", ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330])
        ]
        |> Chart.plot
        |> Chart.showLegend
        |> Chart.title "Company Performance"
