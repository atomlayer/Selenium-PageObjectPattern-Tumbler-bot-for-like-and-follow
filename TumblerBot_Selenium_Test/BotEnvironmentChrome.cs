using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Test_PagePatern
{
    public class BotEnvironmentChrome:BotEnvironmentBase
    {
        public BotEnvironmentChrome()
        {
            Initialize();
        }

        public void Initialize()
        {
            ChromeOptions options = new ChromeOptions();

            /*options.AddArgument(string.Format("user-data-dir={0}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+
                                                                   $"/Local/Google/Chrome/User Data/Profile 1"));*/
            options.AddArgument(string.Format("user-data-dir={0}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                                                   $"/Local/Google/Chrome/User Data/Profile 1"));

            options.AddArgument("--start-maximized");
            options.AddArgument("disable-infobars"); // disabling infobars
            options.AddArgument("--disable-extensions"); // disabling extensions
            options.AddArgument("--disable-gpu"); // applicable to windows os only
            options.AddArgument("--disable-dev-shm-usage"); // overcome limited resource problems
            options.AddArgument("--no-sandbox"); // Bypass OS security model

            //Browser = new RemoteWebDriver();
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver(options);
            Browser.Manage().Window.Maximize();
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse = Browser as IJavaScriptExecutor;

 
        }

        public async void RunAsync(BotBase Bot)
        {
            await Task.Run(() =>
            {
                Bot.Initialize(this);
                Bot.Action();
            });
        }

        public void Run(BotBase Bot)
        {
            {
                Bot.Initialize(this);
                Bot.Action();
            };
        }
    }
}
