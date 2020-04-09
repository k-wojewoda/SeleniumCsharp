using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Selenium.Support;

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
        
        [FindsBy(How = How.XPath, Using = "(//div[contains(@id,'s2id')])[2]")]
        public IWebElement FlightsFrom { get; set; }       
        
        [FindsBy(How = How.XPath, Using = "(//div[contains(@id,'s2id')])[3]")]
        public IWebElement FlightsTo { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@id='select2-drop']/div/input")]
        public IWebElement FloatingTextBox { get; set; }
        
        [FindsBy(How = How.XPath, Using = "(//div[@class='form-icon-left']/div/span/button[contains(@class,'up')])[3]")]
        public IWebElement AdultsAdd { get; set; }

        [FindsBy(How = How.XPath, Using = "(//div[@class='form-icon-left']/div/span/button[contains(@class,'up')])[4]")]
        public IWebElement ChildrenAdd { get; set; }        

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'login')]/a[@id='dropdownCurrency']")]
        public IWebElement MyAccount { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[.='Login']")]
        public IWebElement Login { get; set; }
          
        [FindsBy(How = How.XPath, Using = "(//label[.='Destination'])[1]")]
        public IWebElement Destination { get; set; }
        public By LoginXpath => By.XPath("//a[.='Login']");
        public By FloatingTextBoxXpath => By.XPath("//div[@id='select2-drop']/div/input");
        public By FlightsToListXpath => By.XPath("(//div[@class='select2-result-label'])[1]");
        #endregion

        #region Methods
        public void SelectCalendarDay(IWebDriver driver, string day)
        {
            Utils.ScrollToElementAndClick(driver, driver.FindElement(By.XPath("//*[@id='datepickers-container']/div[8]/div/div/div[2]/div[text()='" + day + "']")));
        }
        #endregion
    }
}
