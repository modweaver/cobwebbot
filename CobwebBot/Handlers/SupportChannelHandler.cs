using DSharpPlus.EventArgs;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;

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
            DiscordMember? member = null;
            bool tg = false;
            try
            {
                member = await e.Guild.GetMemberAsync(userId);
                tg = false;
            }
            catch (NotFoundException)
            {
                tg = true;
            }

            var i = 0;
            if (e.Message.Author == s.CurrentUser)
            {
                return;
            }

            if (messageContent.Split("Title: ").Length < 2)
            {
                if (!tg)
                {
                    await member.SendMessageAsync("Please use the requested format for your message!");
                }

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
                var messagesList = await channel.GetMessagesAsync(100);
                messagesList = messagesList.ToList();
                foreach (var message in messagesList)
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
                    var thread = await channel.CreateThreadAsync(e.Message, name, AutoArchiveDuration.Day);
                    var message = await thread.SendMessageAsync("Hang on, let me grab the admins...");
                    await message.ModifyAsync($"<@&966075968647209060> \nWelcome to your new thread {member.Mention}! \nI've grabbed the moderators so they can make sure nothing bad can happen in this thread. Someone should be around to help you soon :)");
                }
            }
        }
    }
}
