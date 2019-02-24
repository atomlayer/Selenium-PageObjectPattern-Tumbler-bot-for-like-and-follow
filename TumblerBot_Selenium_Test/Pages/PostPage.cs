using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace TumblerBot_Selenium_Test.Pages
{
    class PostPage:PageBase
    {
        public PostPage(IWebDriver driver, Logger logger) : base(driver, logger)
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

        void SwitchToIFrame()
        {
            try {   driver.SwitchTo().Frame(iframe);}
            catch (Exception ex){logger.Error(ex.Message);}
        }

        public bool PutLike(string postURL)
        {
            SwitchToIFrame();
            try
            {
                MouseClickByLocator(@".tx-icon-button.like-button");
                logger.Trace($"Liked:  {postURL}");
                return true;
            }
            catch(Exception ex) { logger.Error($"Not liked: {postURL} | {ex.Message}");}

            return false;
        }

        public bool ToFollow(string postURL)
        {
            try
            {
                MouseClickByLocator(@".tx-button.follow-button");
                logger.Trace($"Followed:  {postURL}");
                return true;
            }
            catch (Exception ex) { logger.Error($"Not followed: {postURL} | {ex.Message}"); }

            return false;
        }
    }
}
