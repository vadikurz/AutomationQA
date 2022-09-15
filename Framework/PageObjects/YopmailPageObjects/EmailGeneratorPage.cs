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

        private readonly By GeneratedEmailAddressLocator = By.XPath("//div[@id='egen']");
        private readonly By CheckMailButtonLocator = By.XPath("//span[text()='Проверить почту']//parent::button");

        public IWebElement CheckMailButton => webDriver.FindElement(CheckMailButtonLocator);

        public EmailGeneratorPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public string CopyGeneratedEmailAddress()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));
            
            var generatedEmailAddress =
                wait.Until(ExpectedConditions.ElementIsVisible(GeneratedEmailAddressLocator)).Text;

            logger.Info("Email address copied");

            return generatedEmailAddress;
        }

        public MailBoxPage ClickButtonCheckMail()
        {
            CheckMailButton.Click();

            return new MailBoxPage(webDriver);
        }
    }
}