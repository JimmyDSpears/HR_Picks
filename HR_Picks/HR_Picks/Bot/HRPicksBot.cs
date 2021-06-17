using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using HR_Picks.Commands;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HR_Picks.Bot
{
    public class HRPicksBot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public HRPicksBot()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("ClientConfig.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);

            Client.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<PickCommands>();
        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        public async Task RunAsync()
        {
            await Client.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
