using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.Pages;
using Selenium.Support;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace SpecFlow.Steps
{
    [Binding]
    public sealed class BookFlightSteps :  Hooks.Hooks1
    {
        public static string DepartSearchPage = "DepartSearchPage";
        public static string ArrivalSearchPage = "ArrivalSearchPage";

        [Given(@"logged user is on the home page")]
        public void GivenUserIsOnTheHomePage()
        {
            HomePage home = new HomePage(driver);
            home.GotIt.Click();

            //log in
            home.MyAccount.Click();
            Utils.WaitForElementVisible(driver, home.LoginXpath, 10);
            home.Login.Click();

            LoginPage login = new LoginPage(driver);
            login.Email.SendKeys(ConfigurationManager.AppSettings["Email"]);
            login.Password.SendKeys(ConfigurationManager.AppSettings["password"]);
            login.Login.Click();

            AccountPage account = new AccountPage(driver);
            Utils.WaitForPageToLoad(driver, account.Greeting, "Hi, Demo User");
            account.Home.Click();
     }

        [Given(@"user goes to Flights tab")]
        public void GivenUserGoesToFlightsTab()
        {
            HomePage home = new HomePage(driver);
            Utils.WaitForPageToLoad(driver, home.Destination, "Destination");
            home.Flights.Click();
        }

        [Given(@"user enters flights booking data")]
        public void GivenUserEntersFlightsBookingData()
        {
            HomePage home = new HomePage(driver);

            home.FlightsFrom.Click();  
            Utils.WaitForElementVisible(driver, home.FloatingTextBoxXpath, 10);
            home.FloatingTextBox.Click();
            home.FloatingTextBox.SendKeys("NYC");
            home.FloatingTextBox.SendKeys(Keys.Tab);

            home.FlightsTo.Click();        
            Utils.WaitForElementVisible(driver, home.FloatingTextBoxXpath, 10);
            home.FloatingTextBox.Click();
            home.FloatingTextBox.SendKeys("MUC");            
            Utils.WaitForElementVisible(driver, home.FlightsToListXpath, 10);
            home.FloatingTextBox.SendKeys(Keys.Enter);

            //select date
            int daysForward = 14;
            home.Depart.Click();
            DateTime date = DateTime.Now.AddDays(daysForward);
            if (DateTime.Now.Month < date.Month)
            {             
                home.MonthNext.Click();
            }
            home.selectCalendarDay(driver, date.Day.ToString());

            home.AdultsAdd.Click();
            home.ChildrenAdd.Click();
            home.ChildrenAdd.Click();
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

            /**
            //CLOSE CHAT-WIDGET
            driver.SwitchTo().Frame("chat-widget");
            Utils.WaitForElementVisible(driver, By.XPath("//span[@class='Linkify']"), 30);
            Utils.HoverOver(driver, search.Greeting);
            search.HideGreeting.Click();
            driver.SwitchTo().DefaultContent();
            **/

            search.ClickBookNow(driver,TripIndex);
        }

        [Then(@"user enters personal and payment data")]
        public void ThenUserEntersPersonalAndPaymentData()
        {
            CheckoutPage checkout = new CheckoutPage(driver);
            Utils.WaitForPageToLoad(driver, checkout.BookingSummary, "Booking Summary");

            checkout.Name.SendKeys("Agata");
            checkout.Surname.SendKeys("Brzozowska");
            checkout.Email.SendKeys("user@phptravels.com");
            checkout.Phone.SendKeys("987654321");
            checkout.Birthday.SendKeys("2000-01-01");
            checkout.PassportNo.SendKeys("54321");
            checkout.ExpirationDate.SendKeys("2025-01-01");

            checkout.Nationality.Click();
            checkout.FoatingTextBox.SendKeys("Poland");
            checkout.FoatingTextBox.SendKeys(Keys.Tab);

            SelectElement CardTypeSelect = new SelectElement(checkout.CardType);
            CardTypeSelect.SelectByIndex(1);
            SelectElement ExpiryMonthSelect = new SelectElement(checkout.ExpiryMonth);
            ExpiryMonthSelect.SelectByIndex(1);
            SelectElement ExpiryYearSelect = new SelectElement(checkout.ExpiryYear);
            ExpiryYearSelect.SelectByIndex(3);

            checkout.CardNo.SendKeys("378282246310005");
            checkout.CardCvv.SendKeys("123");
            checkout.AcceptTerm.Click();
        }

        [Then(@"user can complete booking")]
        public void ThenUserCanCompleteBooking()
        {
            CheckoutPage checkout = new CheckoutPage(driver);
            checkout.ConfirmBooking.Click();
        }

    }
}
