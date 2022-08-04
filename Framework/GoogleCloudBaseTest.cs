using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Framework;

public class GoogleCloudBaseTest : BaseTest
{
    [SetUp]
    protected void DoBeforeEach()
    {
        webDriver = new ChromeDriver();
        webDriver.Manage().Cookies.DeleteAllCookies();
        webDriver.Navigate().GoToUrl("https://cloud.google.com/");
        webDriver.Manage().Window.Maximize();
    }
}