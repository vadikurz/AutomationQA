using OpenQA.Selenium;

namespace WebdriverTask.Pages.YandexMailPageObjects;

public class YandexMailMainPage : AbstractPage
{
    private readonly By signInButton = By.XPath("//button[contains(@class, NoLoginButton)]");

    public YandexMailMainPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public YandexAuthorizationPage SignIn()
    {
        webDriver.FindElement(signInButton).Click();

        return new YandexAuthorizationPage(webDriver);
    }
}