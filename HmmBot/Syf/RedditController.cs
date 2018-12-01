using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

using RedditSharp;
using RedditSharp.Extensions;
using RedditSharp.Multi;
using RedditSharp.Things;



namespace HmmBot.Syf
{

    public class RedditController
    {
        public const string postsFolder = "Posts";
        public const string postsSuffix = "Posts.json";

        public static List<Meme> DownloadSubredditPosts(string subreddit, int amount)
        {
            string path = postsFolder + "/" + subreddit + postsSuffix;
            if (!File.Exists(path))
            {
                Console.WriteLine("Downloading to " + path);
                Reddit r = new Reddit();
                Subreddit sub = r.GetSubreddit(subreddit);
                Post[] p = sub.New.Take(500).ToArray();
                List<Meme> m = new List<Meme>();

                for (int i = 0; i < p.Length; i++)
                {
                    m.Add(new Meme(p[i].Title, p[i].Url.AbsoluteUri));
                }

                if (!Directory.Exists(postsFolder))
                    Directory.CreateDirectory(postsFolder);
                var data = JsonConvert.SerializeObject(m, Formatting.Indented);
                File.WriteAllText(path, data);
                return m;
            }
            else
            {
                List<Meme> m = new List<Meme>();
                m = JsonConvert.DeserializeObject<List<Meme>>(File.ReadAllText(path));
                return m;
            }
        }

        public static Meme RandomSubredditPost(string subreddit)
        {
            List<Meme> m = DownloadSubredditPosts(subreddit, 500);
            int r = new Random().Next(0, m.Count);
            return m[r];
        }

    }
}
