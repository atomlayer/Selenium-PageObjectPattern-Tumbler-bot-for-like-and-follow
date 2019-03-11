using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TumblerBot_Selenium_Test.Pages;

namespace TumblerBot_Selenium_Test
{
    class SearshBlogsAction:ActionBase
    {
        private DataManager _DM;

        public SearshBlogsAction(DataManager dm, BotEnvironmentBase botEnvironment) : base(botEnvironment)
        {
            _DM = dm;
        }

        int _countLoop = 150;

        private List<string> GetPostLinks(string searchWord)
        {
            GoToURL($"https://www.tumblr.com/search/{searchWord}/recent");
            SearchPage searchPage=new SearchPage(BotEnvironment.Driver, BotEnvironment.Logger);
            List<string> links = new List<string>();

            for (int i = 0; i < _countLoop; i++)
            {
                ScrollDown();
                try{ links.AddRange(searchPage.GetImageLinks());}
                catch (Exception e) { /*BotEnvironment.Logger.Debug(e.Message);*/ }
                Thread.Sleep(BotEnvironment.Settings.Delay);
            }
            return links.Distinct().ToList();
        }

        private void GetBlogs(List<string> postLinks)
        {
            var output=postLinks.Select(Helper.GetBlogURL).Distinct().Select(n=>new Blog(){URL=n}).ToList();
            _DM.AddBlogs(output);
        }

        public override void Action()
        {
            foreach (var searchWord in BotEnvironment.Settings.SearchWords)
            {
                GetBlogs(GetPostLinks(searchWord));
            }
        }
    }
}
