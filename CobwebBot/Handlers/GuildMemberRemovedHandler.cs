using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace CobwebBot.Handlers;

public class GuildMemberRemovedHandler
{

    public async static void Handle(DiscordClient s, GuildMemberRemoveEventArgs e)
    {
        if (e.Member.IsBot)
        {
            return;
        }
        var channel = e.Guild.GetChannel(1013164525832392804);
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
            $"{e.Member.DisplayName} has left the server. The server is now at {new_members.Count} members");
    }
}