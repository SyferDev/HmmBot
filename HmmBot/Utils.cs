using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

namespace HmmBot
{
    class Utils
    {
        private static Dictionary<string, string> alerts;

        static Utils()
        {
            string rawJson = File.ReadAllText("SystemLang/alerts.json");
            var data = JsonConvert.DeserializeObject<dynamic>(rawJson);
            alerts = data.ToObject<Dictionary<string, string>>();
        }   

        public static string GetAlert(string key)
        {
            if (alerts.ContainsKey(key))
                return alerts[key];
            return "";
        }

        public static string GetRandomHmm()
        {
            string rawJson = File.ReadAllText("SystemLang/hmm.json");
            string[] hmms = JsonConvert.DeserializeObject<string[]>(rawJson);
            for (int i = 0; i < hmms.Length; i++)
            {
                Random r = new Random();
                int hmmI = r.Next(0, hmms.Length);
                string randomHmm = hmms[hmmI];
                return randomHmm;
            }
            return "";

        }

        
    }
}
