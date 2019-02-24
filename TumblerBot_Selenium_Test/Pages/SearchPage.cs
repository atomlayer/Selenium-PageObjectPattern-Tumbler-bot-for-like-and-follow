using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using OpenQA.Selenium;

namespace TumblerBot_Selenium_Test.Pages
{
    class SearchPage:PageBase
    {
        public SearchPage(IWebDriver driver, Logger logger) : base(driver, logger)
        {
        }

 
        public List<string> GetImageLinks()
        {
            var imageLinks = driver.FindElements(By.CssSelector("div.post-info-tumblelog > a"));
            return imageLinks.Select(n => n.GetAttribute("href")).ToList();
        }
    }
}
