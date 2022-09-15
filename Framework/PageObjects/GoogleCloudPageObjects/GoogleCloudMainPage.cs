using OpenQA.Selenium;

namespace Framework.PageObjects.GoogleCloudPageObjects
{
    public class MainPage : AbstractPage
    {
        private readonly By SearchInputLocator = By.XPath("//input[@placeholder = 'Search']");
        public IWebElement SearchInput => webDriver.FindElement(SearchInputLocator);

        public MainPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public SearchResultsPage Search(string whatToFind)
        {
            SearchInput.Click();
            SearchInput.SendKeys(whatToFind);
            SearchInput.SendKeys(Keys.Enter);

            return new SearchResultsPage(webDriver);
        }
    }
}