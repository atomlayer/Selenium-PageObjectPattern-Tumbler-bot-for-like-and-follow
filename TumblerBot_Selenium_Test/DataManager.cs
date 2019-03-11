using System.Collections.Generic;
using System.Linq;

namespace TumblerBot_Selenium_Test
{
    public class DataManager
    {

        List<Blog> blogs =new List<Blog>();
 
        public void AddBlogs(List<Blog> newBlogs)
        {
            blogs.AddRange(newBlogs);
            blogs=blogs.Distinct().ToList();
        }

        public int CountOfLike()
        {
            return blogs.SelectMany(n => n.Posts).Count(n => n.IsLiked);
        }

        public int CountOfFollow()
        {
            return blogs.Count(n => n.Isfollowed);
        }
 
        public Blog GetBlog()
        {
            return blogs.Where(n => n.Isfollowed == false & n.IsErrorState==false & n.Posts.All(nn=>nn.IsErrorState==false))
                .OrderByDescending(n=>n.URL).First();
        }


        public bool BlogsExists()
        {
            return blogs.Count(n => n.Isfollowed == false & n.IsErrorState == false & n.Posts.All(nn => nn.IsErrorState == false)) > 0;
        }
    }

    public class Blog
    {
        public string URL;
        public bool IsErrorState = false;
        public bool Isfollowed=false;
        public List<Post> Posts=new List<Post>();

        public void AddPosts(List<string> posts)
        {
            Posts = posts.Select(n => new Post() {URL = n}).ToList();
            if (Posts.Count == 0)
                IsErrorState = true;
        }
    }

    public class Post
    {
        public string URL;
        public bool IsErrorState = false;
        public bool IsLiked=false;
    }
}
