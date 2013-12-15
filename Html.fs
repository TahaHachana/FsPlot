module internal FsPlot.Html

let highcharts js =
    String.concat
        ""
        [
            "<!DOCTYPE html>"
            "<head><title>Highcharts Example</title></head>"
            "<body>"
            "<div id='chart' style='width:100%; height:100%'></div>"
            "<script src='http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js'></script>"
            "<script src='http://code.highcharts.com/highcharts.js'></script>"
            "<script src='http://code.highcharts.com/highcharts-more.js'></script>"
            "<script src='http://code.highcharts.com/modules/exporting.js'></script>"
            "<script>"
            js
            "</script>"
            "</body>"
        ]
