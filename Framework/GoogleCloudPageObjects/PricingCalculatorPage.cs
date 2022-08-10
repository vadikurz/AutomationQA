using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.GoogleCloudPageObjects
{
    public class PricingCalculatorPage
    {
        private IWebDriver webDriver;
    
        private readonly By computeEngineSection = By.XPath("//div[@class ='tab-holder compute']/parent::md-tab-item");
        private readonly By sectionsContainer = By.XPath("//md-pagination-wrapper");
        private readonly By insidePricingCalculatorFrame = By.XPath("//iframe[contains(@src, 'cloudpricingcalculator')]");
        private readonly By outerPricingCalculatorFrame = By.XPath("//iframe[contains(@src, 'products/calculator')]");
        private readonly By seriesOfMachineInput = By.XPath("//md-select[@placeholder = 'Series']");
        private readonly By optionN1ForMachineSeries = By.XPath("//md-option[@value='n1']");
        private readonly By machineTypeInput = By.XPath("//md-select[@placeholder = 'Instance type']");
        private readonly By gpuTypeInput = By.XPath("//md-select[@placeholder='GPU type']");
        private readonly By optionNVIDIA_TESLA_V100ForGPUType = By.XPath("//md-option[@value ='NVIDIA_TESLA_V100']");
        private readonly By numberOfGpusInput = By.XPath("//md-select[@placeholder = 'Number of GPUs']");
        private readonly By localSsdInput = By.XPath("//md-select[@placeholder = 'Local SSD']");
        private readonly By emailEstimateButton = By.XPath("//button[@title = 'Email Estimate']");
        private readonly By emailInputInEmailForm = By.XPath("//form[@name='emailForm']//input[@type='email']");
        private readonly By sendEmailButton = By.XPath("//button[@aria-label='Send Email']");
        private readonly By numberOfInstancesInput = By.XPath
        (
            xpathToFind: "//label[contains(text(),'Number of instances')]/following-sibling::input"
        );
        private readonly By optionN1_Standard_8ForMachineType = By.XPath
        (
            xpathToFind: "//md-option[@value='CP-COMPUTEENGINE-VMIMAGE-N1-STANDARD-8']"
        );
        private readonly By addGpusCheckbox = By.XPath
        (
            xpathToFind: "//md-checkbox[@aria-label='Add GPUs' and contains(@ng-model,'computeServer')]"
        );
        private readonly By option1ForNumberOfGpus = By.XPath
        (
            xpathToFind: "//md-option[contains(@ng-repeat,'GpuNumbers') and @value='1']"
        );
        private readonly By option2x375ForLocalSsd = By.XPath
        (
            xpathToFind: "//md-option[contains(@ng-repeat,'dynamicSsd') and @value='2']"
        );
        private readonly By dataCenterLocationInput = By.XPath
        (
            xpathToFind: "//md-select[@placeholder ='Datacenter location' and contains(@ng-model,'computeServer')]"
        );
        private readonly By optionEurope_west3ForDataCenterLocation = By.XPath
        (
            xpathToFind: "//md-option[@value='europe-west3' and contains(@ng-repeat,'computeServer')]"
        );
        private readonly By commitedUsageInput = By.XPath
        (
            xpathToFind: "//md-select[@placeholder = 'Committed usage' and contains(@ng-change,'computeServer')]"
        );
        private readonly By option1YearForCommittedUsage = By.XPath
        (
            xpathToFind:
            "//div[contains(@class, 'cpc-region-select')]//following-sibling::div[contains(@class, 'md-select-menu')]" 
            + "//md-option[@value='1']"
        );
        private readonly By addToEstimateButton = By.XPath
        (
            xpathToFind: "//button[@aria-label='Add to Estimate' and contains(@ng-disabled,'ComputeEngine')]"
        );
        private readonly By totalPrice = By.XPath
        (
            xpathToFind: "//div[contains(@ng-controller,'CloudCartCtrl')]//div[@class='md-list-item-text']/b"
        );

        public PricingCalculatorPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public PricingCalculatorPage SelectComputerEngineSection()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.SwitchTo().Frame(webDriver.FindElement(outerPricingCalculatorFrame));
            webDriver.SwitchTo().Frame(webDriver.FindElement(insidePricingCalculatorFrame));

            wait.Until(ExpectedConditions.ElementToBeClickable(sectionsContainer));
            webDriver.FindElement(computeEngineSection).Click();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage FillInTheForm()
        {
            webDriver.SwitchTo().Frame(webDriver.FindElement(outerPricingCalculatorFrame));
            webDriver.SwitchTo().Frame(webDriver.FindElement(insidePricingCalculatorFrame));

            SetNumberOfInstances();
            SelectSeriesOfMachine();
            SelectMachineType();
            SetСheckboxAddGPUs();
            SelectGPUType();
            SelectNumberOfGpUs();
            SelectLocalSsdConfiguration();
            SelectDataCenterLocation();
            SelectCommittedUsagePeriod();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage SetEmailAddressInput(string email)
        {
            webDriver.SwitchTo().Frame(webDriver.FindElement(outerPricingCalculatorFrame));
            webDriver.SwitchTo().Frame(webDriver.FindElement(insidePricingCalculatorFrame));

            webDriver.FindElement(emailInputInEmailForm).SendKeys(email);

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public double TotalPriceFromPage()
        {
            webDriver.SwitchTo().Frame(webDriver.FindElement(outerPricingCalculatorFrame));
            webDriver.SwitchTo().Frame(webDriver.FindElement(insidePricingCalculatorFrame));

            var price = ParsePrice(webDriver.FindElement(totalPrice).Text);

            webDriver.SwitchTo().DefaultContent();

            return price;
        }

        private double ParsePrice(string price)
        {
            var regex = new Regex(@"(\d[0-9]*[,]*[0-9]*[.]+[0-9]+)",RegexOptions.Compiled,TimeSpan.FromSeconds(10));
            var matches = regex.Matches(price);
            var formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
        
            return double.Parse(matches.First().Value.Replace(",", ""), formatter);
        }
    
        private void SetNumberOfInstances()
        {
            webDriver.FindElement(numberOfInstancesInput).SendKeys("4");
        }

        private void SelectSeriesOfMachine()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(seriesOfMachineInput).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(optionN1ForMachineSeries)).Click();
        }

        private void SelectMachineType()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(machineTypeInput).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(optionN1_Standard_8ForMachineType)).Click();
        }

        private void SetСheckboxAddGPUs()
        {
            webDriver.FindElement(addGpusCheckbox).Click();
        }

        private void SelectGPUType()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(gpuTypeInput).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(optionNVIDIA_TESLA_V100ForGPUType)).Click();
        }

        private void SelectNumberOfGpUs()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(numberOfGpusInput).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(option1ForNumberOfGpus)).Click();
        }

        private void SelectLocalSsdConfiguration()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(localSsdInput).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(option2x375ForLocalSsd)).Click();
        }

        private void SelectDataCenterLocation()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(dataCenterLocationInput).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(optionEurope_west3ForDataCenterLocation)).Click();
        }

        private void SelectCommittedUsagePeriod()
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            webDriver.FindElement(commitedUsageInput).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(option1YearForCommittedUsage)).Click();
        }

        public PricingCalculatorPage ClickAddToEstimateButton()
        {
            webDriver.SwitchTo().Frame(webDriver.FindElement(outerPricingCalculatorFrame));
            webDriver.SwitchTo().Frame(webDriver.FindElement(insidePricingCalculatorFrame));

            webDriver.FindElement(addToEstimateButton).Click();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage ClickEmailEstimateButton()
        {
            webDriver.SwitchTo().Frame(webDriver.FindElement(outerPricingCalculatorFrame));
            webDriver.SwitchTo().Frame(webDriver.FindElement(insidePricingCalculatorFrame));

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(emailEstimateButton)).Click();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }

        public PricingCalculatorPage ClickButtonSendEmail()
        {
            webDriver.SwitchTo().Frame(webDriver.FindElement(outerPricingCalculatorFrame));
            webDriver.SwitchTo().Frame(webDriver.FindElement(insidePricingCalculatorFrame));

            webDriver.FindElement(sendEmailButton).Click();

            webDriver.SwitchTo().DefaultContent();

            return this;
        }
    }
}