using OpenQA.Selenium;

namespace WebdriverTask.YandexMailPageObjects;

public class YandexMailMainPage
{
    private IWebDriver webDriver;
    
    private readonly By signInButton = By.XPath("//button[contains(@class, NoLoginButton)]");

    public YandexMailMainPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    public YandexAuthorizationPage SignIn()
    {
        webDriver.FindElement(signInButton).Click();

        return new YandexAuthorizationPage(webDriver);
    }
}