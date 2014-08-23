module internal FsPlot.Browser

#if INTERACTIVE
#r """.\packages\Selenium.WebDriver.2.42.0\lib\net40\WebDriver.dll"""
#endif

open OpenQA.Selenium.Firefox
open System.Drawing

let start url =
    let driver = new FirefoxDriver()
    driver.Manage().Window.Size <- Size(1000, 700)
    driver.Url <- url
    driver