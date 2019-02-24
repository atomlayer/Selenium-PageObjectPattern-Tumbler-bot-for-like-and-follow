using NLog;
using OpenQA.Selenium;

namespace TumblerBot_Selenium_Test
{
    public abstract class BotEnvironmentBase
    {
        public IWebDriver Browser;
        public IJavaScriptExecutor jse;
        public Logger Logger;
    }
}
