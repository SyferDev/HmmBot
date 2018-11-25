using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using RedditSharp;
using RedditSharp.Multi;
using RedditSharp.Things;
using RedditSharp.Extensions;


namespace HmmBot.RedditHmm
{

    public class RedditHandler
    {
        const string postsFolder = "Resources";
        const string hmmmPosts = "hmmposts.json";
        const string postsPath = postsFolder + "/" + hmmmPosts;

        const string gtFolder = "Resources";
        const string gtPosts = "gtPosts.json";
        const string gtPath = gtFolder + "/" + gtPosts;

        public static string[] hmmPosts = new string[500];
        public static Greentext[] greentexts = new Greentext[500];

        bool hasDownloadedHmm = false, hasDownloadedGreenText = false;

        public RedditHandler()
        {
            InitializeHmmm();
            InitializeGreentext();
        }

        public void DownloadHmmm(int numberOfPosts)
        {
            if (!hasDownloadedHmm)
            {
                Console.WriteLine("Downloading " + numberOfPosts + " posts in r/hmmm");
                Reddit r = new Reddit();
                var sub = r.GetSubreddit("hmmm");
                Post[] p = sub.New.Take(numberOfPosts).ToArray();
                for (int i = 0; i < p.Length; i++)
                {
                    hmmPosts[i] = p[i].Url.AbsoluteUri;
                }
                hasDownloadedHmm = true;
            }
        }

        public void DownloadGreentext(int numberOfPosts)
        {

            if (!hasDownloadedGreenText)
            {
                Console.WriteLine("Downloading " + numberOfPosts + " posts in r/greentext");
                Reddit r = new Reddit();
                var sub = r.GetSubreddit("greentext");
                Post[] p = sub.New.Take(numberOfPosts).ToArray();
                for (int i = 0; i < p.Length; i++)
                {
                    greentexts[i] = new Greentext(p[i].Title, p[i].Url.AbsoluteUri);
                }
                hasDownloadedGreenText = true;
            }

        }

        void InitializeHmmm()
        {
            if (!Directory.Exists(postsFolder))
                Directory.CreateDirectory(postsFolder);

            if (File.Exists(postsPath))
            {
                string raw = File.ReadAllText(postsPath);
                string[] data = JsonConvert.DeserializeObject<string[]>(raw);
                if (data.Contains("null"))
                    DownloadHmmm(500);
                else
                {
                    hasDownloadedHmm = true;
                    for (int i = 0; i < data.Length; i++)
                    {
                        hmmPosts[i] = data[i];
                    }
                }
            }
            else
            {
                DownloadHmmm(500);
                string data = JsonConvert.SerializeObject(hmmPosts, Formatting.Indented);
                File.WriteAllText(postsPath, data);
            }
        }

        void InitializeGreentext()
        {
            if (!Directory.Exists(gtFolder))
                Directory.CreateDirectory(gtFolder);

            if (File.Exists(gtPath))
            {
                string raw = File.ReadAllText(gtPath);
                List<Greentext> data = JsonConvert.DeserializeObject<List<Greentext>>(raw);
                if (greentexts.Contains(new Greentext("null", "null")))
                {
                    DownloadGreentext(500);
                    hasDownloadedGreenText = true;
                    for (int i = 0; i < greentexts.Length; i++)
                    {
                        greentexts[i] = data[i];
                    }
                }
            }
            else
            {
                DownloadGreentext(500);
                string data = JsonConvert.SerializeObject(greentexts, Formatting.Indented);
                File.WriteAllText(gtPath, data);
            }
        }

        public static string RandomHmm()
        {
            Random r = new Random();
            int _r =
                r.Next(0, hmmPosts.Length);
            return hmmPosts[_r];
        }

        public static Greentext RandomGreentext()
        {
            Random r = new Random();
            int _r = r.Next(0, greentexts.Length);
            Greentext text = greentexts[_r];
            return text;
        }

    }

    public struct GreentextModel
    {
    }
    
    public struct Greentext
    {
        public string title;
        public string imgUrl;

        public Greentext(string title, string url)
        {
            this.title = title;
            imgUrl = url;
        }
    }
}
