module internal FsPlot.KendoJs

do ()
//
//#if INTERACTIVE
//#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.dll"""
//#r """.\packages\FunScript.1.1.0.28\lib\net40\FunScript.Interop.dll"""
//#r """.\packages\FunScript.TypeScript.Binding.lib.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.lib.dll"""
//#r """.\packages\FunScript.TypeScript.Binding.jquery.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.jquery.dll"""
//#r """.\packages\FunScript.TypeScript.Binding.highcharts.1.1.0.13\lib\net40\FunScript.TypeScript.Binding.highcharts.dll"""
//#endif
//
//open System
//open FunScript
//open DataSeries
//open Options
//open Quote
//
//[<ReflectedDefinition>]
//module Utils =
//
//    let jq(selector:string) = Globals.Dollar.Invoke selector
//
//
//module Inline = do ()
//
////    [<JSEmitInline("{0}.center = {1}")>]
////    let pieCenter options arr : unit = failwith "never"
//
//
//open Utils
//open Inline
//
//[<ReflectedDefinition>]
//module Chart =
//
//    let area (series:Series []) chartTitle legend categories xTitle yTitle pointFormat subtitle stacking inverted =
//
//
//let area a b c d e f g h i j =
//    let e1, e2, e3, e4, e5, e6, e7, e8 = quoteArgs a b c d e f g h
//    let e9 = quoteStacking i
//    let e10 = quoteBool j
//    Compiler.Compiler.Compile(
//        <@ Chart.area %%e1 %%e2 %%e3 %%e4 %%e5 %%e6 %%e7 %%e8 %%e9 %%e10 @>,
//        noReturn=true,
//        shouldCompress=true)
