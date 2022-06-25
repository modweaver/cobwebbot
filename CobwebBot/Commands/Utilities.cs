using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace CobwebBot.Commands
{
    public class Utilities : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            
            await ctx.RespondAsync("Greetings! I am Cobweb Bot");
        }
    }
}
