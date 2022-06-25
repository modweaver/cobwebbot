using System.Net.Http.Json;
using System.Text;
using dotenv.net;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CobwebBot
{
    class Program
    {
        private static string token;
        static void Main(string[] args)
        {
            
            MainAsync().GetAwaiter().GetResult(); 
        }

        static async Task MainAsync()
        {
            var json = "";
            using(var fs = File.OpenRead("config.json"))
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
            
            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                }
            };

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