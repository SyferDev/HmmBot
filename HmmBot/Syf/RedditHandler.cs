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
using HmmBot.Syf;

namespace HmmBot.RedditHmm
{

    public class RedditHandler
    {
        public static Post RandomSubredditPost(string subreddit, int amount)
        {
            List<Post> m = RedditController.DownloadSubredditPosts(subreddit, amount);
            int r = new Random().Next(0, amount);
            return m[r];
        }

    }
}
