using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AiConfiguration
{
    public OllamaConfiguration? Ollama { get; init; }
}

[ExcludeFromCodeCoverage]
public sealed record OllamaConfiguration
{
    public string? Endpoint { get; init; }

    public string? Model { get; init; }
}
