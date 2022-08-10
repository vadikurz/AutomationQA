using NUnit.Framework;

namespace Framework
{
    public class GoogleCloudBaseTest : BaseTest
    {
        [SetUp]
        protected void DoBeforeEach()
        {
            webDriver = Driver.GetDriver();
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Navigate().GoToUrl("https://cloud.google.com/");
            webDriver.Manage().Window.Maximize();
        }
    }
}