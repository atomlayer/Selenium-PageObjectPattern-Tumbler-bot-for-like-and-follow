using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TumblerBot_Selenium_Test
{
    class Helper
    {
        static string FindGroup(string str, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.Match(str).Groups[1].Value;
        }

        public static string GetBlogURL(string postLink)
        {
            return FindGroup(postLink, @":\/\/([\w\d\-]+\.tumblr\.com)\/.?");
        }
    }
}
