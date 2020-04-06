using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Selenium.Pages
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region Properties
        [FindsBy(How = How.XPath, Using = "//span[@class='font700']")]
        public IList<IWebElement> DepartArrivalCheckout { get; set; }
        #endregion

        #region Methods

        #endregion
    }
}
