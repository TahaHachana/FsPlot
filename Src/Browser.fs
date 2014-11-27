module internal FsPlot.Browser

#if INTERACTIVE
#r """.\packages\Selenium.WebDriver.2.44.0\lib\net40\WebDriver.dll"""
#endif

open OpenQA.Selenium.Chrome
open System.Drawing
open Settings

let start url =
    let driver = new ChromeDriver(FSPlotSettings.chromeDriverDirectory)
    driver.Manage().Window.Size <- Size(1000, 700)
    driver.Url <- url
    driver