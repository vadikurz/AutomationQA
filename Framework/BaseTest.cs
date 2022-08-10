using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework
{
    public abstract class BaseTest
    {
        protected IWebDriver webDriver;
    
        [TearDown]
        protected void DoAfterEach()
        {
            Driver.CloseDriver();
        }
    }
}