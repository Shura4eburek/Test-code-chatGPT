using System.Reflection;
using System.Text;
using BotChatGPT.Model;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;

namespace BotChatGPT;

public class ChatGPTService
{
    private static readonly HttpClient HttpClient = new();
    private const string OpenaiUrl = "https://api.openai.com/v1/completions";
    private const string V = "OpenAI API";
    public static string ChatModel => chatModel;
    private const string chatModel = "text-davinci-003";
    public static async Task<string> GetResponse(string newText)
    {
        // Сериализуем объект в JSON
        var requestJson = JsonConvert.SerializeObject(new
        {
            prompt = newText,
            model = chatModel,
            max_tokens = 500,
            temperature = 0.7,
            top_p = 1,
            frequency_penalty = 0,
            presence_penalty = 0
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
        var responseObject = JsonConvert.DeserializeObject<OpenAIResponse>(responseJson);

        // Возвращаем первый вариант ответа
        return responseObject?.Choices?.FirstOrDefault()?.Text ?? "Empty response";
    }
}

