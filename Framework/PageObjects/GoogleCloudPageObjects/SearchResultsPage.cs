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

        private readonly By resultHeaderTexts = By.XPath("//a[@class ='gs-title']/b");
        private readonly By resultsContainer = By.XPath("//div[@class = 'gsc-resultsbox-visible']");

        public SearchResultsPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public IWebElement FindResultByTitle(string name)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));
            wait.Until(ExpectedConditions.ElementIsVisible(resultsContainer));

            var result = webDriver.FindElements(resultHeaderTexts).First(b => b.Text.Contains(name));

            logger.Info("Result found");

            return result;
        }
    }
}