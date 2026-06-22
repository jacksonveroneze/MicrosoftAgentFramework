using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record CacheConfiguration
{
    public string? Endpoint { get; init; }
}
