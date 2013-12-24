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

module Arearange =
    
    let arearange1 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> Series.Arearange
        |> Highcharts.Arearange

    let arearange2 =
        [
            DateTime.Now, -2.5, 5.
            DateTime.Now.AddDays 1., -3., 2.
            DateTime.Now.AddDays 2., 3., 15.
        ]
        |> Highcharts.Arearange

    let arearange3 =
        [
            [
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            ]
            [
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            ]
        ]
        |> Seq.ofList
        |> Highcharts.Arearange

    let arearange4 =
        [
            [|
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            |]
            [|
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            |]
        ]
        |> Seq.ofList
        |> Highcharts.Arearange

    let arearange5 =
        [
            Seq.ofArray [|
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            |]
            Seq.ofArray [|
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            |]
        ]
        |> Seq.ofList
        |> Highcharts.Arearange

    let arearange6 =
        [
            Series.Arearange [|
                DateTime.Now, -2.5, 5.
                DateTime.Now.AddDays 1., -3., 2.
                DateTime.Now.AddDays 2., 3., 15.
            |]
            Series.Arearange [|
                DateTime.Now, -5., 15.
                DateTime.Now.AddDays 3., -3., 2.
                DateTime.Now.AddDays 4., 3., 15.
            |]
        ]
        |> Seq.ofList
        |> Highcharts.Arearange

module Bar =
    
    let bar1 =
        Series.Bar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Bar

    let bar2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Bar

    let bar3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Bar

    let bar4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Bar

    let bar5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Bar

    let bar6 =
        [
            Series.Bar ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Bar ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Bar

    let bar7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Bar

module Bubble =
    
    let bubble1 =
        Series.Bubble [97,36,79; 94,74,60; 68,76,58]
        |> Highcharts.Bubble

    let bubble2 =
        [97,36,79; 94,74,60; 68,76,58]
        |> Highcharts.Bubble

    let bubble3 =
        [
            [36,79; 74,60; 76,58]
        ]
        |> Highcharts.Bubble

    let bubble4 =
        [
            [54,26,59; 64,74,60; 70,26,58]
            [97,36,79; 94,74,60; 68,76,58]
        ]
        |> Highcharts.Bubble

    let bubble5 =
        [
            [26,59; 74,60; 26,58]
            [36,79; 74,60; 76,58]
        ]
        |> Highcharts.Bubble

    let bubble6 =
        [
            [|54,26,59; 64,74,60; 70,26,58|]
            [|97,36,79; 94,74,60; 68,76,58|]
        ]
        |> Highcharts.Bubble

    let bubble7 =
        [
            [|26,59; 74,60; 26,58|]
            [|36,79; 74,60; 76,58|]
        ]
        |> Highcharts.Bubble

    let bubble8 =
        [
            Series.Bubble [|26,59; 74,60; 26,58|]
            Series.Bubble [|36,79; 74,60; 76,58|]
        ]
        |> Highcharts.Bubble

    let bubble9 =
        [
            Seq.ofArray [|54,26,59; 64,74,60; 70,26,58|]
            Seq.ofArray [|97,36,79; 94,74,60; 68,76,58|]
        ]
        |> Highcharts.Bubble

    let bubble10 =
        [
            Seq.ofArray [|26,59; 74,60; 26,58|]
            Seq.ofArray [|36,79; 74,60; 76,58|]
        ]
        |> Highcharts.Bubble

module Column =

    let column1 =
        Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Column

    let column2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Column

    let column3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Column

    let column4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Column

    let column5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Column

    let column6 =
        [
            Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Column ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Column

    let column7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Column

module Combination =

    let comb1 =
        [
            Series.Column [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Series.Line [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|] 
        ]
        |> Highcharts.Combine

module Donut =

    let donut1 =
        Series.Donut ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Donut

    let donut2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Donut

    let donut3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Donut

module Funnel =

    let funnel1 =
        Series.Funnel ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Funnel

    let funnel2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Funnel

    let funnel3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Funnel

module Line =

    let line1 =
        Series.Line ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Line

    let line2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Line

    let line3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Line

    let line4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Line

    let line5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Line

    let line6 =
        [
            Series.Column ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Column ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Line

    let line7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Line

module Pie =

    let pie1 =
        Series.Pie ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Pie

    let pie2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Pie

    let pie3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Pie

module Scatter =

    let scatter1 =
        Series.Scatter ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Scatter

    let scatter2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Scatter

    let scatter3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Scatter

    let scatter4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Scatter

    let scatter5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Scatter

    let scatter6 =
        [
            Series.Scatter ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Scatter ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Scatter

    let scatter7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Scatter

module Spline =

    let spline1 =
        Series.Spline ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Spline

    let spline2 =
        [1000; 1170; 560; 1030]
        |> Highcharts.Spline

    let spline3 =
        ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
        |> Highcharts.Spline

    let spline4 =
        [
            ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Spline

    let spline5 =
        [
            [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Spline

    let spline6 =
        [
            Series.Spline ["2010", 1000; "2011", 1170; "2012", 560; "2013", 1030]
            Series.Spline ["2010", 1300; "2011", 1470; "2012", 740; "2013", 1330]
        ]
        |> Highcharts.Spline

    let spline7 =
        [
            Seq.ofArray [|"2010", 1000; "2011", 1170; "2012", 560; "2013", 1030|]
            Seq.ofArray [|"2010", 1300; "2011", 1470; "2012", 740; "2013", 1330|]
        ]
        |> Highcharts.Spline


