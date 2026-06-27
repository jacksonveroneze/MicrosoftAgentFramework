using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Infrastructure.Configurations;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace JacksonVeroneze.OrderAgent.Api.Extensions;

[ExcludeFromCodeCoverage]
internal static class OllamaChatClientExtensions
{
    public static IServiceCollection AddOllamaChatClient(
        this IServiceCollection services,
        AppConfiguration appConfiguration,
        IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        OllamaConfiguration ollamaConfiguration =
            appConfiguration.Ai?.Ollama ??
            throw new InvalidOperationException("AI Ollama configuration was not found.");

        if (!Uri.TryCreate(ollamaConfiguration.Endpoint, UriKind.Absolute, out Uri? endpoint))
        {
            throw new InvalidOperationException("AI Ollama endpoint must be a valid absolute URI.");
        }
        
        if (string.IsNullOrWhiteSpace(ollamaConfiguration.Model))
        {
            throw new InvalidOperationException("AI Ollama model must be configured.");
        }

        services.AddSingleton<IChatClient>(_ =>
        {
            IChatClient chatClient = new OllamaApiClient(
                endpoint, ollamaConfiguration.Model);

            return chatClient
                .AsBuilder()
                .UseOpenTelemetry(
                    sourceName: "orders-agent",
                    configure: cfg =>
                    {
                        cfg.EnableSensitiveData = hostEnvironment.IsDevelopment();
                    })
                .Build();
        });

        return services;
    }
}
