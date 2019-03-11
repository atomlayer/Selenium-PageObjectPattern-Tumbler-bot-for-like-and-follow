using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using TumblerBot_Selenium_Test.Pages;

namespace TumblerBot_Selenium_Test
{
    class LikeAndFollowAction:ActionBase
    {

        private ArchivePage _archivePage;
        private PostPage _postPage;
        private  DataManager _DM;


        public LikeAndFollowAction(){}
        
        public LikeAndFollowAction(BotEnvironmentBase botEnvironment) : base(botEnvironment) {}

        public void Initialize()
        {
            _archivePage = new ArchivePage(BotEnvironment.Driver, BotEnvironment.Logger);
            _postPage = new PostPage(BotEnvironment.Driver, BotEnvironment.Logger);
            _DM = new DataManager();
        }

        private bool likePosts(Blog blog)
        {
            if (blog.Posts.Count() == 0) return false;

            foreach (var post in blog.Posts)
            {
                GoToURL(post.URL);
                if (_postPage.PutLike(post.URL) )
                {
                    post.IsLiked = true;
                    BotEnvironment.Logger.Info($"Liked: {post.URL}");
                    Thread.Sleep(BotEnvironment.Settings.Delay);
                }
                else
                {
                    post.IsErrorState = true;
                    BotEnvironment.Logger.Info($"Error! Not liked: {post.URL}");
                    return false;
                }
            }
            return true;
        }

        private void ToFollow(Blog blog)
        {
            if (_postPage.ToFollow(blog.URL))
            {
                blog.Isfollowed = true;
                BotEnvironment.Logger.Info($"Followed: {blog.URL}");
            }
            else
            {
                blog.IsErrorState = true;
                BotEnvironment.Logger.Info($"Error! Not followed: {blog.URL}");
            }
        }

        public override void Action()
        {
            Initialize();
            new SearshBlogsAction(_DM, BotEnvironment).Action();

            while (_DM.BlogsExists())
            {
                var blog = _DM.GetBlog();

                GoToURL($"https://{blog.URL}/archive/{DateTime.Now.Year}/{DateTime.Now.Month}");
                blog.AddPosts(_archivePage.GetImageLinks(blog.URL).Take(BotEnvironment.Settings.MaxCountOfLikePerUser).ToList());

                if(likePosts(blog))
                    ToFollow(blog);

                BotEnvironment.Logger.Trace($"Count of like: {_DM.CountOfLike()}| Count Of follow {_DM.CountOfFollow()}");
                if(_DM.CountOfLike() >= BotEnvironment.Settings.MaxCountOfLikePerDay | 
                   _DM.CountOfFollow() >= BotEnvironment.Settings.MaxCountOfFollowPerDay)
                    return;
            }

        }

    }
}
