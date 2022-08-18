using DSharpPlus;

namespace CobwebBot.Handlers
{
    internal class ModsChannelHandler
    {
        public async static void Handle(DiscordClient s, MessageCreateEventArgs e)
        {
            DiscordMember member = (DiscordMember)e.Message.Author;
            if (e.Message.Attachments.Count != 0 || e.Message.Content.Split("https:").Length > 1)
            {
                await member.SendMessageAsync("Please make a thread to discuss mods. If you want to post a mod, make sure there is a link to the mod or the mod file is uploaded.");
            }
        }
    }
}
