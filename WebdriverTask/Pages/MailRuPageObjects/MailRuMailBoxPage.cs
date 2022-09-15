using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebdriverTask.Pages.MailRuPageObjects;

public class MailRuMailBoxPage : AbstractPage
{
    private readonly By SideBarButtonLocator = By.XPath("//div[@data-testid = 'whiteline-account']");
    private readonly By NewEmailButtonLocator = By.XPath("//span[@class = 'compose-button__wrapper']");
    private readonly By MessagesAfterFilteringLocator = By.XPath("//div/a[contains(@href,'/search/inbox/')]");
    private readonly By SearchButtonLocator = By.XPath("//div[@class = 'search-panel__right-col']");
    private readonly By SearchInputLocator = By.XPath("//input[contains(@class,'mail-operands')]");
    private readonly By FindButtonLocator = By.XPath("//span[text() = 'Найти']");
    private readonly By MessageTextlocator = By.XPath("//div[contains(@class,'js-readmsg')]//div[@class]/div");
    private readonly By PersonalDataButtonLocator = By.XPath("//div[text()='Личные данные']");
    private readonly By NickNameInputLocator = By.XPath("//input[@id = 'nickname']");
    private readonly By SaveButtonLocator = By.XPath("//button[@data-test-id = 'save-button']");
    private readonly By SearchInSpamAndTrashFoldersButtonLocator = By.XPath("//div[@class='list-letter-preview-action']");
    private readonly By CloseButtonForSuggestionToMakeDefaultBrowserLocator = By.CssSelector("div.ph-project-promo-close-icon");
    
    public IWebElement FindButton =>webDriver.FindElement(FindButtonLocator);
    public IWebElement SideBarButton => webDriver.FindElement(SideBarButtonLocator);
    public IWebElement SaveButton => webDriver.FindElement(SaveButtonLocator);
    public IWebElement NickNameInput => webDriver.FindElement(NickNameInputLocator);
    
   
    public MailRuMailBoxPage(IWebDriver webDriver) : base(webDriver)
    {
    }

    public string GetUserLogin()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        return wait.Until(ExpectedConditions.ElementIsVisible(SideBarButtonLocator)).Text;
    }

    public MailRuNewEmailPage EnterNewEmailButton()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));

        wait.Until(ExpectedConditions.ElementIsVisible(NewEmailButtonLocator)).Click();

        return new MailRuNewEmailPage(webDriver);
    }

    private IWebElement FindNewMessageBySender(string sender)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        wait.Until(ExpectedConditions.ElementToBeClickable(SearchButtonLocator)).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(SearchInputLocator)).SendKeys(sender);
        
        FindButton.Click();
        
        wait.Until(ExpectedConditions.ElementIsVisible(SearchInSpamAndTrashFoldersButtonLocator));
        wait.Until(ExpectedConditions.ElementIsVisible(MessagesAfterFilteringLocator));

        return WaitForMessageAppearing(MessagesAfterFilteringLocator);
    }

    public string ReadMessage(string sender)
    { 
        FindNewMessageBySender(sender).Click();
        
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        var messageText = wait.Until(ExpectedConditions.ElementIsVisible(MessageTextlocator)).Text;

        return messageText;
    }

    public void RenameUser(string newUserFirstName)
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        SideBarButton.Click();
        
        wait.Until(ExpectedConditions.ElementIsVisible(PersonalDataButtonLocator)).Click();

        var input = wait.Until(ExpectedConditions.ElementIsVisible(NickNameInputLocator));
        input.Clear();
        input.SendKeys(newUserFirstName);
        
        SaveButton.Click();
    }

    public string GetUserNickName() // убрать
    {
        return NickNameInput.GetAttribute("value");
    }

    public MailRuMailBoxPage CloseSuggestionToMakeDefaultBrowser()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        wait.Until(ExpectedConditions.ElementIsVisible(CloseButtonForSuggestionToMakeDefaultBrowserLocator)).Click();
        
        return this;
    }
    
    private IWebElement WaitForMessageAppearing(By messagesLocator)
    {
        var timeBetweenPageRefresh = 5;
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitingTimeout));
        
        var message = webDriver.FindElements(messagesLocator).FirstOrDefault();
        
        while (message is null || IsMessageRead(message.FindElements(By.TagName("span"))))
        {
            Thread.Sleep(TimeSpan.FromSeconds(timeBetweenPageRefresh));
            
            webDriver.Navigate().Refresh();
            
            wait.Until(ExpectedConditions.ElementIsVisible(MessagesAfterFilteringLocator));
            
            message = webDriver.FindElements(messagesLocator).FirstOrDefault();
        }

        return message;
    }

    private bool IsMessageRead(ReadOnlyCollection<IWebElement> elements) =>
        elements.Select(span => span.GetAttribute("title"))
            .Any(title => title.Contains("непрочитанным"));
}