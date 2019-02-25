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

        private List<string> GetImagesLinks(string searchWord)
        {
            GoToURL($"https://www.tumblr.com/search/{searchWord}/recent");
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

        public string FindGroup(string str, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.Match(str).Groups[1].Value;
        }

        private void GetBlogs(List<string> imageLinks)
        {
            var output=imageLinks.Select(n => FindGroup(n, @":\/\/([\w\d\-]+\.tumblr\.com)\/.?"))
                .Distinct().ToList();
            BotEnvironment.DataBase.AddBlogs(output);
        }

        public override void Action()
        {
            foreach (var searchWord in BotEnvironment.Settings.SearchWords)
            {
                GetBlogs(GetImagesLinks(searchWord));
            }
        }
    }
}
