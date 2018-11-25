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
        const string postsFile = "hmmposts.json";
        const string postsPath = postsFolder + "/" + postsFile;

        public static string[] posts = new string[500];

        bool hasDownloaded = false;

        public void DownloadPosts(int numberOfPosts)
        {
            if (!hasDownloaded)
            { 
                Console.WriteLine("Downloading " + numberOfPosts + " posts in r/hmmm");
                Reddit r = new Reddit();
                var sub = r.GetSubreddit("hmmm");
                Post[] p = sub.New.Take(numberOfPosts).ToArray();
                for (int i = 0; i < p.Length; i++)
                {
                    posts[i] = p[i].Url.AbsoluteUri;
                }
                hasDownloaded = true;
            }
        }

        public RedditHandler()
        {
            if (!Directory.Exists(postsFolder))
                Directory.CreateDirectory(postsFolder);

            if (File.Exists(postsPath))
            {
                string raw = File.ReadAllText(postsPath);
                string[] data = JsonConvert.DeserializeObject<string[]>(raw);
                if (data.Contains("null"))
                    DownloadPosts(500);
                else
                {
                    hasDownloaded = true;
                    for (int i = 0; i < data.Length; i++)
                    {
                        posts[i] = data[i];
                    }
                }
            }
            else
            {
                DownloadPosts(500);
                string data = JsonConvert.SerializeObject(posts, Formatting.Indented);
                File.WriteAllText(postsPath, data);
            }

        }

        public static string RandomHmmLink()
        {
            Random r = new Random();
            int _r = r.Next(0, posts.Length);
            return posts[_r];
        }

    }
}
