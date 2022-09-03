using System;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.PageObjects.YopmailPageObjects
{
    public class EmailGeneratorPage : AbstractPage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly By generatedEmailAddressLocator = By.XPath("//div[@id='egen']");
        private readonly By checkMailButton = By.XPath("//span[text()='Проверить почту']//parent::button");

        public EmailGeneratorPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public string CopyGeneratedEmailAddress()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            var generatedEmailAddress =
                wait.Until(ExpectedConditions.ElementIsVisible(generatedEmailAddressLocator)).Text;

            logger.Info("Email address copied");

            return generatedEmailAddress;
        }

        public MailBoxPage ClickButtonCheckMail()
        {
            webDriver.FindElement(checkMailButton).Click();

            return new MailBoxPage(webDriver);
        }
    }
}