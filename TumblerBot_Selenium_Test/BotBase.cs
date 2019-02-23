using System.Collections.Generic;
using OpenQA.Selenium;

namespace TumblerBot_Selenium_Test
{
    public abstract class BotBase
    {

        public IWebDriver Browser;
        public IJavaScriptExecutor Jse;
        public BotEnvironmentBase BotEnvironment;

        public void Initialize(BotEnvironmentBase botEnvironment)
        {
            BotEnvironment = botEnvironment;
            Browser = botEnvironment.Browser;
            Jse = botEnvironment.jse;
        }

        public virtual void Action()
        {
            
        }

        public virtual void Action(string message)
        {

        }

        public List<string> ResultList;
    }
}
