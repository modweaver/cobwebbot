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
using CobwebBot.Handlers;

namespace CobwebBot
{
    class Program
    {
        public static DefaultLogger Logger;
        private static bool devMode = true;
        private static string _buildLoc = Path.Combine(Directory.GetCurrentDirectory() + "./bin/Debug/net6.0/");
        private static string? token;
        public static string[] prefixes = new[] { "!" };
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
            discord.MessageCreated += new CommandExecutor().MessageCreatedAsync;

            /*var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                StringPrefixes = new[] { cfgjson.PREFIX }
            });*/


            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                }
                if (e.Message.Channel.Name == "support")
                {
                    SupportChannelHandler.Handle(s, e);
                }
            };

            discord.ComponentInteractionCreated += (s, e) =>
            {
                InteractionHandler.Handle(s, e);
                return Task.CompletedTask;
            };
            var commands = discord.GetCommandsNext();
            commands.RegisterCommands();
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