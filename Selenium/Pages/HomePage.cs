using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages
{
    public class HomePage 
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region Properties
        [FindsBy(How=How.XPath, Using = "//button[text()='Got it!']")]
        public IWebElement GotIt { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(@class, 'text-center flights')]")]
        public IWebElement Flights { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='FlightsDateStart']")]
        public IWebElement Depart { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='datepickers-container']/div[8]/nav/div[@data-action='next']")]
        public IWebElement MonthNext { get; set; }

        [FindsBy(How = How.XPath, Using = "//form[@name='flightmanualSearch']//button[@type='submit']")]
        public IWebElement Search { get; set; }
        #endregion

        #region Methods
        public IWebElement selectCalendarDay(string day)
        {
            return driver.FindElement(By.XPath("//*[@id='datepickers-container']/div[8]/div/div/div[2]/div[text()='" + day + "']"));
        }
        #endregion
    }
}
