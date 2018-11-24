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

        public void InitializeReddit(int numberOfPosts)
        {
            if (posts != null)
            {
                Console.WriteLine("Downloading " + numberOfPosts + " posts in r/hmmm");
                Reddit r = new Reddit();
                var sub = r.GetSubreddit("hmmm");
                Post[] p = sub.New.Take(numberOfPosts).ToArray();
                for (int i = 0; i < p.Length; i++)
                {
                    posts[i] = p[i].Url.AbsoluteUri;
                }
            }
        }

        public RedditHandler()
        {
            InitializeReddit(500);
            if (!Directory.Exists(postsFolder))
                Directory.CreateDirectory(postsFolder);

            if (!File.Exists(postsPath))
            {
                string json = JsonConvert.SerializeObject(posts, Formatting.Indented);
                File.WriteAllText(postsPath, json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(posts, Formatting.Indented);
                File.WriteAllText(postsPath, json);
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
