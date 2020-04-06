using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Selenium.Support
{
    public class Utils
    {
        public static void WaitForElementVisible(IWebDriver driver, By path, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(path));
        }
    }
}
