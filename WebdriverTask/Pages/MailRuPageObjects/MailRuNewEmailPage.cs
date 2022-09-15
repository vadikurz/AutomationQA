using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebdriverTask.Pages.MailRuPageObjects;

public class MailRuNewEmailPage : AbstractPage
{
    private readonly By TextInputLocator = By.XPath("//div[contains(@class, 'editable-container')]//br");
    private readonly By RecipientInputLocator = By.XPath("//div[contains(@class, 'contactsContainer')]//input");
    private readonly By SendButtonLocator = By.XPath("//button[@data-test-id = 'send']");
    private readonly By CloseUndoWindowButtonLocator = By.XPath("//span[@title = 'Закрыть']");
    
    public IWebElement TextInput => webDriver.FindElement(TextInputLocator);
    public IWebElement SendButton => webDriver.FindElement(SendButtonLocator);

    public MailRuNewEmailPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public MailRuMailBoxPage SendEmail(string recipient, string text)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(RecipientInputLocator)).SendKeys(recipient);
        
        TextInput.SendKeys(text);
        SendButton.Click();
        
        CloseUndoSendWindow();

        return new MailRuMailBoxPage(webDriver);
    }

    private void CloseUndoSendWindow()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(CloseUndoWindowButtonLocator)).Click();
    }
}