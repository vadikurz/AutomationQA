using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Threading;
using NLog;
using OpenQA.Selenium;

namespace Framework.PageObjects.YopmailPageObjects
{
    public class MailBoxPage : AbstractPage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly By MessagesLocator = By.CssSelector("div.m");
        private readonly By MessageHeadersLocator = By.CssSelector("div.lms");
        private readonly By ListOfMessagesFrameLocator = By.XPath("//iframe[@id='ifinbox']");
        private readonly By MessageFrameLocator = By.XPath("//iframe[@id='ifmail']");
        private readonly By TextViewButtonLocator = By.XPath("//span[text()='Text']/parent::button");
        private readonly By MessageTextLocator = By.XPath("//pre");

        public IWebElement ListOfMessagesFrame => webDriver.FindElement(ListOfMessagesFrameLocator);
        public IWebElement MessageFrame => webDriver.FindElement(MessageFrameLocator);
        public IWebElement TextViewButton => webDriver.FindElement(TextViewButtonLocator);
        public IWebElement MessageText => webDriver.FindElement(MessageTextLocator);
        public ReadOnlyCollection<IWebElement> MessageHeaders => webDriver.FindElements(MessageHeadersLocator);
        public ReadOnlyCollection<IWebElement> Messages => webDriver.FindElements(MessagesLocator);
        

        public MailBoxPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public string ReadMessage(string headlineOfMessage)
        {
            webDriver.SwitchTo().Frame(ListOfMessagesFrame);

            var message = FindMessageByHeadline(headlineOfMessage);
            message.Click();

            webDriver.SwitchTo().DefaultContent();

            webDriver.SwitchTo().Frame(MessageFrame);
            TextViewButton.Click();

            var messageText = MessageText.Text;

            logger.Info("Message read");

            return messageText;
        }

        private IWebElement FindMessageByHeadline(string headlineOfMessage)
        {
            WaitForMessagesAppearing();

            var foundMessage = MessageHeaders
                .First(messageHeader => messageHeader.Text.Contains(headlineOfMessage));

            logger.Info("Message found");

            return foundMessage;
        }

        public double PriceFromMessage(string textFromMessage)
        {
            var regex = new Regex(@"(\d[0-9]*[,]*[0-9]*[.]+[0-9]+)", RegexOptions.Compiled, TimeSpan.FromSeconds(10));
            var matches = regex.Matches(textFromMessage);
            var formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

            var price = double.Parse(matches.First().Value.Replace(",", ""), formatter);

            logger.Info("Price parsed from message");

            return price;
        }

        private ReadOnlyCollection<IWebElement> WaitForMessagesAppearing()
        {
            var timeBetweenRefreshPage = 5;
            var messages = Messages;

            while (messages.Count == 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(timeBetweenRefreshPage));
                
                webDriver.Navigate().Refresh();
                
                webDriver.SwitchTo().Frame(ListOfMessagesFrame);
                
                messages = Messages;
            }

            return messages;
        }
    }
}