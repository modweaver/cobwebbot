using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using CobwebBot.Commands;
using dotenv.net;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CobwebBot
{
    class Program
    {
        private static bool devMode = true;
        private static string _buildLoc = Path.Combine(Directory.GetCurrentDirectory() + "./bin/Debug/net6.0/");
        private static string? token;
        static void Main(string[] args)
        {

            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var json = "";
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var cfgjson = JsonConvert.DeserializeObject<ConfigJson>(json);


            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = cfgjson.TOKEN,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MM DD -- hh:mm:ss tt"
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                StringPrefixes = new[] { cfgjson.PREFIX }
            });


            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                }
            };

            discord.ComponentInteractionCreated += async (s, e) =>
            {
                await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                var user = e.User;
                var member = await e.Guild.GetMemberAsync(user.Id);
                DiscordRole role;
                var guildId = e.Guild.Id;


                if (e.Id == "modweaver_role_give") 
                {
                    if (guildId == 843489593319751682) // test server
                    {
                        role = e.Guild.GetRole(990313379153473586);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    } else if (guildId == 952598946511986699) // real server
                    {
                        role = e.Guild.GetRole(990314173416226816);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    }
                } else if (e.Id == "cobwebapi_role_give")
                {
                    if (guildId == 843489593319751682) // test server
                    {
                        role = e.Guild.GetRole(990313429078265877);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    }
                    else if (guildId == 952598946511986699) // real server
                    {
                        role = e.Guild.GetRole(990314228198027274);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    }
                } else if (e.Id == "cwlauncher_role_give")
                {
                    if (guildId == 843489593319751682) // test server
                    {
                        role = e.Guild.GetRole(990313457163337778);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    }
                    else if (guildId == 952598946511986699) // real server
                    {
                        role = e.Guild.GetRole(990314259537870928);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    }
                } else if (e.Id == "webcrawler_role_give")
                {
                    if (guildId == 843489593319751682) // test server
                    {
                        role = e.Guild.GetRole(990313520593788988);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    }
                    else if (guildId == 952598946511986699) // real server
                    {
                        role = e.Guild.GetRole(990314322934784090);
                        var userRoles = member.Roles;
                        if (userRoles.Contains(role))
                        {
                            await member.RevokeRoleAsync(role);
                        }
                        else
                        {
                            await member.GrantRoleAsync(role);
                        }
                    }
                }             
            };
            commands.RegisterCommands<Commands.Utilities>();
            commands.RegisterCommands<Commands.Moderation>();
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }


    }

    public struct ConfigJson
    {
        [JsonProperty("TOKEN")]
        public string TOKEN { get; private set; }

        [JsonProperty("PREFIX")]
        public string PREFIX { get; private set; }
    }
}