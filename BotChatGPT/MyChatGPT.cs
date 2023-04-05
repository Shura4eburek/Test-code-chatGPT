using Discord;
using Discord.WebSocket;

namespace BotChatGPT
{
    class Program
    {
        private DiscordSocketClient? _client;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            // Создаем клиента Discord
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All,
                LogLevel = LogSeverity.Debug
            });

            // Подписываемся на события
            _client.Log += Log;
            _client.Ready += () => Log(new LogMessage(LogSeverity.Info, "BotChatGPT", "Bot is ready"));
            _client.MessageReceived += MessageHandler;

            // Логинимся в Discord
            const string token = "Discort_token";

            // Запускаем бота
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            // Выводим в консоль сообщения от Discord.NET
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageHandler(SocketMessage messageParam)
        {
            // Не обрабатываем сообщения от ботов
            if (messageParam.Author.IsBot) return;
            // Не обрабатываем сообщения, которые не являются текстовыми
            if (messageParam is not SocketUserMessage message) return;

            

            // Удаляем форматирование текста, чтобы оно не мешало работе GPT-3
            var newText = message.Content
                .Replace("*", "")
                .Replace("_", "")
                .Replace("~", "")
                .Replace("<", "")
                .Replace(">", "");

            string response = string.Empty;

            if (newText.StartsWith("!d2 "))
            {
                newText = newText.Replace("!d2 ", "");
                response = await DALL_E.GetResponse(newText, messageParam.Author.Username); 
            }
            else if (newText.StartsWith("!model"))
            {
                // Обрабатываем команду !model
                response = await statusModel.GetResponse(newText, messageParam.Author.Username); ;
            }
            else
            {
                // Получаем ответ от GPT-3
                response = await ChatGPTService.GetResponse(newText);
            }


            // Отправляем ответ в канал Discord
            var channel = message.Channel as SocketTextChannel;
            await channel!.SendMessageAsync(response);
        }
    }
}