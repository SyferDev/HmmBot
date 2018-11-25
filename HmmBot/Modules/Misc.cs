using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Audio;
using HmmBot.RedditHmm;

namespace HmmBot.Modules
{
    /// <summary>
    /// Seifer
    /// </summary>
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("greentext")]
        public async Task Greentext()
        {
            var embed = new EmbedBuilder().
                WithTitle(RedditHandler.RandomGreentext().title).
                WithImageUrl(RedditHandler.RandomGreentext().imgUrl);

            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("what is life")]
        [Alias(new string[] { "what is the meaning of life?", "what is the meaning of life", "what is life", "meaning of life"})]
        public async Task Life()
        {
            var embed = new EmbedBuilder().WithColor(Color.Green).WithTitle("42");
            await Context.Channel.SendMessageAsync("", false, embed);
        }
        

        [Command("time")]
        public async Task TimeZone([Remainder]string country)
        {
            var embed = new EmbedBuilder();
            embed.WithColor(Color.Green);
            embed.WithTitle(TimeRetriever.TimeIn(country));

            await Context.Channel.SendMessageAsync("", false, embed);
        }


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
            embed.WithImageUrl(RedditHandler.RandomHmm());
            
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("game")]
        public async Task SetGame([Remainder]string game)
        {
            await Context.Client.SetGameAsync(game);
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
