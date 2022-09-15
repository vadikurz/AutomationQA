using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebdriverTask.Pages.YandexMailPageObjects;

public class YandexAuthorizationPage : AbstractPage
{
    private readonly By loginInput = By.XPath("//input[@id = 'passp-field-login']");
    private readonly By passwordInput = By.XPath("//input[@id = 'passp-field-passwd']");
    private readonly By signInSubmitButton = By.XPath("//button[@id = 'passp:sign-in']");
    
    public YandexAuthorizationPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public YandexMailBoxPage SignIn(string login, string password)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        webDriver.FindElement(loginInput).SendKeys(login);
        webDriver.FindElement(signInSubmitButton).Click();

        wait.Until(ExpectedConditions.ElementIsVisible(passwordInput)).SendKeys(password);

        webDriver.FindElement(signInSubmitButton).Click();

        return new YandexMailBoxPage(webDriver);
    }
}