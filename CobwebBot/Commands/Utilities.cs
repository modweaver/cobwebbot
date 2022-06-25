using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace CobwebBot.Commands
{
    public class Utilities : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Greetings! I am Cobweb Bot");
        }

        [Command("avatar"),
         Description("Displays the avatar of the command runner or user mentioned with this command")]
        public async Task AvatarCommand(CommandContext ctx, DiscordMember mentionedUser)
        {
            await ctx.RespondAsync(mentionedUser != null
                ? mentionedUser.GetGuildAvatarUrl(ImageFormat.Auto, 512)
                : ctx.Member.GetGuildAvatarUrl(ImageFormat.Auto, 512));
        }

        [Command("avatar"),
         Description("Displays the avatar of the command runner or user mentioned with this command")]
        public async Task AvatarCommand(CommandContext ctx)
        {
            await ctx.RespondAsync(ctx.Member.GetGuildAvatarUrl(ImageFormat.Auto, 512));
        }
        [Command("help"), Description("Shows a help page")]
        public async Task HelpCommand(CommandContext ctx) 
        {
            string EmbedDescription = "`help` \n Shows this page. \n \n `avatar` \n Gets the avatar for a user. \n Syntax: avatar [user] \n \n `kick` (Admin Only) \n Kicks a member. \n Syntax: kick <user> \"<reason>\" \n \n `ban` (Admin Only) \n Bans a member. \n Syntax: ban <user> \"<reason>\" \n \n `mute` (Admin Only) \n Mutes a user. \n Syntax: mute <user> <duration> \"<reason>\" \n For duration, the following values are valid: \n <number>h: Hours \n <number>m: Minutes \n <number>s: Seconds"
            DiscordEmbed Embed = new DiscordEmbedBuilder().WithDescription(EmbedDescription).WithTitle("Commands").WithColor(DiscordColor.Green);
            await ctx.RespondAsync(Embed);
        }
    }
}
