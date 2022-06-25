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
    }
}
