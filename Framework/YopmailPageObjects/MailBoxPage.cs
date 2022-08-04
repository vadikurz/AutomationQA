using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;

namespace Framework.YopMailPageObjects;

public class MailBoxPage
{
    private IWebDriver webDriver;

    private readonly By messagesLocator = By.CssSelector("div.m");
    private readonly By messageHeaders = By.CssSelector("div.lms");
    private readonly By listOfMessagesFrame = By.XPath("//iframe[@id='ifinbox']");
    private readonly By messageFrame = By.XPath("//iframe[@id='ifmail']");
    private readonly By textViewButton = By.XPath("//span[text()='Text']/parent::button");
    private readonly By messageText = By.XPath("//pre");

    public MailBoxPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    public string ReadMessage(string headlineOfMessage)
    {
        webDriver.SwitchTo().Frame(webDriver.FindElement(listOfMessagesFrame));

        var message = FindMessageByHeadline(headlineOfMessage);
        message.Click();

        webDriver.SwitchTo().DefaultContent();

        webDriver.SwitchTo().Frame(webDriver.FindElement(messageFrame));
        webDriver.FindElement(textViewButton).Click();

        return webDriver.FindElement(messageText).Text;
    }

    private IWebElement FindMessageByHeadline(string headlineOfMessage)
    {
        WaitForMessagesAppearing();

        return webDriver.FindElements(messageHeaders)
            .First(messageHeader => messageHeader.Text.Contains(headlineOfMessage));
    }

    public double PriceFromMessage(string textFromMessage)
    {
        var regex = new Regex(@"(\d[0-9]*[,]*[0-9]*[.]+[0-9]+)",RegexOptions.Compiled,TimeSpan.FromSeconds(10));
        var matches = regex.Matches(textFromMessage);
        var formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
        
        return double.Parse(matches.First().Value.Replace(",", ""), formatter);
    }

    private ReadOnlyCollection<IWebElement> WaitForMessagesAppearing()
    {
        var messages = webDriver.FindElements(messagesLocator);
        
        while (messages.Count == 0)
        {
            Thread.Sleep(5000);
            webDriver.Navigate().Refresh();
            webDriver.SwitchTo().Frame(webDriver.FindElement(listOfMessagesFrame));
            messages = webDriver.FindElements(messagesLocator);
        }

        return messages;
    }
}