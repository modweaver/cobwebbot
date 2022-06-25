using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus;
using DSharpPlus.Entities;

namespace CobwebBot.Handlers
{
    internal class SupportChannelHandler
    {
        public async static void Handle(DiscordClient s, MessageCreateEventArgs e)
        {
            var test = false;
            var found = false;
            var messageContent = e.Message.Content;
            var userId = e.Message.Author.Id;
            var member = await e.Guild.GetMemberAsync(userId);
            var i = 0;
            if (e.Message.Author == s.CurrentUser)
            {
                return;
            }
            if (messageContent.Split("Title: ").Length < 2)
            {
                await member.SendMessageAsync("Please use the requested format for your message!");
                await e.Message.DeleteAsync();
                return;
            }
            else
            {
                var title = messageContent.Split("Title: ")[1].Split("\n")[0];
                var username = e.Message.Author.Username;
                var channel = e.Message.Channel;
                var name = "[" + username + "] " + title;
                var formatMessage = "Please use this format for your message:\nTitle:\nDescription:";
                var messages_list = await channel.GetMessagesAsync(100);
                messages_list = messages_list.ToList();
                foreach (var message in messages_list)
                {
                    if (message.Author == s.CurrentUser)
                    {
                        found = true;
                        await message.DeleteAsync();
                        if (!test)
                        { 
                            var smessage = await channel.SendMessageAsync(formatMessage);
                            await smessage.PinAsync();
                            test = true;

                        }
                    }
                }
                if (!found) 
                {
                    var smessage = await channel.SendMessageAsync(formatMessage);
                    await smessage.PinAsync();
                }
                if (i == 0)
                {
                    await channel.CreateThreadAsync(e.Message, name, AutoArchiveDuration.Day);
                }
                i = 1;
                test = false;

            }
        }
    }
}
