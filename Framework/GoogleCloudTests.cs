using Framework.Extensions;
using Framework.GoogleCloudPageObjects;
using Framework.YopMailPageObjects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Framework;

public class GoogleCloudTests : GoogleCloudBaseTest
{
    private const string whatToFind = "Google Cloud Platform Pricing Calculator";
    
    [Test]
    public void PricingCalculatorTest()
    {
        var mainPage = new MainPage(webDriver);
        var pricingCalculatorPage = new PricingCalculatorPage(webDriver);
        var yopMailMainPage = new YopMailMainPage(webDriver);
        
        mainPage.Search(whatToFind).FindResultByTitle(whatToFind).Click();

        var priceOnPricingCalculatorPage = pricingCalculatorPage
            .SelectComputerEngineSection()
            .FillInTheForm()
            .ClickAddToEstimateButton()
            .ClickEmailEstimateButton()
            .TotalPriceFromPage();
        
        webDriver.SwitchTo().NewWindow(WindowType.Tab);
        webDriver.Navigate().GoToUrl("https://yopmail.com/");
        
        var emailGeneratorPage = yopMailMainPage.ClickButtonGenerateEmail();
        var generatedEmailAddress = emailGeneratorPage.CopyGeneratedEmailAddress();
        
        webDriver.SwitchToPreviousTab();
        
        pricingCalculatorPage
            .SetEmailAddressInput(generatedEmailAddress)
            .ClickButtonSendEmail();
        
        webDriver.SwitchToNextTab();
        
        var mailBoxPage = emailGeneratorPage.ClickButtonCheckMail();
        var priceFromMessage = mailBoxPage.PriceFromMessage(mailBoxPage.ReadMessage("Google Cloud Price Estimate"));

        Assert.AreEqual(priceOnPricingCalculatorPage,priceFromMessage);
    }
}