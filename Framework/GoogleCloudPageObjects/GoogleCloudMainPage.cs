using OpenQA.Selenium;


namespace Framework.GoogleCloudPageObjects
{
    public class MainPage
    {
        private IWebDriver webDriver;
    
        private readonly By searchInput = By.XPath("//input[@placeholder = 'Search']");
    
        public MainPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }
    
        public SearchResultsPage Search(string whatToFind)
        {
            webDriver.FindElement(searchInput).Click();
            webDriver.FindElement(searchInput).SendKeys(whatToFind);
            webDriver.FindElement(searchInput).SendKeys(Keys.Enter);

            return new SearchResultsPage(webDriver);
        }
    }
}