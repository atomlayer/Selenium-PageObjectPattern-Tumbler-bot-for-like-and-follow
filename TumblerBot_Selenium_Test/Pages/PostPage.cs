using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace TumblerBot_Selenium_Test.Pages
{
    class PostPage:PageBase
    {
        public PostPage(IWebDriver driver) : base(driver)
        {
        }

        public void MouseClickByLocator(String cssLocator)
        {
            IWebElement el = driver.FindElement(By.CssSelector(cssLocator));
            Actions builder = new Actions(driver);
            builder.MoveToElement(el).Click(el);
            builder.Perform();
        }

        [FindsBy(How = How.CssSelector, Using = "body > iframe.tmblr-iframe.tmblr-iframe--unified-controls.tmblr-iframe--loaded.iframe-controls--desktop")]
        private IWebElement iframe;

        public void SwitchToIFrame()
        {
            driver.SwitchTo().Frame(iframe);
        }

        public void PutLike()
        {
            SwitchToIFrame();
            MouseClickByLocator(@".tx-icon-button.like-button");
        }

        public void ToFollow()
        {
            SwitchToIFrame();
            MouseClickByLocator(@".tx-button.follow-button");
        }
    }
}
