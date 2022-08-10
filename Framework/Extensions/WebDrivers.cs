using OpenQA.Selenium;

namespace Framework.Extensions
{
    public static class WebDrivers
    {
        public static void SwitchToPreviousTab(this IWebDriver driver)
        {
            var currentWindowHandleIndex = driver.CurrentWindowHandleIndex();
            var windowHandles = driver.WindowHandles;

            if (currentWindowHandleIndex is not 0)
            {
                driver.SwitchTo().Window(windowHandles[currentWindowHandleIndex - 1]);
            }
        }

        public static void SwitchToNextTab(this IWebDriver driver)
        {
            var currentWindowHandleIndex = driver.CurrentWindowHandleIndex();
            var windowHandles = driver.WindowHandles;
            if (currentWindowHandleIndex != windowHandles.Count - 1)
            {
                driver.SwitchTo().Window(windowHandles[currentWindowHandleIndex + 1]);
            }
        }

        private static int CurrentWindowHandleIndex(this IWebDriver driver)
        {
            var tabs = driver.WindowHandles;
        
            return tabs.IndexOf(driver.CurrentWindowHandle);
        }
    }
}