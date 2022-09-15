using OpenQA.Selenium;

namespace WebdriverTask.Pages;

public class AbstractPage
{
    protected IWebDriver webDriver;

    protected const int WaitingTimeout = 10;

    protected AbstractPage(IWebDriver webDriver)
    {
        this.webDriver = webDriver;
    }
}