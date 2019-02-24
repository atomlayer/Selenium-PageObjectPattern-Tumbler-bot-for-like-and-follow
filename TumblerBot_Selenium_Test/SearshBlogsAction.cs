using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NLog.Fluent;
using OpenQA.Selenium;
using TumblerBot_Selenium_Test.Pages;

namespace TumblerBot_Selenium_Test
{
    class SearshBlogsAction:ActionBase
    {
        public SearshBlogsAction(BotEnvironmentBase botEnvironment) : base(botEnvironment)
        {
        }

        int _countLoop = 150;

        private List<string> GetImagesLinks()
        {
            BotEnvironment.Driver.Navigate().GoToUrl(Url);
            SearchPage searchPage=new SearchPage(BotEnvironment.Driver, BotEnvironment.Logger);
            List<string> links = new List<string>();

            for (int i = 0; i < _countLoop; i++)
            {
                ScrollDown();
                try{ links.AddRange(searchPage.GetImageLinks());}
                catch (Exception e) { BotEnvironment.Logger.Error(e.Message); }
                Thread.Sleep(BotEnvironment.Settings.Delay);
            }
            return links.Distinct().ToList();
        }

        private void GetBlogs(List<string> imageLinks)
        {
            int count = 0;
            Regex regex = new Regex(@":\/\/([\w\d\-]+\.tumblr\.com)\/.?");
            List<string> output = new List<string>();

            while (imageLinks.Count > 0)
            {
                output.Add(imageLinks[0]);
                string value = regex.Match(imageLinks[count]).Groups[1].Value;
                List<string> newlinks = new List<string>();

                foreach (var link in imageLinks)
                {
                    if (!link.Contains(value))
                        newlinks.Add(link);
                }
                imageLinks = newlinks;
            }
            BotEnvironment.DataBase.AddBlogs(output);
        }

        public override void Action()
        {
            GetBlogs(GetImagesLinks());
        }
    }
}
