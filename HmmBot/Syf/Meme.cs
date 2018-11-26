using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmmBot.Syf
{
    public struct Meme
    {

        public string title;
        public string imgUrl;

        public Meme(string title, string url)
        {
            this.title = title;
            imgUrl = url;
        }

    }
}
