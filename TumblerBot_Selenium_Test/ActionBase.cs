using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium;

namespace TumblerBot_Selenium_Test
{
    public abstract class ActionBase
    {
        public BotEnvironmentBase BotEnvironment;

        public ActionBase(BotEnvironmentBase botEnvironment)
        {
            BotEnvironment = botEnvironment;
        }


        public void Initialize(BotEnvironmentBase botEnvironment)
        {
            BotEnvironment = botEnvironment;
        }

        public abstract void Action();

        public void GoToURL(string URL)
        {
            BotEnvironment.Driver.Navigate().GoToUrl(URL);
        }

        public  void ScrollDown()
        {
            try
            {
                BotEnvironment.Jse.ExecuteScript("window.scrollBy(0,1000)");
            }
            catch (Exception e)
            {
                BotEnvironment.Logger.Error(e);
            }

        }
    }
}
