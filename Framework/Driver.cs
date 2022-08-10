using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Framework
{
    public class Driver
    {
        private static Lazy<IWebDriver> lazy = new Lazy<IWebDriver>(() =>
        {
            switch(TestContext.Parameters.Get("Browser"))
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();
                    break;
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();
                    break;
                default: 
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();
            }
        });
    
        public static IWebDriver GetDriver() => lazy.Value;

        public static void CloseDriver()
        {
            GetDriver().Quit();
        }
    }
}