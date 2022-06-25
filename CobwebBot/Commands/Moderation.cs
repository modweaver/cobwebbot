using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace CobwebBot.Commands
{
    public class Moderation : BaseCommandModule
    {
        [Command("ban")]
        public async Task BanCommand(CommandContext ctx, DiscordMember member, string reason)
        {
            string EmbedDescription = "You have been banned from " + ctx.Guild.Name + "\n \n Reason: " + reason;
            DiscordEmbed Embed = new DiscordEmbedBuilder().WithTitle("Banned!").WithDescription(EmbedDescription).WithAuthor("Moderator: " + ctx.Message.Author.Username + "#" + ctx.Message.Author.Discriminator).WithColor(DiscordColor.Red);
            await member.SendMessageAsync(Embed);
            await member.BanAsync(0, reason);
            await ctx.RespondAsync("Banned user " + member.Mention);
        }

        [Command("kick")]
        public async Task KickCommand(CommandContext ctx, DiscordMember member, string reason)
        {
            string EmbedDescription = "You have been banned from " + ctx.Guild.Name + "\n \n Reason: " + reason;
            DiscordEmbed Embed = new DiscordEmbedBuilder().WithTitle("Kicked!").WithDescription(EmbedDescription).WithAuthor("Moderator: " + ctx.Message.Author.Username + "#" + ctx.Message.Author.Discriminator).WithColor(DiscordColor.Orange);
            await member.SendMessageAsync(Embed);
            await member.RemoveAsync(reason);
            await ctx.RespondAsync("Kicked user " + member.Mention);
        }
        
        [Command("mute")]   
        public async Task MuteCommand(CommandContext ctx, DiscordMember member, string duration, string reason)
        {
            var tDuration = duration;
            string durationSuffix = "";
            bool isHours = false, isMinutes = false, isSeconds = false;
            if (duration.EndsWith("h"))
            {
                isHours = true;
                duration = duration.Split("h")[0];
                if (duration.Equals("1h"))
                {
                    durationSuffix = "hour";
                }
                else
                {
                    durationSuffix = "hours";
                }
            }
            else if (duration.EndsWith("m"))
            {
                isMinutes = true;
                duration = duration.Split("m")[0];
                durationSuffix = duration.Equals("1m") ? "minute" : "minutes";
            }
            else if (duration.EndsWith("s"))
            {
                isSeconds = true;
                duration = duration.Split("s")[0];
                durationSuffix = duration.Equals("1s") ? "second" : "seconds";
            }
            else
            {
                await ctx.RespondAsync("Invalid duration! See !help for more info!");
                return;
            }
            string EmbedDescription = "You have been muted in " + ctx.Guild.Name + "\n \n Reason: " + reason + "\n \n Duration: " + duration + " " + durationSuffix;
            DiscordEmbed Embed = new DiscordEmbedBuilder().WithTitle("Muted!").WithDescription(EmbedDescription).WithAuthor("Moderator: " + ctx.Message.Author.Username + "#" + ctx.Message.Author.Discriminator).WithColor(DiscordColor.Yellow);
            DateTimeOffset time = new DateTimeOffset(DateTime.Now.AddMinutes(5));
            if (isHours)
            {
                time = new DateTimeOffset(DateTime.Now.AddHours(double.Parse(duration, System.Globalization.CultureInfo.InvariantCulture)));

            }
            else if (isMinutes)
            {
                time = new DateTimeOffset(DateTime.Now.AddMinutes(double.Parse(duration, System.Globalization.CultureInfo.InvariantCulture)));
            }
            else if (isSeconds)
            {
                time = new DateTimeOffset(DateTime.Now.AddSeconds(double.Parse(duration, System.Globalization.CultureInfo.InvariantCulture)));
            }
            else
            {
                tDuration = "5m";
                time = new DateTimeOffset(DateTime.Now.AddMinutes(5));
            }

            await member.SendMessageAsync(Embed);
            await member.TimeoutAsync(time, reason);
            await ctx.Message.RespondAsync("Muted " + member.Mention + " for " + duration + " " + durationSuffix + "!");
        }
    }
}

