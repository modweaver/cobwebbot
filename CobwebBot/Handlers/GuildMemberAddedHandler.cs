using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace CobwebBot.Handlers;

public class GuildMemberAddedHandler
{

    public async static void Handle(DiscordClient s, GuildMemberAddEventArgs e)
    {
        var channel = e.Guild.GetDefaultChannel();
        var members_ = e.Guild.Members;
        Dictionary<ulong, DiscordMember?> new_members = new();
        foreach (var member in members_)
        {
            if (!member.Value.IsBot)
            {
                new_members.Add(member.Key, member.Value);
            }
        }
        
        await channel.SendMessageAsync(
            $"Welcome {e.Member.Mention}, you're member #{new_members.Count}! If you're here to download mods, visit <#966085042621251614> - this channel is for posting mods you have made and downloading from. Or you can visit https://modweaver.org, a site created by us. If you need help with anything, feel free to post a message in <#980789224217403422>");
    }
}