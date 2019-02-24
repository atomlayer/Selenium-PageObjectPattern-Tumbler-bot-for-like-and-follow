using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TumblerBot_Selenium_Test.Pages
{
    class PageBase
    {
        protected IWebDriver driver;

        public PageBase(IWebDriver driver)
        {
           this.driver = driver;
           PageFactory.InitElements(driver, this);
        }
    }
}
