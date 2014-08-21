module internal FsPlot.Browser

#if INTERACTIVE
#r """.\packages\Selenium.WebDriver.2.39.0\lib\net40\WebDriver.dll"""
#endif

open OpenQA.Selenium.Chrome
open System.Drawing

let start url =
    let options = ChromeOptions()
    options.AddArgument("test-type")
    let driver = new ChromeDriver(options)
    driver.Manage().Window.Size <- Size(1000, 700)
    driver.Url <- url
    driver