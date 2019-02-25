using NLog;
using OpenQA.Selenium;

namespace TumblerBot_Selenium_Test
{
    public abstract class BotEnvironmentBase
    {
        public IWebDriver Driver;
        public IJavaScriptExecutor Jse;
        public Logger Logger;
        public Settings Settings;
        public DataBase DataBase;

        protected BotEnvironmentBase(Logger logger, Settings settings)
        {
            Logger = logger;
            Settings = settings;
            Initialize();
        }

        public virtual void Initialize()
        {
            Jse = Driver as IJavaScriptExecutor;
            DataBase =new DataBase();
        }
    }
}
