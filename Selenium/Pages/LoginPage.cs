using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Selenium.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region Properties        
        [FindsBy(How = How.XPath, Using = "//input[@name='username']")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='password']")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[.='Login']")]
        public IWebElement Login { get; set; }
       
        #endregion

        #region Methods
       
        #endregion
    }
}
