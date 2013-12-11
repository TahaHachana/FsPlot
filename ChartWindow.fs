module FsPlot.ChartWindow

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media.Imaging

let private bitmapFrame =
    let uriString = @"pack://application:,,,/FsPlot;component/ChartIcon.ico"
    let iconUri = Uri(uriString, UriKind.RelativeOrAbsolute)
    BitmapFrame.Create(iconUri)

let show html =
    let wnd = Window()
    wnd.Icon <- bitmapFrame
    wnd.Width <- 700.
    wnd.Height <- 500.
    wnd.WindowStartupLocation <- WindowStartupLocation.CenterScreen 
    wnd.Topmost <- true
    let browser = new WebBrowser()
    wnd.Content <- browser
    wnd.Show()
    wnd, browser