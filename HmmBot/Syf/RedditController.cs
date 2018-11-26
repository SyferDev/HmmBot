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
        public const string postsSuffix = "Posts";

        public static List<Post> DownloadSubredditPosts(string subreddit, int amount)
        {

            string path = postsFolder + "/" + subreddit + postsSuffix;
            if (!File.Exists(path))
            {
                Reddit r = new Reddit();
                Subreddit sub = r.GetSubreddit(subreddit);
                List<Post> p = sub.Posts.Take(500).ToList();

                if (!Directory.Exists(postsFolder))
                    Directory.CreateDirectory(postsFolder);
                // Serialize posts to json
                var data = JsonConvert.SerializeObject(p, Formatting.Indented);
                // create file
                File.WriteAllText(path, data);
                return p;
            }
            else
            {

                List<Post> p = JsonConvert.DeserializeObject<List<Post>>(path);
                return p;
            }
        }

    }
}
