using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Selenium.Pages
{
    public class AccountPage
    {
        private IWebDriver driver;
        public AccountPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region Properties        
        
        [FindsBy(How = How.XPath, Using = "//a[.='Home']")]
        public IWebElement Home { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//h3[.='Hi, Demo User']")]
        public IWebElement Greeting { get; set; }
        #endregion

        #region Methods

        #endregion
    }
}
