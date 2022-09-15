using NUnit.Framework;
using WebdriverTask.Pages.MailRuPageObjects;
using WebdriverTask.Pages.YandexMailPageObjects;

namespace WebdriverTask
{
    [TestFixture]
    public class MailServicesTests : MailServicesBaseTest
    {
        private const string MessageWithName = "Alex Alex";

        [Test]
        public void SignInPositiveTest()
        {
            var mainMenuPage = new MailRuMainMenuPage(webDriver);

            var actualLogin = mainMenuPage
                .ClickSignInButton()
                .SignIn(UserCredentials.MailRuLogin, UserCredentials.Password, out LoginResult result)?.GetUserLogin();

            Assert.AreEqual(UserCredentials.MailRuLogin, actualLogin);
        }
        
        [TestCase("abcabdcabc")]
        [TestCase("abcabdcabc@mail")]
        [TestCase("")]
        [TestCase(" ")]
        public void SignInInvalidLoginNegativeTest(string login)
        {
            new MailRuMainMenuPage(webDriver).ClickSignInButton().SignIn(login, UserCredentials.Password, out LoginResult loginResult);
            
            Assert.AreEqual(LoginResult.InvalidLogin, loginResult);
        }
        
        [TestCase("aaa")]
        [TestCase("")]
        [TestCase(" ")]
        public void SignInInvalidPasswordNegativeTest(string password)
        {
            new MailRuMainMenuPage(webDriver).ClickSignInButton().SignIn(UserCredentials.MailRuLogin, password, out LoginResult loginResult);
            
            Assert.AreEqual(LoginResult.InvalidPassword, loginResult);
        }
        
        [TestCase("Feugiat sociosqu nostra dis massa magna auctor aliquam nullam, " +
                  "porta mauris aliquet iaculis integer ornare netus, suscipit bibendum imperdiet elit ",MessageWithName), Order(0)]
        public void SendingReadingEmailTest(string messageFromFirstServer, string messageFromSecondServer)
        {
            var mailRuMainMenuPage = new MailRuMainMenuPage(webDriver);
            var yandexMainPage = new YandexMailMainPage(webDriver);
            
            mailRuMainMenuPage.ClickSignInButton().SignIn(UserCredentials.MailRuLogin, UserCredentials.Password)?
                .CloseSuggestionToMakeDefaultBrowser()
                .EnterNewEmailButton()
                .SendEmail(UserCredentials.YandexLogin, messageFromFirstServer);
            
            webDriver.Navigate().GoToUrl("https://mail.yandex.by");
            
            var yandexMailBoxPage =
                yandexMainPage.ClickSignInButton().SignIn(UserCredentials.YandexLogin, UserCredentials.Password);
            
            var actualMessage = yandexMailBoxPage.ReadMessage(UserCredentials.MailRuLogin);
            yandexMailBoxPage.SendEmail(UserCredentials.MailRuLogin,messageFromSecondServer);
            
            Assert.AreEqual(messageFromFirstServer, actualMessage);
        }

        [TestCase(MessageWithName), Order(1)]
        public void ChangeUsernameToNewOneFromMessageTest(string newNickName)
        {
            var mailRuMainMenuPage = new MailRuMainMenuPage(webDriver);
            var mailRuMailBoxPage = new MailRuMailBoxPage(webDriver);

            var messageText = mailRuMainMenuPage.ClickSignInButton().SignIn(UserCredentials.MailRuLogin, UserCredentials.Password)?
                .ReadMessage(UserCredentials.YandexLogin);
            mailRuMailBoxPage.RenameUser(messageText);
            
            Assert.AreEqual(MessageWithName,mailRuMailBoxPage.GetUserNickName());
        }
    }
}