using DSharpPlus;
using DSharpPlus.EventArgs;

namespace CobwebBot.Handlers;

public class GuildMemberAddedHandler
{

    public async static void Handle(DiscordClient s, GuildMemberAddEventArgs e)
    {
        var channel = e.Guild.GetDefaultChannel();
        await channel.SendMessageAsync(
            $"Welcome {e.Member.Mention}! If you're here to download mods, visit <#966085042621251614> - this channel is for posting mods you have made and downloading from. Or you can visit https://modweaver.org, a site created by us. If you need help with anything, feel free to post a message in <#980789224217403422>");
    }
}