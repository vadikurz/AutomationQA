using OpenQA.Selenium;

namespace WebdriverTask.Pages.MailRuPageObjects
{
    public class MailRuMainMenuPage : AbstractPage
    {
        private readonly By SignInButtonLocator = By.XPath("//div[@id = 'mailbox']//button");
        
        public IWebElement SignInButton => webDriver.FindElement(SignInButtonLocator);

        public MailRuMainMenuPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public MailRuAuthorizationPage ClickSignInButton()
        {
            SignInButton.Click();

            return new MailRuAuthorizationPage(webDriver);
        }
    }
}