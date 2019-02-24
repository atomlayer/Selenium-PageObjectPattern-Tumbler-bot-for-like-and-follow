using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TumblerBot_Selenium_Test.Pages
{
    abstract class PageBase
    {
        protected IWebDriver driver;
        protected Logger logger;

        public PageBase(IWebDriver driver, Logger logger)
        {
            this.driver = driver;
            this.logger = logger;
            PageFactory.InitElements(driver, this);
        }
    }
}
