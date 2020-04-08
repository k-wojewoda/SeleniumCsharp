using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Selenium.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.Pages
{
    public class SearchPage
    {
        private IWebDriver driver;
        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        #region Properties

        [FindsBy(How = How.XPath, Using = "//form[@class='row']//div[contains(@class,'price')]//strong")]
        public IList<IWebElement> Prices { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()='New York']")]
        public IList<IWebElement> StartingPoints { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()='Munich']")]
        public IList<IWebElement> DestPoints { get; set; }

        //first on page departure time
        [FindsBy(How = How.XPath, Using = "//form[@class='row']//div[@class='row']/div[position()=1]//p[@class='theme-search-results-item-flight-section-meta-time']")]
        public IWebElement DepartTime { get; set; }

        //first on page arrival time    
        [FindsBy(How = How.XPath, Using = "//form[@class='row']//div[@class='row']/div[position()=3]//p[@class='theme-search-results-item-flight-section-meta-time']")]
        public IWebElement ArrivalTime { get; set; }

        //first on page departure date
        [FindsBy(How = How.XPath, Using = "//form[@class='row']//div[@class='row']/div[position()=1]//p[@class='theme-search-results-item-flight-section-meta-date']")]
        public IWebElement DepartDate { get; set; }

        //first on page arrival date
        [FindsBy(How = How.XPath, Using = "//form[@class='row']//div[@class='row']/div[position()=3]//p[@class='theme-search-results-item-flight-section-meta-date']")]
        public IWebElement ArrivalDate { get; set; }

        //first on search page Book Now button
        [FindsBy(How = How.XPath, Using = "//button[text()='Book Now']")]
        public IWebElement BookNow { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='theme-search-results-item-flight-section-path-fly-time']//p[contains(.,'Trip Duration')]")]
        public IList<IWebElement> TripDurations { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Hide greeting']")]
        public IWebElement HideGreeting { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//span[@class='Linkify']")]
        public IWebElement Greeting { get; set; }

        #endregion

        #region Methods
        public bool ArePricesSorted()
        {
            List<Int16> pricesWeb = new List<Int16>();
            foreach (var p in Prices)
            {   //removes "USD "                
                pricesWeb.Add(Int16.Parse(p.Text.Substring(4)));
            }
            List<Int16> pricesSorted = new List<Int16>(pricesWeb);
            pricesSorted.Sort();
            return pricesWeb.SequenceEqual(pricesSorted);
        }

        public bool AreDepartureCitiesCorrect()
        {
            //check if all flights have NYC as a starting point
            return (Prices.Count() == StartingPoints.Count());
        }

        public bool AreArrivalCitiesCorrect()
        {
            //check if all flights have Munich as destination point
            return (Prices.Count() == DestPoints.Count());
        }

        public void ClickBookNow(IWebDriver driver, int index)
        {                         
            var element = driver.FindElement(By.XPath("(//button[text()='Book Now'])[" + index + "]"));
            Utils.ScrollToElementAndClick(driver, element);
         }
        #endregion
    }
}
