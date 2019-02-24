using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TumblerBot_Selenium_Test
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Settings
    {
        [JsonProperty]
        public int MaxCountOfLikePerDay;

        [JsonProperty]
        public int MaxCountOfLikePerUser;

        [JsonProperty]
        public int MaxCountOfFollowPerDay;

        [JsonProperty]
        public List<string> SearchWords;

        public int Delay;

        private static string _settingsFile=@"settings.json";

        

        public string SearchWordsText
        {
            set
            {
                if(value=="")
                    throw new Exception("Invalid data format for SearchWords");
                try { SearchWords = value.Split(',').ToList(); }
                catch { throw new Exception("Invalid data format for SearchWords");}
            }
            get
            {
                StringBuilder sb = new StringBuilder();
                if (SearchWords == null) return sb.ToString();
                for (int i = 0; i < SearchWords.Count; i++)
                {
                    sb.Append(SearchWords[i]);
                    if (i != SearchWords.Count - 1) sb.Append(", ");
                }
                return sb.ToString();
            }
        }

        public string MaxCountOfFollowPerDayText
        {
            set
            {
                try { MaxCountOfFollowPerDay = Convert.ToInt32(value); }
                catch { throw new Exception("Invalid data format for  MaxCountOfFollowPerDay "); }
                if( MaxCountOfFollowPerDay<=0)
                    throw new Exception("Invalid data format for  MaxCountOfFollowPerDay ");
             }
            get { return MaxCountOfFollowPerDay.ToString(); }
        }

        public string MaxCountOfLikePerUserText
        {
            set
            {
                try { MaxCountOfLikePerUser = Convert.ToInt32(value); }
                catch { throw new Exception("Invalid data format for  MaxCountOfLikePerUser");}
                if(MaxCountOfLikePerUser<=0)
                    throw new Exception("Invalid data format for  MaxCountOfFollowPerDay ");
            }
            get { return MaxCountOfLikePerUser.ToString(); }
        }

        public string MaxCountOfLikePerDayText
        {
            set
            {
                try { MaxCountOfLikePerDay = Convert.ToInt32(value); }
                catch { throw new Exception("Invalid data format for  MaxCountOfLikePerDay"); }
                if(MaxCountOfLikePerDay<=0)
                    throw new Exception("Invalid data format for  MaxCountOfLikePerDay");
            }
            get { return MaxCountOfLikePerDay.ToString(); }
        }



        public void SaveSettings()
        {
            string serialize = JsonConvert.SerializeObject(this);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + _settingsFile), serialize);
        }

        public static Settings LoadSettings()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + _settingsFile);
            if (!File.Exists(path))
            {
                GetDefaultSettings().SaveSettings();
            }
            return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
        }

        public static Settings GetDefaultSettings()
        {
            Settings settings = new Settings()
            {
                MaxCountOfLikePerDay = 1000,
                MaxCountOfLikePerUser = 3,
                MaxCountOfFollowPerDay = 200,
                SearchWords = new List<string> {"test1", "test2"}
            };
            return settings;
        }
    }
}
