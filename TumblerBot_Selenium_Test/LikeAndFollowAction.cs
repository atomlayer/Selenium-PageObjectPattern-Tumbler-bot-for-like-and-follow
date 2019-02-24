using System;
using System.Linq;
using System.Threading;
using TumblerBot_Selenium_Test.Pages;

namespace TumblerBot_Selenium_Test
{
    class LikeAndFollowAction:ActionBase
    {
        public LikeAndFollowAction() : base()
        {
        }

        public LikeAndFollowAction(BotEnvironmentBase botEnvironment) : base(botEnvironment)
        {
        }


        public override void Action()
        {
            SearshBlogsAction searshBlogsAction =new SearshBlogsAction(BotEnvironment);
            searshBlogsAction.Action();

            ArchivePage archivePage =new ArchivePage(BotEnvironment.Driver, BotEnvironment.Logger);
            PostPage postPage =new PostPage(BotEnvironment.Driver, BotEnvironment.Logger);

            while (BotEnvironment.DataBase.BlogsExists())
            {
                var blogUrl = BotEnvironment.DataBase.GetBlog();
                var x = $"https://{blogUrl}/archive/{DateTime.Now.Year}/{DateTime.Now.Month}";
                GoToURL($"https://{blogUrl}/archive/{DateTime.Now.Year}/{DateTime.Now.Month}");

                var imgesLinks = archivePage.GetImageLinks(blogUrl).Take(BotEnvironment.Settings.MaxCountOfLikePerUser).ToList();
                foreach (var imageLink in imgesLinks)
                {
                    GoToURL(imageLink);
                    if(!postPage.PutLike(imageLink))
                    break;
                    Thread.Sleep(BotEnvironment.Settings.Delay);
                }
                if (imgesLinks.Count > 0) postPage.ToFollow(blogUrl);

                int countOfLike = BotEnvironment.DataBase.GetCountOfLike,countOfFollow = BotEnvironment.DataBase.GetCountOfFollow;
                BotEnvironment.Logger.Trace($"Count of like: {countOfLike}| Count Of follow {countOfFollow}");
                if(countOfLike>=BotEnvironment.Settings.MaxCountOfLikePerDay | countOfFollow>=BotEnvironment.Settings.MaxCountOfFollowPerDay)
                    return;
            }

        }
    }
}
