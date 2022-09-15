using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebdriverTask.Pages.YandexMailPageObjects;

public class YandexAuthorizationPage : AbstractPage
{
    private readonly By LoginInputLocator = By.XPath("//input[@id = 'passp-field-login']");
    private readonly By PasswordInputLocator = By.XPath("//input[@id = 'passp-field-passwd']");
    private readonly By SignInSubmitButtonLocator = By.XPath("//button[@id = 'passp:sign-in']");
    
    public IWebElement LoginInput => webDriver.FindElement(LoginInputLocator);
    public IWebElement SignInSubmitButton => webDriver.FindElement(SignInSubmitButtonLocator);

    public YandexAuthorizationPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public YandexMailBoxPage SignIn(string login, string password)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        LoginInput.SendKeys(login);
        SignInSubmitButton.Click();

        wait.Until(ExpectedConditions.ElementIsVisible(PasswordInputLocator)).SendKeys(password);

        SignInSubmitButton.Click();

        return new YandexMailBoxPage(webDriver);
    }
}