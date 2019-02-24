using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TumblerBot_Selenium_Test.Pages
{
    class ArchivePage:PageBase
    {
        public ArchivePage(IWebDriver driver, Logger logger) : base(driver,logger)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "div.post_glass.post_micro_glass_w_controls.post_micro_glass > a")]
        private IList<IWebElement> imageLinks;

        public List<string> GetImageLinks(string blogUrl)
        {
            return imageLinks.Select(n => n.GetAttribute("href")).ToList();
        }

    }
}
