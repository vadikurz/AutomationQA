using OpenQA.Selenium;

namespace WebdriverTask.Pages.MailRuPageObjects
{
    public class MailRuMainMenuPage : AbstractPage
    {
        private readonly By signInButton = By.XPath("//div[@id = 'mailbox']//button");

        public MailRuMainMenuPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public MailRuAuthorizationPage SignIn()
        {
            webDriver.FindElement(signInButton).Click();

            return new MailRuAuthorizationPage(webDriver);
        }
    }
}