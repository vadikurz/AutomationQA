using System;
using System.IO;
using Framework.Services;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Framework
{
    public class GoogleCloudBaseTest 
    {
        protected IWebDriver webDriver;
        
        [SetUp]
        protected void DoBeforeEach()
        {
            webDriver = Driver.GetDriver();
            TestConfigurationReader.TestConfiguration();
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Navigate().GoToUrl("https://cloud.google.com/");
            webDriver.Manage().Window.Maximize();
        }
        
        [TearDown]
        protected void DoAfterEach()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
                if (!Directory.Exists(".//screenshots/"))
                {
                    Directory.CreateDirectory(".//screenshots/");
                }
                
                screenshot.SaveAsFile(".//screenshots/" + $"{DateTime.Now:dd-MM-yyyy_HH-mm-ss.fffff}.jpg",ScreenshotImageFormat.Jpeg);
            }

            Driver.CloseDriver();
        }
    }
}