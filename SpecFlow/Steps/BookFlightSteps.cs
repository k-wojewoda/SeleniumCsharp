using System;
using NUnit.Framework;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Selenium.Pages;
using System.Threading;
using Selenium.Support;

namespace SpecFlow.Steps 
{
    [Binding]
    public sealed class BookFlightSteps :  Hooks.Hooks1
    {
        public static string DepartSearchPage = "DepartSearchPage";
        public static string ArrivalSearchPage = "ArrivalSearchPage";

        [Given(@"user is on the home page")]
        public void GivenUserIsOnTheHomePage()
        {
            new HomePage(driver).GotIt.Click();
        }

        [Given(@"user goes to Flights tab")]
        public void GivenUserGoesToFlightsTab()
        {
            new HomePage(driver).Flights.Click();
        }

        [Given(@"user enters flights booking data")]
        public void GivenUserEntersFlightsBookingData()
        {
            //select date
            HomePage home = new HomePage(driver);
            home.Depart.Click();
            DateTime date = DateTime.Now.AddDays(14);
            if (DateTime.Now.Month < date.Month)
            { //change to the next month              
                home.MonthNext.Click();
            }

            Utils.WaitForElementVisible(driver, By.XPath("//*[@id='datepickers-container']/div[8]/div/div/div[2]/div[text()='" + "20" + "']"), 10);
            home.selectCalendarDay(date.Day.ToString()).Click();
        }

        [When(@"user presses Search")]
        public void WhenUserPressesSearch()
        {
            new HomePage(driver).Search.Click();
        }

        [Then(@"the search results are sorted by price ascending, departures and arrivals cities are correct")]
        public void ThenTheSearchResultsAreSortedAscendingDeparturesAndArrivalsCitiesAreCorrect()
        {
            SearchPage search = new SearchPage(driver);

            Assert.IsTrue(search.ArePricesSorted());
            Assert.IsTrue(search.AreDepartureCitiesCorrect());
            Assert.IsTrue(search.AreArrivalCitiesCorrect());            
        }

        [When(@"user sees on a search result page departure and arrival times")]
        public void WhenUserSeesOnASearchResultPageDepartureAndArrivalTimes()
        {
            SearchPage search = new SearchPage(driver);
            //first on page departure time
            string DepartTimeStr = search.DepartTime.Text;
            //first on page arrival time
            string ArrivalTimeStr= search.ArrivalTime.Text;
            //first on page departure date
            string DepartDateStr = search.DepartDate.Text;
            //first on page arrival date
            string ArrivalDateStr = search.ArrivalDate.Text;

            string departDateTimeOnSearchPage = DepartDateStr + " " + DepartTimeStr;
            string arrivalDateTimeOnSearchPage = ArrivalDateStr + " " + ArrivalTimeStr;

            DateTime departOnSearchPageDateTime = BasicHelper.ConvertToDateTime(departDateTimeOnSearchPage);
            DateTime arrivalOnSearchPageDateTime = BasicHelper.ConvertToDateTime(arrivalDateTimeOnSearchPage);

            ScenarioContext.Current.Add(DepartSearchPage, departOnSearchPageDateTime);
            ScenarioContext.Current.Add(ArrivalSearchPage, arrivalOnSearchPageDateTime);
        }

        [When(@"user chooses flight by pressing Book Now")]
        public void WhenUserChoosesFlightByPressingBookNow()
        {
            //first on page Book Now button click
            new SearchPage(driver).BookNow.Click();
        }

        [Then(@"on the checkout page booking summary displays correct information")]
        public void ThenOnTheCheckoutPageBookingSummaryDisplaysCorrectInformation()
        {
            CheckoutPage checkout = new CheckoutPage(driver);          

            DateTime departDateTimeCheckout = BasicHelper.ConvertToDateTime(checkout.DepartArrivalCheckout[0].Text);
            DateTime arrivalDateTimeCheckout = BasicHelper.ConvertToDateTime(checkout.DepartArrivalCheckout[1].Text);

            Assert.AreEqual(ScenarioContext.Current[DepartSearchPage], departDateTimeCheckout, "Departure Date / Time are different on Search and Checkout page");
            Assert.AreEqual(ScenarioContext.Current[ArrivalSearchPage], arrivalDateTimeCheckout, "Arrival Date / Time are different on Search and Checkout page");
        }

        [When(@"user chooses fastest flight by pressing Book Now")]
        public void WhenUserChoosesFastestFlightByPressingBookNow()
        {         
            SearchPage search = new SearchPage(driver);
            int TripIndex = BasicHelper.GetIndexOfFastestFlight(search.TripDurations);
            search.ClickBookNow(TripIndex);
        }

    }
}
