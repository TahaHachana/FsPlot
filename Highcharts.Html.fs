module internal FsPlot.Highcharts.Html

/// Generates the HTML document for common chart types.
let common js =
    String.concat
        ""
        [
            "<!DOCTYPE html><head><title>Highcharts Chart</title></head><body>"
            """<div id="chart" style="width:100%; height:100%"></div>"""
            """<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>"""
            """<script src="http://code.highcharts.com/highcharts.js"></script>"""
            """<script src="http://code.highcharts.com/modules/exporting.js"></script>"""
            "<script>"
            js
            "</script></body></html>"
        ]

/// Generates the HTML document for the combination chart type.
let combine js =
    String.concat
        ""
        [
            "<!DOCTYPE html><head><title>Highcharts Chart</title></head><body>"
            """<div id="chart" style="width:100%; height:100%"></div>"""
            """<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>"""
            """<script src="http://code.highcharts.com/highcharts.js"></script>"""
            """<script src="http://code.highcharts.com/highcharts-more.js"></script>"""
            """<script src="http://code.highcharts.com/modules/funnel.js"></script>"""
            """<script src="http://code.highcharts.com/modules/exporting.js"></script>"""
            "<script>"
            js
            "</script></body></html>"
        ]

/// Generates the HTML document for the funnel chart type.
let funnel js =
    String.concat
        ""
        [
            "<!DOCTYPE html><head><title>Highcharts Chart</title></head><body>"
            """<div id="chart" style="width:100%; height:100%"></div>"""
            """<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>"""
            """<script src="http://code.highcharts.com/highcharts.js"></script>"""
            """<script src="http://code.highcharts.com/modules/exporting.js"></script>"""
            """<script src="http://code.highcharts.com/modules/funnel.js"></script>"""
            "<script>"
            js
            "</script></body></html>"
        ]

/// Generates the HTML document for the arearange, bubble and radar chart types.
let more js =
    String.concat
        ""
        [
            "<!DOCTYPE html><head><title>Highcharts Chart</title></head><body>"
            """<div id="chart" style="width:100%; height:100%"></div>"""
            """<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>"""
            """<script src="http://code.highcharts.com/highcharts.js"></script>"""
            """<script src="http://code.highcharts.com/highcharts-more.js"></script>"""
            """<script src="http://code.highcharts.com/modules/exporting.js"></script>"""
            "<script>"
            js
            "</script></body></html>"
        ]

let dynamicCommon address js =
    let hubsJs = sprintf """<script src="%s/signalr/hubs" type="text/javascript"></script>""" address
    String.concat
        ""
        [
            "<!DOCTYPE html><head><title>Highcharts Chart</title></head><body>"
            """<div id="chart" style="width:100%; height:100%"></div>"""
            """<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>"""
            """<script src="http://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.0.0.min.js"></script>"""
            hubsJs
            """<script src="http://code.highcharts.com/highcharts.js"></script>"""
            """<script src="http://code.highcharts.com/modules/exporting.js"></script>"""
            "<script>"
            js
            "</script></body></html>"
        ]

let dynamicMore address js =
    let hubsJs = sprintf """<script src="%s/signalr/hubs" type="text/javascript"></script>""" address
    String.concat
        ""
        [
            "<!DOCTYPE html><head><title>Highcharts Chart</title></head><body>"
            """<div id="chart" style="width:100%; height:100%"></div>"""
            """<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>"""
            """<script src="http://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.0.0.min.js"></script>"""
            hubsJs
            """<script src="http://code.highcharts.com/highcharts.js"></script>"""
            """<script src="http://code.highcharts.com/highcharts-more.js"></script>"""
            """<script src="http://code.highcharts.com/modules/exporting.js"></script>"""
            "<script>"
            js
            "</script></body></html>"
        ]
