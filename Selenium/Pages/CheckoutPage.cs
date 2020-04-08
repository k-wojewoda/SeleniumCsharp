using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

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
        
        [FindsBy(How = How.XPath, Using = "//input[@id='name']")]
        public IWebElement Name { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//input[@id='surname']")]
        public IWebElement Surname { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//h5[.='Booking Summary']")]
        public IWebElement BookingSummary { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='email']")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='phone']")]
        public IWebElement Phone { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='birthday']")]
        public IWebElement Birthday { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='cardno']")]
        public IWebElement PassportNo { get; set; }        

        [FindsBy(How = How.XPath, Using = "//input[@id='expiration']")]
        public IWebElement ExpirationDate { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='select2-drop']/div/input")]
        public IWebElement FoatingTextBox { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@id='s2id_nationality']")]
        public IWebElement Nationality { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//select[@id='cardtype']")]
        public IWebElement CardType { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='expiry-month']")]
        public IWebElement ExpiryMonth { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='expiry-year']")]
        public IWebElement ExpiryYear { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//input[@id='card-number']")]
        public IWebElement CardNo { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//input[@id='cvv']")]
        public IWebElement CardCvv { get; set; }       

        [FindsBy(How = How.XPath, Using = "//button[@id='confirmBooking']")]
        public IWebElement ConfirmBooking { get; set; }
        

        [FindsBy(How = How.XPath, Using = "//label[@for='acceptTerm']")]
        public IWebElement AcceptTerm { get; set; }

        #endregion

        #region Methods

        #endregion
    }
}
