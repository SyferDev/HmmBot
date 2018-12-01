using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiphyDotNet;
using GiphyDotNet.Manager;
using GiphyDotNet.Model.Parameters;
using GiphyDotNet.Tools;

namespace HmmBot.Syf
{
    class GiphyHandler
    {
        static Giphy giphy;

        public GiphyHandler()
        {
            giphy = new Giphy("1mTJGb9rBi7F7tlGQV5KyZb8GhEuMz7z");
        }
        
        public static async Task<string> TranslateGif(string translated)
        {
            var translatedResult = await giphy.TranslateIntoGif(new TranslateParameter()
            {
                Phrase = translated
            });

            string url = translatedResult.Data.BitlyGifUrl;
            Console.WriteLine(url);
            return url;
        }

        public static async Task<string> RandomGifWithSearch(string query)
        {
            var gifResult = await giphy.RandomGif(new RandomParameter() {
                Tag = query
            });

            var url = gifResult.Data.ImageOriginalUrl;
            return url;
            
        }

    }
}
