using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.YopMailPageObjects
{
    public class YopMailMainPage
    {
        private IWebDriver webDriver;

        private readonly By emailGeneratorButton = By.XPath("//div[@id='listeliens']//a[@href = 'email-generator']");

        public YopMailMainPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public EmailGeneratorPage ClickButtonGenerateEmail()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(emailGeneratorButton)).Click();
        
            return new EmailGeneratorPage(webDriver);
        }
    }
}