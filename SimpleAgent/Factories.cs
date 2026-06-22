using Anthropic;
using Microsoft.Extensions.AI;
using OllamaSharp;
using OpenAI;
using OpenAI.Chat;

namespace SimpleAgent;

public static class Factories
{
    public static IAnthropicClient FactoryAnthopic(string apiKey, string? model = null)
    {
        var agent = new AnthropicClient
        {
            ApiKey = apiKey,
            MaxRetries = 3,
            Timeout = TimeSpan.FromSeconds(30)
        };

        return agent;
    }

    public static ChatClient FactoryOpenAi(string apiKey, string? model = null)
    {
        var agent = new OpenAIClient(apiKey)
            .GetChatClient(model ?? "gpt-4o-mini");

        return agent;
    }

    public static IChatClient FactoryOllama(Uri url, string? defaultModel = null)
    {
        var agent = new OllamaApiClient(url, defaultModel: "yogeshisspl/phi3:latest");

        return agent;
    }
}