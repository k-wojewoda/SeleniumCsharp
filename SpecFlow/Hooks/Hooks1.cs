using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace SpecFlow.Hooks
{
    [Binding]
    public class Hooks1
    {
        #region Fields
        private static readonly string ChormeDrvPath = ConfigurationManager.AppSettings["browserBinary"];
        private static readonly string HomeUrl = ConfigurationManager.AppSettings["homeUrl"];
        public static IWebDriver driver = null;
        #endregion

        #region Private methodes
        private static IWebDriver InitChromeDriver()
        {
            if (ChormeDrvPath == null)
            {
                throw new ConfigurationErrorsException("Chrome Browser Needs Binary Path");                
            }

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("incognito");
            options.AddArgument("--start-maximized");

            return new ChromeDriver(ChormeDrvPath, options);
        }
        #endregion

        [BeforeScenario]
        private void BeforeScenario()
        {
            driver = InitChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl(HomeUrl);
        }

        [AfterScenario]
        private void AfterScenario()
        {
            if (driver == null)
            {
                throw new Exception("Driver is null, call BeforeScenario() first.");
            }
            driver.Quit();
        }
    }
}
