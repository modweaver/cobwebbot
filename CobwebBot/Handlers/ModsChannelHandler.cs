using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;

namespace CobwebBot.Handlers
{
    internal class ModsChannelHandler
    {
        public async static void Handle(DiscordClient s, MessageCreateEventArgs e)
        
        {  
            try {
            if (e.Message.Author.IsBot) return;
            //await e.Message.Channel.SendMessageAsync(e.Message.Attachments.Count.ToString());
            //await e.Message.Channel.SendMessageAsync(e.Message.Content.Split("https:").Length.ToString());
            DiscordMember member = (DiscordMember)e.Message.Author;
            if (!e.Message.Content.Contains("https://git")) {
                    await e.Message.DeleteAsync();
                    await member.SendMessageAsync("Please attach a link to the git repository for your mod.");
                    return;
             }
            if (e.Message.Attachments.Count == 0 && !e.Message.Content.Contains("https:"))
            {
                
                await member.SendMessageAsync("Please make a thread to discuss mods. If you want to post a mod, make sure there is a link to the mod or the mod file is uploaded.");
                await e.Message.DeleteAsync();
            }
            }
            catch (NotFoundException ex)
            {
                return;
            }
        }
    }
}
