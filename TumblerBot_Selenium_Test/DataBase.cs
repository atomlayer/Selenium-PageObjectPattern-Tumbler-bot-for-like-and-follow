using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumblerBot_Selenium_Test
{
    public class DataBase
    {
        private List<string> imageLinks;
        public List<string> blogs;
        public int GetCountOfLike { get; set; }
        public int GetCountOfFollow { get; set; }

        public void AddLinkToImage(string input)
        {
            imageLinks.Add(input);
        }

        public void AddBlogs(List<string> blogs)
        {
            this.blogs = blogs;
        }

        private int i = 0;
        public string GetBlog()
        {
            return blogs[++i];
        }

        public bool BlogsExists()
        {
            throw new NotImplementedException();
        }
    }
}
