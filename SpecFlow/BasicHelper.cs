using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpecFlow
{
    public class BasicHelper
    {
        public static DateTime ConvertToDateTime(string text)
        {
            DateTime date = DateTime.Now;
            try
            {
                date = DateTime.Parse(text);
            }
            catch (FormatException)
            {
                Assert.Fail("Unable to parse the specific date");
            }
            return date;
        }

        public static int GetIndexOfFastestFlight(IList<IWebElement> tripDurations)
        {
            IList<string> TripDurationsString = new List<string>();  
            foreach (var d in tripDurations)
            {
                string temp = d.Text.Substring(14).Replace('h', ':');
                temp = temp.Replace('m', ' ');
                temp = Regex.Replace(temp, @"\s+", "");

                string hours = temp.Substring(0, temp.IndexOf(':'));
                string min = temp.Substring(temp.IndexOf(':')+1);
                int hoursInt = Int16.Parse(hours);
                int days = hoursInt / 24;
                int hourInt = hoursInt - (days * 24);
                temp=days.ToString() +':'+ hoursInt.ToString() +':'+ min;

                TripDurationsString.Add(temp);
            }

            List<TimeSpan> TripDurationsTimeSpan = new List<TimeSpan>();
            foreach (var d in TripDurationsString)
            {
                try
                {
                    TripDurationsTimeSpan.Add(TimeSpan.Parse(d));
                }
                catch (FormatException)
                {
                    Assert.Fail("Bad format of Trip Duration");
                }
                catch (OverflowException)
                {
                    Assert.Fail("Overflow parsing Trip Duration");
                }
            }
            IList<TimeSpan> TripDurationsNotSorted = new List<TimeSpan>(TripDurationsTimeSpan);
            TripDurationsTimeSpan.Sort();
            int TripIndex = TripDurationsNotSorted.IndexOf(TripDurationsTimeSpan[0]);
            return ++TripIndex;
        }
    }
}
