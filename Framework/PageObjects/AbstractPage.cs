using OpenQA.Selenium;

namespace Framework.PageObjects
{
    public abstract class AbstractPage
    {
        protected IWebDriver webDriver;
        protected const int WaitTimeOut = 10;

        protected AbstractPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
    }
}