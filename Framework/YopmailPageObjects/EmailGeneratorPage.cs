using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.YopMailPageObjects;

public class EmailGeneratorPage
{
    private IWebDriver webDriver;

    private readonly By generatedEmailAddress = By.XPath("//div[@id='egen']");
    private readonly By checkMailButton = By.XPath("//span[text()='Проверить почту']//parent::button");

    public EmailGeneratorPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }

    public string CopyGeneratedEmailAddress()
    {
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

        return wait.Until(ExpectedConditions.ElementIsVisible(generatedEmailAddress)).Text;
    }

    public MailBoxPage ClickButtonCheckMail()
    {
        webDriver.FindElement(checkMailButton).Click();

        return new MailBoxPage(webDriver);
    }
}