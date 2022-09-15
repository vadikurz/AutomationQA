using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Framework.Models;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.PageObjects.GoogleCloudPageObjects
{
    public class PricingCalculatorPage : AbstractPage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly By ComputeEngineSectionLocator = By.XPath("//div[@class ='tab-holder compute']/parent::md-tab-item");
        private readonly By SectionsContainerLocator = By.XPath("//md-pagination-wrapper");
        private readonly By OuterPricingCalculatorFrameLocator = By.XPath("//iframe[contains(@src, 'products/calculator')]");
        private readonly By SeriesOfMachineInputLocator = By.XPath("//md-select[@placeholder = 'Series']");
        private readonly By OptionN1ForMachineSeriesLocator = By.XPath("//md-option[@value='n1']");
        private readonly By MachineTypeInputLocator = By.XPath("//md-select[@placeholder = 'Instance type']");
        private readonly By GPUTypeInputLocator = By.XPath("//md-select[@placeholder='GPU type']");
        private readonly By OptionNVIDIA_TESLA_V100ForGPUTypeLocator = By.XPath("//md-option[@value ='NVIDIA_TESLA_V100']");
        private readonly By NumberOfGpusInputLocator = By.XPath("//md-select[@placeholder = 'Number of GPUs']");
        private readonly By LocalSsdInputLocator = By.XPath("//md-select[@placeholder = 'Local SSD']");
        private readonly By EmailEstimateButtonLocator = By.XPath("//button[@title = 'Email Estimate']");
        private readonly By EmailInputInEmailFormLocator = By.XPath("//form[@name='emailForm']//input[@type='email']");
        private readonly By SendEmailButtonLocator = By.XPath("//button[@aria-label='Send Email']");
        private readonly By UserFirstNameInputLocator = By.XPath("//input[contains(@ng-model,'user.firstname')]");
        private readonly By UserLastNameInputLocator = By.XPath("//input[contains(@ng-model,'user.lastname')]");

        private readonly By InsidePricingCalculatorFrameLocator = By.XPath
        (
            xpathToFind: "//iframe[contains(@src, 'cloudpricingcalculator')]"
        );

        private readonly By NumberOfInstancesInputLocator = By.XPath
        (
            xpathToFind: "//label[contains(text(),'Number of instances')]/following-sibling::input"
        );

        private readonly By OptionN1_Standard_8ForMachineTypeLocator = By.XPath
        (
            xpathToFind: "//md-option[@value='CP-COMPUTEENGINE-VMIMAGE-N1-STANDARD-8']"
        );

        private readonly By AddGPUsCheckboxLocator = By.XPath
        (
            xpathToFind: "//md-checkbox[@aria-label='Add GPUs' and contains(@ng-model,'computeServer')]"
        );

        private readonly By Option1ForNumberOfGPUsLocator = By.XPath
        (
            xpathToFind: "//md-option[contains(@ng-repeat,'GpuNumbers') and @value='1']"
        );

        private readonly By Option2x375ForLocalSSDLocator = By.XPath
        (
            xpathToFind: "//md-option[contains(@ng-repeat,'dynamicSsd') and @value='2']"
        );

        private readonly By DataCenterLocationInputLocator = By.XPath
        (
            xpathToFind: "//md-select[@placeholder ='Datacenter location' and contains(@ng-model,'computeServer')]"
        );

        private readonly By OptionEurope_west3ForDataCenterLocationLocator = By.XPath
        (
            xpathToFind: "//md-option[@value='europe-west3' and contains(@ng-repeat,'computeServer')]"
        );

        private readonly By CommitedUsageInputLocator = By.XPath
        (
            xpathToFind: "//md-select[@placeholder = 'Committed usage' and contains(@ng-change,'computeServer')]"
        );

        private readonly By Option1YearForCommittedUsageLocator = By.XPath
        (
            xpathToFind:
            "//div[contains(@class, 'cpc-region-select')]//following-sibling::div[contains(@class, 'md-select-menu')]"
            + "//md-option[@value='1']"
        );

        private readonly By AddToEstimateButtonLocator = By.XPath
        (
            xpathToFind: "//button[@aria-label='Add to Estimate' and contains(@ng-disabled,'ComputeEngine')]"
        );

        private readonly By TotalPriceLocator = By.XPath
        (
            xpathToFind: "//div[contains(@ng-controller,'CloudCartCtrl')]//div[@class='md-list-item-text']/b"
        );

        public IWebElement OuterPricingCalculatorFrame => webDriver.FindElement(OuterPricingCalculatorFrameLocator);
        public IWebElement InsidePricingCalculatorFrame => webDriver.FindElement(InsidePricingCalculatorFrameLocator);
        public IWebElement ComputeEngineSection => webDriver.FindElement(ComputeEngineSectionLocator);
        public IWebElement EmailInputInEmailForm => webDriver.FindElement(EmailInputInEmailFormLocator);
        public IWebElement UserFirstNameInput => webDriver.FindElement(UserFirstNameInputLocator);
        public IWebElement UserLastNameInput => webDriver.FindElement(UserLastNameInputLocator);
        public IWebElement TotalPrice => webDriver.FindElement(TotalPriceLocator);
        public IWebElement NumberOfInstancesInput => webDriver.FindElement(NumberOfInstancesInputLocator);
        public IWebElement SeriesOfMachineInput => webDriver.FindElement(SeriesOfMachineInputLocator);
        public IWebElement MachineTypeInput => webDriver.FindElement(MachineTypeInputLocator);
        public IWebElement AddGPUsCheckbox => webDriver.FindElement(AddGPUsCheckboxLocator);
        public IWebElement GPUTypeInput=> webDriver.FindElement(GPUTypeInputLocator);
        public IWebElement NumberOfGpusInput => webDriver.FindElement(NumberOfGpusInputLocator);
        public IWebElement LocalSsdInput => webDriver.FindElement(LocalSsdInputLocator);
        public IWebElement DataCenterLocationInput => webDriver.FindElement(DataCenterLocationInputLocator);
        public IWebElement CommitedUsageInput => webDriver.FindElement(CommitedUsageInputLocator);
        public IWebElement AddToEstimateButton => webDriver.FindElement(AddToEstimateButtonLocator);
        public IWebElement SendEmailButton => webDriver.FindElement(SendEmailButtonLocator);
        

        private const string NumberOfInstances = "4";

        public PricingCalculatorPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public PricingCalculatorPage SelectComputerEngineSection()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);

            wait.Until(ExpectedConditions.ElementToBeClickable(SectionsContainerLocator));
            ComputeEngineSection.Click();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage FillInTheForm()
        {
            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);

            NumberOfInstancesInput.SendKeys(NumberOfInstances);
            SelectSeriesOfMachine();
            SelectMachineType();
            SetСheckboxAddGPUs();
            SelectGPUType();
            SelectNumberOfGpUs();
            SelectLocalSsdConfiguration();
            SelectDataCenterLocation();
            SelectCommittedUsagePeriod();

            webDriver.SwitchTo().DefaultContent();

            logger.Info("Compute engine form filled");

            return this;
        }

        public PricingCalculatorPage SetEmailAddressInput(string email)
        {
            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);
            
            EmailInputInEmailForm.SendKeys(email);

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage SetUserName(User user)
        {
            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);

            UserFirstNameInput.SendKeys(user.FirstName);
            UserLastNameInput.SendKeys(user.LastName);

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public double TotalPriceFromPage()
        {
            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);

            var price = ParsePrice(TotalPrice.Text);

            webDriver.SwitchTo().DefaultContent();

            return price;
        }

        private double ParsePrice(string price)
        {
            var regex = new Regex(@"(\d[0-9]*[,]*[0-9]*[.]+[0-9]+)", RegexOptions.Compiled, TimeSpan.FromSeconds(10));
            var matches = regex.Matches(price);
            
            var formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

            return double.Parse(matches.First().Value.Replace(",", ""), formatter);
        }

        private void SelectSeriesOfMachine()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            SeriesOfMachineInput.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(OptionN1ForMachineSeriesLocator)).Click();
        }

        private void SelectMachineType()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            MachineTypeInput.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(OptionN1_Standard_8ForMachineTypeLocator)).Click();
        }

        private void SetСheckboxAddGPUs()
        {
            AddGPUsCheckbox.Click();
        }

        private void SelectGPUType()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            GPUTypeInput.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(OptionNVIDIA_TESLA_V100ForGPUTypeLocator)).Click();
        }

        private void SelectNumberOfGpUs()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            NumberOfGpusInput.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(Option1ForNumberOfGPUsLocator)).Click();
        }

        private void SelectLocalSsdConfiguration()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            LocalSsdInput.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(Option2x375ForLocalSSDLocator)).Click();
        }

        private void SelectDataCenterLocation()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            DataCenterLocationInput.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(OptionEurope_west3ForDataCenterLocationLocator)).Click();
        }

        private void SelectCommittedUsagePeriod()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            CommitedUsageInput.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(Option1YearForCommittedUsageLocator)).Click();
        }

        public PricingCalculatorPage ClickAddToEstimateButton()
        {
            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);

            AddToEstimateButton.Click();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage ClickEmailEstimateButton()
        {
            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(WaitTimeOut));

            wait.Until(ExpectedConditions.ElementIsVisible(EmailEstimateButtonLocator)).Click();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage ClickButtonSendEmail()
        {
            webDriver.SwitchTo().Frame(OuterPricingCalculatorFrame);
            webDriver.SwitchTo().Frame(InsidePricingCalculatorFrame);

            SendEmailButton.Click();

            webDriver.SwitchTo().DefaultContent();

            logger.Info("Email sent");

            return this;
        }
    }
}