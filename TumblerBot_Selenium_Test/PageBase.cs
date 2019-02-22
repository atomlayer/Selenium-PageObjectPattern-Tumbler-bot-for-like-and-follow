using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Test_PagePatern
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
