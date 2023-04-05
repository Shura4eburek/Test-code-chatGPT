using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace BotChatGPT
{
    public class statusModel
    {
        private DiscordSocketClient _client;
        private CommandService _commands;

        public statusModel()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.MessageReceived += HandleCommandAsync;
        }

        private Task HandleCommandAsync(SocketMessage arg)
        {
            throw new NotImplementedException();
        }

        public static async Task<string> GetResponse(string message, string username)
        {
            if (message.StartsWith("!model"))
            {
                string text = "Chatbot status: ";
                var response = "Модель чата: " + ChatGPTService.ChatModel + "\nМодель генерации картинок: Стандартная";
                return response;
            }
            return string.Empty;
        }

        public async Task RunAsync()
        {
            string token = "Your bot token here";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }
    }
}