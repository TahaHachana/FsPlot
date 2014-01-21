module internal FsPlot.ChartWindow
//module ChartWindow =

#if INTERACTIVE
#r "PresentationCore.dll"
#r "PresentationFramework.dll"
#r "WindowsBase.dll"
#endif

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media.Imaging

/// Creates the bitmap frame used to set the chart's window icon.
let private bitmapFrame =
    let uriString = @"pack://application:,,,/FsPlot;component/ChartIcon.ico"
    let iconUri = Uri(uriString, UriKind.RelativeOrAbsolute)
    BitmapFrame.Create(iconUri)

/// Displays a window containing a browser control.
let show html =
    let wnd = Window(Height = 500., Topmost = true, Width = 700.)
    wnd.Icon <- bitmapFrame
    wnd.WindowStartupLocation <- WindowStartupLocation.CenterScreen 
    let browser = new WebBrowser()
    wnd.Content <- browser
    wnd.Show()
    wnd.Topmost <- false
    wnd, browser