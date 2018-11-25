using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace HmmBot
{
    class TimeRetriever
    {

        public static string TimeZoneList()
        {
            string allTimeZones = "";
            var raw = File.ReadAllText("SystemLang/timezones.json");
            string[] converted = JsonConvert.DeserializeObject<string[]>(raw);
            allTimeZones = string.Join(" ", converted);
            return allTimeZones;
        }

        public static string TimeIn(string country)
        {
            string concatTime = country + " Standard Time";
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(concatTime);
            var time = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);

            DateTime timeOfDay = time;
            string t = string.Format("{0}:{1}", timeOfDay.Hour, timeOfDay.Minute);
            string newTime = string.Format("Time in {0} is {1}", timeZone.Id, timeOfDay.ToString());
            return newTime;
        }

    }
}
