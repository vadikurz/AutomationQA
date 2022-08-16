using Framework.Services;
using NUnit.Framework;

namespace Framework
{
    public class GoogleCloudBaseTest : BaseTest
    {
        [SetUp]
        protected void DoBeforeEach()
        {
            webDriver = Driver.GetDriver();
            TestConfigurationReader.TestConfiguration();
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Navigate().GoToUrl("https://cloud.google.com/");
            webDriver.Manage().Window.Maximize();
        }
    }
}