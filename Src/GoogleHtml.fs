module FsPlot.Google.Html

/// Generates the HTML document for common chart types.
let common js =
    String.concat
        ""
        [
            "<!DOCTYPE html><html><head><title>Google Chart</title></head><body>"
            """<div id="chart" style="width: 900px; height: 500px;"></div>"""
            """<script type="text/javascript" src="https://www.google.com/jsapi"></script>"""
            "<script>"
            js
            "</script></body></html>"
        ]
