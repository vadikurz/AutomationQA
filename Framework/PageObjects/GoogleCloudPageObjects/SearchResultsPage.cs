using System;
using System.Linq;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.PageObjects.GoogleCloudPageObjects
{
    public class SearchResultsPage : AbstractPage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly By ResultHeaderTextsLocator = By.XPath("//a[@class ='gs-title']/b");
        private readonly By ResultsContainerLocator = By.XPath("//div[@class = 'gsc-resultsbox-visible']");

        public SearchResultsPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public IWebElement FindResultByTitle(string name)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));
            wait.Until(ExpectedConditions.ElementIsVisible(ResultsContainerLocator));

            var result = webDriver.FindElements(ResultHeaderTextsLocator).First(b => b.Text.Contains(name));

            logger.Info("Result found");

            return result;
        }
    }
}