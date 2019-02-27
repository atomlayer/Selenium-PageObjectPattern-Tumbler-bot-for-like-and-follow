using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TumblerBot_Selenium_Test.Pages;

namespace TumblerBot_Selenium_Test
{
    class LikeAndFollowAction:ActionBase
    {
        public LikeAndFollowAction()
        {
        }

        public LikeAndFollowAction(BotEnvironmentBase botEnvironment) : base(botEnvironment)
        {
        }

        private void likePosts(List<string> imgesLinks, PostPage postPage)
        {
            foreach (var postUrl in imgesLinks)
            {
                GoToURL(postUrl);
                if (postPage.PutLike(postUrl))
                {
                    BotEnvironment.DB.SetPostURLIsLiked(postUrl);
                    Thread.Sleep(BotEnvironment.Settings.Delay);
                }
                else
                {
                    BotEnvironment.DB.SetPostURLErrorState(postUrl);
                    break;
                }
            }
        }

        private void ToFollow(List<string> imgesLinks, PostPage postPage, string blogUrl)
        {
            if (imgesLinks.Count > 0)
            {
                if(postPage.ToFollow(blogUrl))
                    BotEnvironment.DB.SetBlogURLIsFollowed(blogUrl);
                else BotEnvironment.DB.SetBlogURLErrorState(blogUrl);
            }
        }

        public override void Action()
        {
            BotEnvironment.DB.ConnectionOpen();
            new SearshBlogsAction(BotEnvironment).Action();

            ArchivePage archivePage =new ArchivePage(BotEnvironment.Driver, BotEnvironment.Logger);
            PostPage postPage =new PostPage(BotEnvironment.Driver, BotEnvironment.Logger);

            while (BotEnvironment.DB.BlogsExists())
            {
                var blogUrl = BotEnvironment.DB.GetBlog();
                GoToURL($"https://{blogUrl}/archive/{DateTime.Now.Year}/{DateTime.Now.Month}");
                var imgesLinks = archivePage.GetImageLinks(blogUrl).Take(BotEnvironment.Settings.MaxCountOfLikePerUser).ToList();
                BotEnvironment.DB.AddPosts(imgesLinks, blogUrl);

                likePosts(imgesLinks, postPage);
                ToFollow(imgesLinks, postPage, blogUrl);

                int countOfLike = BotEnvironment.DB.GetCountOfLikePerDay(),countOfFollow = BotEnvironment.DB.GetCountOfFollowPerDay();
                BotEnvironment.Logger.Trace($"Count of like: {countOfLike}| Count Of follow {countOfFollow}");
                if(countOfLike>=BotEnvironment.Settings.MaxCountOfLikePerDay | countOfFollow>=BotEnvironment.Settings.MaxCountOfFollowPerDay)
                    return;
            }
            BotEnvironment.DB.ConnectionClose();
        }

    }
}
