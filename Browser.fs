module FsPlot.Browser

#if INTERACTIVE
#r """.\packages\Selenium.WebDriver.2.39.0\lib\net40\WebDriver.dll"""
#endif

open OpenQA.Selenium.Chrome
open OpenQA.Selenium.Firefox

let size = System.Drawing.Size(700, 500)

let start() =
    let driver = new ChromeDriver(__SOURCE_DIRECTORY__)
    let window = driver.Manage().Window
    window.Size <- size
    driver