module internal FsPlot.Browser

#if INTERACTIVE
#r """.\packages\Selenium.WebDriver.2.39.0\lib\net40\WebDriver.dll"""
#endif

open OpenQA.Selenium.Chrome
open System.Drawing

let private size = Size(700, 500)

let start() =
    let driver = new ChromeDriver(__SOURCE_DIRECTORY__)
    driver.Manage().Window.Size <- size
    driver