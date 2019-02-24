using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TumblerBot_Selenium_Test
{
    [JsonObject(MemberSerialization.OptIn)]
    class Settings
    {
        [JsonProperty]
        public int MaxCountOfLikePerDay;

        [JsonProperty]
        public int MaxCountOfLikePerUser;

        [JsonProperty]
        public int MaxCountOfFollowPerDay;

        [JsonProperty]
        public List<string> searchwords;

        private string _settingsFile="settings.json";

        public void SaveSettings()
        {
            string serialize = JsonConvert.SerializeObject(this);
            File.WriteAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory()+_settingsFile), serialize);
        }

        public Settings LoadSettings()
        {
            string settings = File.ReadAllText(Path.Combine(System.IO.Directory.GetCurrentDirectory() + _settingsFile));
            return JsonConvert.DeserializeObject<Settings>(settings);
        }
    }
}
