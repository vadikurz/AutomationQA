﻿using System;
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
    private readonly By messages = By.XPath("//div[@class = 'mail-MessageSnippet-Content']");
    private readonly By allMessages = By.XPath("//div[contains(@class, 'mail-MessagesList')]");
    private readonly By messageContainer = By.XPath("//div[contains(@class, 'MessageBody')]");
    private readonly By newEmailButton = By.XPath("//a[@href = '#compose']");
    private readonly By recipientInput =
        By.XPath("//div[@class = 'ComposeRecipients-TopRow']//div[@class = 'composeYabbles']");
    private readonly By textInput = By.XPath("//div[contains(@placeholder,'Напишите')]/div");
    private readonly By sendButton = By.XPath("//div[contains(@class,'ComposeSendButton')]/button");
    private readonly By messageDoneScreen = By.XPath("//div[@class = 'ComposeDoneScreen-Wrapper']");
    private readonly By loadMoreMessagesButton = By.XPath("//button[contains(@class, 'message-load-more')]");
    private readonly By searchInput = By.XPath("//input[@placeholder = 'Поиск']");
    private readonly By advancedSearchButton = By.XPath("//button[@title = 'расширенный поиск']");
    private readonly By fromWhomButton = By.XPath("//span[text() = 'От кого']//ancestor::button");
    private readonly By fromWhomInput = By.XPath("//span[contains(@class,'input_theme_websearch')]/input");
    private readonly By searchInfo = By.XPath("//span[@class = 'mail-MessagesSearchInfo-Title']");

    public YandexMailBoxPage(IWebDriver webDriver) : base(webDriver)
    {
    }
    
    private IWebElement FindNewMessageBySenderViaSearch(string sender)
    {
        var actions = new Actions(webDriver);
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        webDriver.FindElement(searchInput).Click();
        actions.MoveToElement(webDriver.FindElement(advancedSearchButton)).Perform();
        actions.Click(webDriver.FindElement(advancedSearchButton)).Perform();

        wait.Until(ExpectedConditions.ElementToBeClickable(fromWhomButton)).Click();
        wait.Until(ExpectedConditions.ElementToBeClickable(fromWhomInput)).Click();
        webDriver.FindElement(fromWhomInput).SendKeys(sender);
        webDriver.FindElement(fromWhomInput).SendKeys(Keys.Enter);
        
        wait.Until(ExpectedConditions.ElementIsVisible(searchInfo));
        
        return WaitForMessageAppearing(messages);
    }

    private string GetMessageText()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        wait.Until(ExpectedConditions.ElementIsVisible(messageContainer));

        return FindTagContainsMessageText();
    }

    public string ReadMessage(string sender)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout)); //30
        wait.Until(ExpectedConditions.ElementIsVisible(allMessages));

        var message = FindNewMessageBySenderViaSearch(sender);

        wait.Until(ExpectedConditions.ElementToBeClickable(message)).Click();

        return GetMessageText();
    }

    public void SendEmail(string recipient, string message)
    {
        EnterNewEmailButton();

        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(recipientInput)).SendKeys(recipient);

        webDriver.FindElement(textInput).SendKeys(message);
        webDriver.FindElement(sendButton).Click();

        wait.Until(ExpectedConditions.ElementIsVisible(messageDoneScreen));
    }

    private void EnterNewEmailButton()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(newEmailButton)).Click();
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
            
            wait.Until(ExpectedConditions.ElementIsVisible(messages));
            
            message = webDriver.FindElements(messages).FirstOrDefault();
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
        var div = GetDivWithoutEmptyString(webDriver.FindElement(messageContainer));
        
        return div.Text == String.Empty ? div.FindElement(By.TagName("span")).Text : div.Text;
    }
}