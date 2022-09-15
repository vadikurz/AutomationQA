using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebdriverTask.Pages.YandexMailPageObjects;

public class YandexMailBoxPage : AbstractPage
{
    private readonly By MessagesLocator = By.XPath("//div[@class = 'mail-MessageSnippet-Content']");
    private readonly By AllMessagesLocator = By.XPath("//div[contains(@class, 'mail-MessagesList')]");
    private readonly By MessageContainerLocator = By.XPath("//div[contains(@class, 'MessageBody')]");
    private readonly By NewEmailButtonLocator = By.XPath("//a[@href = '#compose']");
    private readonly By RecipientInputLocator =
        By.XPath("//div[@class = 'ComposeRecipients-TopRow']//div[@class = 'composeYabbles']");
    private readonly By TextInputLocator = By.XPath("//div[contains(@placeholder,'Напишите')]/div");
    private readonly By SendButtonLocator = By.XPath("//div[contains(@class,'ComposeSendButton')]/button");
    private readonly By MessageDoneScreenLocator = By.XPath("//div[@class = 'ComposeDoneScreen-Wrapper']");
    private readonly By SearchInputLocator = By.XPath("//input[@placeholder = 'Поиск']");
    private readonly By AdvancedSearchButtonLocator = By.XPath("//button[@title = 'расширенный поиск']");
    private readonly By FromWhomButtonLocator = By.XPath("//span[text() = 'От кого']//ancestor::button");
    private readonly By FromWhomInputLocator = By.XPath("//span[contains(@class,'input_theme_websearch')]/input");
    private readonly By SearchInfoLocator = By.XPath("//span[@class = 'mail-MessagesSearchInfo-Title']");
    
    public IWebElement SearchInput => webDriver.FindElement(SearchInputLocator);
    public IWebElement AdvancedSearchButton => webDriver.FindElement(AdvancedSearchButtonLocator);
    public IWebElement FromWhomInput => webDriver.FindElement(FromWhomInputLocator);
    public IWebElement TextInput => webDriver.FindElement(TextInputLocator);
    public IWebElement SendButton => webDriver.FindElement(SendButtonLocator);
    public IWebElement MessageContainer => webDriver.FindElement(MessageContainerLocator);

    public YandexMailBoxPage(IWebDriver webDriver) : base(webDriver)
    {
    }
    
    private IWebElement FindNewMessageBySenderViaSearch(string sender)
    {
        var actions = new Actions(webDriver);
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        SearchInput.Click();
        actions.MoveToElement(AdvancedSearchButton).Perform();
        actions.Click(AdvancedSearchButton).Perform();

        wait.Until(ExpectedConditions.ElementToBeClickable(FromWhomButtonLocator)).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(FromWhomInputLocator)).Click();
        
        FromWhomInput.SendKeys(sender);
        FromWhomInput.SendKeys(Keys.Enter);
        
        wait.Until(ExpectedConditions.ElementIsVisible(SearchInfoLocator));
        
        return WaitForMessageAppearing(MessagesLocator);
    }

    private string GetMessageText()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        wait.Until(ExpectedConditions.ElementIsVisible(MessageContainerLocator));

        return FindTagContainsMessageText();
    }

    public string ReadMessage(string sender)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout)); //30
        wait.Until(ExpectedConditions.ElementIsVisible(AllMessagesLocator));

        var message = FindNewMessageBySenderViaSearch(sender);

        wait.Until(ExpectedConditions.ElementToBeClickable(message)).Click();

        return GetMessageText();
    }

    public void SendEmail(string recipient, string message)
    {
        EnterNewEmailButton();

        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(RecipientInputLocator)).SendKeys(recipient);

        TextInput.SendKeys(message);
        SendButton.Click();

        wait.Until(ExpectedConditions.ElementIsVisible(MessageDoneScreenLocator));
    }

    private void EnterNewEmailButton()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(NewEmailButtonLocator)).Click();
    }
    
    private IWebElement WaitForMessageAppearing(By messageLocator)
    {
        var timeoutBetweenPageRefresh = 5;
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        var message = webDriver.FindElements(messageLocator).FirstOrDefault();
        
        while (message is null || !ContainsClassIsActive(message.FindElements(By.TagName("span"))))
        {
            Thread.Sleep(TimeSpan.FromSeconds(timeoutBetweenPageRefresh));
            
            webDriver.Navigate().Refresh();
            
            wait.Until(ExpectedConditions.ElementIsVisible(MessagesLocator));
            
            message = webDriver.FindElements(messageLocator).FirstOrDefault();
        }

        return message;
    }

    private bool ContainsClassIsActive(ReadOnlyCollection<IWebElement> elements) =>
        elements.Select(span => span.GetAttribute("class"))
            .Any(@class => @class.Contains("is-active"));

    private IWebElement GetDivWithoutEmptyString(IWebElement element)
    {
        return element.FindElements(By.TagName("div"))
            .First(div => !div.Text.Contains("&nbsp"));
    }

    private string FindTagContainsMessageText()
    {
        var div = GetDivWithoutEmptyString(MessageContainer);
        
        return div.Text == String.Empty ? div.FindElement(By.TagName("span")).Text : div.Text;
    }
}