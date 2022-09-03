using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.PageObjects.YopmailPageObjects
{
    public class YopMailMainPage : AbstractPage
    {
        private readonly By emailGeneratorButton = By.XPath("//div[@id='listeliens']//a[@href = 'email-generator']");

        public YopMailMainPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public EmailGeneratorPage ClickButtonGenerateEmail()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            wait.Until(ExpectedConditions.ElementIsVisible(emailGeneratorButton)).Click();

            return new EmailGeneratorPage(webDriver);
        }
    }
}