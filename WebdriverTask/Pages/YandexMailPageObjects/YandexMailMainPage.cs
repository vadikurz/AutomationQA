using OpenQA.Selenium;

namespace WebdriverTask.Pages.YandexMailPageObjects;

public class YandexMailMainPage : AbstractPage
{
    private readonly By SignInButtonLocator = By.XPath("//button[contains(@class, NoLoginButton)]");

    public IWebElement SignInButton =>  webDriver.FindElement(SignInButtonLocator);
    public YandexMailMainPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public YandexAuthorizationPage ClickSignInButton()
    {
        SignInButton.Click();

        return new YandexAuthorizationPage(webDriver);
    }
}