using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebdriverTask.Pages.MailRuPageObjects;

public class MailRuAuthorizationPage : AbstractPage
{
    private readonly By loginInput = By.XPath("//input[@name='username']");
    private readonly By continueButon = By.XPath("//button[@data-test-id = 'next-button']");
    private readonly By passwordInput = By.XPath("//input[@name = 'password']");
    private readonly By submitButton = By.XPath("//button[@data-test-id = 'submit-button']");
    private readonly By loginFormFrame = By.XPath("//iframe[contains(@src, 'mail.ru/login')]");
    private readonly By invalidCredentialsMessageElement = By.XPath("//div[@data-test-id = 'error-footer-text']");

    public MailRuAuthorizationPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public MailRuMailBoxPage? SignIn(string login, string password, out LoginResult loginResult)
    {
        if (EnterLogin(login) is not { })
        {
            loginResult = LoginResult.InvalidLogin;
            return null;
        }

        if (EnterPassword(password) is not { } mailBoxPage)
        {
            loginResult = LoginResult.InvalidPassword;
            return null;
        }

        loginResult = LoginResult.Success;

        return mailBoxPage;
    }

    public MailRuMailBoxPage? SignIn(string login, string password)
    {
        return SignIn(login, password, out LoginResult result);
    }

    private MailRuAuthorizationPage? EnterLogin(string login)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        webDriver.SwitchTo().Frame(webDriver.FindElement(loginFormFrame));

        wait.Until(ExpectedConditions.ElementIsVisible(loginInput)).SendKeys(login);

        webDriver.FindElement(continueButon).Click();

        return CheckPresenceInvalidCredentialsMessage() ? null : this;
    }

    private MailRuMailBoxPage? EnterPassword(string password)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(passwordInput)).SendKeys(password);

        webDriver.FindElement(submitButton).Click();

        return CheckPresenceInvalidCredentialsMessage() ? null : new MailRuMailBoxPage(webDriver);
    }

    private bool CheckPresenceInvalidCredentialsMessage()
    {
        var messageWaitingTime = 2;
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(messageWaitingTime));

        try
        {
            wait.Until(ExpectedConditions.ElementIsVisible(invalidCredentialsMessageElement));

            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
}