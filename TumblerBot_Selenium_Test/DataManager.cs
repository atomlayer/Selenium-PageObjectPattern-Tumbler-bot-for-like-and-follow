using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumblerBot_Selenium_Test
{
    public class DataManager
    {
        private string _connectionString = @"Data Source = database ; Version=3";

        private SQLiteConnection connection;
        public void ConnectionOpen()
        {
            connection = new SQLiteConnection(_connectionString);
            connection.Open();
        }

        public void ConnectionClose()
        {
            connection.Close();
        }

        public SQLiteDataReader Select(string sql)
        {
            return new SQLiteCommand(sql, connection).ExecuteReader();
        }

        public void ExecuteNonQuery(string sql)
        {
            new SQLiteCommand(sql, connection).ExecuteNonQuery();
        }


        List<string> GetBlogsDB()
        {
            var reader = Select("select blog_url from blogs;");
            List<string> blogsDB=new List<string>();
            while (reader.Read())
            {
                blogsDB.Add((string)reader["blog_url"]);
            }
            return blogsDB;
        }

        public void AddBlogs(List<string> newBlogs)
        {
            var blogs = newBlogs.Except(GetBlogsDB()).ToList();
            ExecuteNonQuery("begin");
            blogs.ForEach(n=>ExecuteNonQuery($"insert into blogs (blog_url, time, to_follow, error_state) " +
                                    $"values (\"{n}\",current_timestamp, false, false);"));
            ExecuteNonQuery("end");
        }

        public void AddPosts(List<string> posts, string blog)
        {
            var blogId = GetBlogID(blog);
            if (blogId != null)
            {
                ExecuteNonQuery("begin");
                posts.ForEach(n=> ExecuteNonQuery($"insert into posts(blog_id, post_url, time, liked, error_state) values({blogId}, \"{n}\", current_timestamp, false, false);"));
                ExecuteNonQuery("end");
            }
            else throw new Exception("Blog with the specified id does not exist");
        }

        public int GetCountOfLikePerDay()
        {
            int count = 0;
            var reader = Select(@"select count(*) as count from posts
                                  where strftime('%d-%m-%Y', time) =strftime('%d-%m-%Y', 'now') and liked=true;");
            if (reader.Read())
                count = Convert.ToInt32(reader["count"]);
            return count;
        }

        public int GetCountOfFollowPerDay()
        {
            int count = 0;
            var reader = Select(@"select count(*) as count from blogs 
                                  where strftime('%d-%m-%Y', time) =strftime('%d-%m-%Y', 'now') and to_follow=true;");
            if (reader.Read())
                count = Convert.ToInt32(reader["count"]);
            return count;
        }

        int? GetBlogID(string BlogURL)
        {
            int? id = null; 
            var reader = Select($"select id from blogs where blog_url in (\"{BlogURL}\") limit 1;");
            if (reader.Read())
                id= Convert.ToInt32(reader["id"]);
            return id;
        }

        public void AddPost(string postURL)
        {
            int? blogId = GetBlogID(Helper.GetBlogURL(postURL));
            if (blogId != null)
            {
                ExecuteNonQuery($"insert into posts(blog_id, post_url, time, liked, error_state) " +
                       $"values({blogId}, \"{postURL}\", current_timestamp, false, false); ");
            }
            else throw new Exception("Blog with the specified id does not exist");
        }


        public string GetBlog()
        {
            string blogUrl="";
            var reader = Select(@"select blog_url from 
                                  (select blog_url from blogs
                                  where to_follow = false and error_state = false
                                  order by time desc)
                                  order by rowid asc limit 1;");
            if (reader.Read())
                blogUrl = (string) reader["blog_url"];
            return blogUrl;
        }



        public bool BlogsExists()
        {
            int count = 0;
            var reader = Select(@"select count(*) as count from blogs where to_follow=false and error_state=false;");
            if (reader.Read())
                count = Convert.ToInt32(reader["count"]);
            return count>0;
        }

        public void SetPostURLIsLiked(string postUrl)
        {
            ExecuteNonQuery($"update posts set liked=true where post_url=\"{postUrl}\";");
        }

        public void SetBlogURLIsFollowed(string blogUrl)
        {
            ExecuteNonQuery($"update blogs set to_follow = true where blog_url = \"{blogUrl}\"");
        }


        public void SetPostURLErrorState(string postUrl)
        {
            ExecuteNonQuery($"update posts set error_state=true where post_url=\"{postUrl}\";");
        }


        public void SetBlogURLErrorState(string blogUrl)
        {
            ExecuteNonQuery($"update blogs set error_state=true where blog_url=\"{blogUrl}\";");

        }
    }
}
