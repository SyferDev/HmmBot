using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using HmmBot.RedditHmm;

namespace HmmBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("hmm?")]
        [Alias(new string[]
        {
            "quote"
        })]
        public async Task Hmm()
        {

            var embed = new EmbedBuilder();
            embed.WithTitle(Utils.GetRandomHmm());
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("hmm")]
        [Alias(new string[]
        {
            "meme",
            "post"
        })]
        public async Task HmmMeme()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("hmmm :thinking:");
            embed.WithImageUrl(RedditHandler.RandomHmmLink());
            
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("echo")]
        public async Task Echo([Remainder]string command)
        {
            await Context.Channel.SendMessageAsync(command);
        }

        [Command("urgay")]
        public async Task NoU()
        {
            await Context.Channel.SendMessageAsync("no u " + Context.User.Mention);
        }

    }
}
