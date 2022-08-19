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
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                switch(TestContext.Parameters.Get("Browser"))
                {
                    case "chrome":
                        new DriverManager().SetUpDriver(new ChromeConfig());
                        driver =  new ChromeDriver();
                        break;
                    case "firefox":
                        new DriverManager().SetUpDriver(new FirefoxConfig());
                        driver =  new FirefoxDriver();
                        break;
                    default: 
                        new DriverManager().SetUpDriver(new FirefoxConfig());
                        driver =  new FirefoxDriver();
                        break;
                }
            }

            return driver;
        }

        public static void CloseDriver()
        {
            GetDriver().Quit();
            driver = null;
        }
    }
}