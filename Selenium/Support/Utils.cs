using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.Support
{
    public class Utils
    {
        public static void WaitForElementVisible(IWebDriver driver, By path, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(ExpectedConditions.ElementIsVisible(path));
        }

        public static void WaitForPageToLoad(IWebDriver driver, IWebElement elementOnNewPage, string textOnNewPage)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(ExpectedConditions.TextToBePresentInElement(elementOnNewPage, textOnNewPage));
        }
        public static void HoverOver(IWebDriver driver, IWebElement element)
        {
            Actions actionBuilder = new Actions(driver);
            actionBuilder.MoveToElement(element).Build().Perform();
        }
        public static void ScrollToElementAndClick(IWebDriver driver, IWebElement element)
        { 
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Build().Perform();            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            element.Click();
        }   
  
    }
}



