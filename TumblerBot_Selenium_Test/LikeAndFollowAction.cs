using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TumblerBot_Selenium_Test.Pages;

namespace TumblerBot_Selenium_Test
{
    class LikeAndFollowAction:ActionBase
    {
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
                var blogURL = BotEnvironment.DataBase.GetBlog();
                var imgesLinks = archivePage.GetImageLinks(blogURL).Take(BotEnvironment.Settings.MaxCountOfLikePerUser).ToList();
                foreach (var imageLink in imgesLinks)
                {
                    BotEnvironment.Driver.Navigate().GoToUrl(imageLink);
                    if(!postPage.PutLike(imageLink))
                    break;
                }
                if (imgesLinks.Count > 0) postPage.ToFollow(blogURL);

                int countOfLike = BotEnvironment.DataBase.GetCountOfLike,countOfFollow = BotEnvironment.DataBase.GetCountOfFollow;
                BotEnvironment.Logger.Trace($"Count of like: {countOfLike}| Count Of follow {countOfFollow}");
                if(countOfLike>=BotEnvironment.Settings.MaxCountOfLikePerDay | countOfFollow>=BotEnvironment.Settings.MaxCountOfFollowPerDay)
                    return;
            }
            
            
        }
    }
}
