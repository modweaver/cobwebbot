using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace CobwebBot.Commands
{
    public class Moderation : BaseCommandModule
    {
        #region VARIABLES

        #endregion
        #region COMMANDS
        
        #region BAN COMMAND
        [Command("ban")]
        public async Task BanCommand(CommandContext ctx, DiscordMember member, string reason)
        {
            if (!ctx.Member.Permissions.HasPermission(Permissions.BanMembers))
            {
                await ctx.Message.RespondAsync("You do not have permission to ban this user.");
                return;
            }
            string EmbedDescription = "You have been banned from " + ctx.Guild.Name + "\n \n Reason: " + reason;
            DiscordEmbed Embed = new DiscordEmbedBuilder().WithTitle("Banned!").WithDescription(EmbedDescription).WithAuthor("Moderator: " + ctx.Message.Author.Username + "#" + ctx.Message.Author.Discriminator).WithColor(DiscordColor.Red);
            await member.SendMessageAsync(Embed);
            await member.BanAsync(0, reason);
            await ctx.RespondAsync("Banned user " + member.Mention);
        }
        #endregion
        
        #region KICK COMMAND
        [Command("kick")]
        public async Task KickCommand(CommandContext ctx, DiscordMember member, string reason)
        {
            if (!ctx.Member.Permissions.HasPermission(Permissions.KickMembers))
            {
                await ctx.Message.RespondAsync("You do not have permission to kick this user.");
                return;
            }
            string EmbedDescription = "You have been banned from " + ctx.Guild.Name + "\n \n Reason: " + reason;
            DiscordEmbed Embed = new DiscordEmbedBuilder().WithTitle("Kicked!").WithDescription(EmbedDescription).WithAuthor("Moderator: " + ctx.Message.Author.Username + "#" + ctx.Message.Author.Discriminator).WithColor(DiscordColor.Orange);
            await member.SendMessageAsync(Embed);
            await member.RemoveAsync(reason);
            await ctx.RespondAsync("Kicked user " + member.Mention);
        }
        #endregion
        
        #region MUTE COMMAND
        [Command("mute")]
        public async Task MuteCommand(CommandContext ctx, DiscordMember member, string duration, string reason)
        {
            if (!ctx.Member.Permissions.HasPermission(Permissions.ModerateMembers))
            {
                await ctx.Message.RespondAsync("You do not have permission to mute this user.");
                return;
            }
            var tDuration = duration;
            string durationSuffix = "";
            bool isHours = false, isMinutes = false, isSeconds = false;
            if (duration.EndsWith("h"))
            {
                isHours = true;
                duration = duration.Split("h")[0];
                if (duration.Equals("1h"))
                {
                    durationSuffix = "hour";
                }
                else
                {
                    durationSuffix = "hours";
                }
            }
            else if (duration.EndsWith("m"))
            {
                isMinutes = true;
                duration = duration.Split("m")[0];
                durationSuffix = duration.Equals("1m") ? "minute" : "minutes";
            }
            else if (duration.EndsWith("s"))
            {
                isSeconds = true;
                duration = duration.Split("s")[0];
                durationSuffix = duration.Equals("1s") ? "second" : "seconds";
            }
            else
            {
                await ctx.RespondAsync("Invalid duration! See !help for more info!");
                return;
            }
            string EmbedDescription = "You have been muted in " + ctx.Guild.Name + "\n \n Reason: " + reason + "\n \n Duration: " + duration + " " + durationSuffix;
            DiscordEmbed Embed = new DiscordEmbedBuilder().WithTitle("Muted!").WithDescription(EmbedDescription).WithAuthor("Moderator: " + ctx.Message.Author.Username + "#" + ctx.Message.Author.Discriminator).WithColor(DiscordColor.Yellow);
            DateTimeOffset time = new DateTimeOffset(DateTime.Now.AddMinutes(5));
            if (isHours)
            {
                time = new DateTimeOffset(DateTime.Now.AddHours(double.Parse(duration, System.Globalization.CultureInfo.InvariantCulture)));

            }
            else if (isMinutes)
            {
                time = new DateTimeOffset(DateTime.Now.AddMinutes(double.Parse(duration, System.Globalization.CultureInfo.InvariantCulture)));
            }
            else if (isSeconds)
            {
                time = new DateTimeOffset(DateTime.Now.AddSeconds(double.Parse(duration, System.Globalization.CultureInfo.InvariantCulture)));
            }
            else
            {
                tDuration = "5m";
                time = new DateTimeOffset(DateTime.Now.AddMinutes(5));
            }

            await member.SendMessageAsync(Embed);
            await member.TimeoutAsync(time, reason);
            await ctx.Message.RespondAsync("Muted " + member.Mention + " for " + duration + " " + durationSuffix + "!");
        }
        
        #endregion
        
        #region PURGE COMMAND

        [Command("purge"), Aliases("clear")]
        public async Task PurgeCommand(CommandContext ctx, int amountToDelete)
        {
            if (!ctx.Member.Permissions.HasPermission(Permissions.ManageMessages))
            {
                await ctx.RespondAsync("You must have permissions to manage messages.");
                return;
            }
            switch (amountToDelete)
            {
                case <= 0:
                    await ctx.RespondAsync("You must provide an amount of messages to clear");
                    return;
                case >= 101:
                    await ctx.RespondAsync("Amount to delete cannot be greater than 100");
                    return;
            }

            var messagesToDelete = await ctx.Channel.GetMessagesAsync(limit: amountToDelete);
            Console.WriteLine($"[CobwebBot] Deleting {amountToDelete} messages from {ctx.Channel}, Command run by {ctx.Message.Author} | {ctx.Message.Author.Id}");
            await ctx.Channel.DeleteMessagesAsync(messagesToDelete);
        }
        [Command("purge")]
        public async Task PurgeCommand(CommandContext ctx, DiscordMember memberMessagesToPurge, int amountToDelete)
        {
            if (!ctx.Member.Permissions.HasPermission(Permissions.ManageMessages))
            {
                await ctx.RespondAsync("You must have permissions to manage messages.");
                return;
            }

            if (ctx.Message.Author == memberMessagesToPurge)
                amountToDelete += 1;
            switch (amountToDelete)
            {
                case <= 0:
                    await ctx.RespondAsync("You must provide an amount of messages to clear");
                    return;
                case >= 101:
                    await ctx.RespondAsync("Amount to delete cannot be greater than 100");
                    return;
            }

            var messagesGot = await ctx.Channel.GetMessagesAsync(limit: amountToDelete);
            IReadOnlyList<DiscordMessage> messagesToDelete = null;
            foreach (var msg in messagesGot)
            {
                if (msg.Author == memberMessagesToPurge)
                    messagesToDelete.Append(msg);
            }

            await ctx.Channel.DeleteMessagesAsync(messagesToDelete);
            Console.WriteLine($"[CobwebBot] Deleting {amountToDelete} messages from {ctx.Channel}, Command run by {ctx.Message.Author} | {ctx.Message.Author.Id}");
        }

        #endregion
        
        #endregion
    }
}

