using System.Drawing;
using System.Reflection;
using System.Text;
using BotChatGPT.Model;
using Discord;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;

namespace BotChatGPT;

public class DALL_E
{
    private static readonly HttpClient HttpClient = new();
    private const string OpenaiUrl = "https://api.openai.com/v1/images/generations";
    private const string V = "OpenAI API";

    public static async Task<string> GetResponse(string newText, string username)
    {
        // Сериализуем объект в JSON
        var requestJson = JsonConvert.SerializeObject(new
        {
            prompt = newText,
            n = 4,
            size = "1024x1024",
            user = username,
        });

        // Создаем запрос
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(OpenaiUrl),
            Headers = { { "Authorization", $"Bearer {V}" } },
            Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
        };

        // Отправляем запрос и получаем ответ
        var response = await HttpClient.SendAsync(request);
        var responseJson = await response.Content.ReadAsStringAsync();

        // Десериализуем ответ
        var responseObject = JsonConvert.DeserializeObject<Images>(responseJson);

        // Возвращаем первый вариант ответа
        return string.Join('\n', responseObject?.Data?.Select(x => x.Url) ?? new string[] {"Хуй сос"});
    }
}