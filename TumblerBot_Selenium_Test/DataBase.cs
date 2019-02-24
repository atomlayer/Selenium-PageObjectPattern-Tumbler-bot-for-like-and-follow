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

        public void AddLinkToImage(string input)
        {
            imageLinks.Add(input);
        }
    }
}
