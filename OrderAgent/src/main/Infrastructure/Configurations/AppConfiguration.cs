using System.Diagnostics.CodeAnalysis;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    public bool Type { get; set; }
    
    public AppInfoConfiguration? Application { get; init; }

    public AuthConfiguration? Auth { get; init; }
    
    public CacheConfiguration? Cache { get; init; }

    public DatabaseConfiguration? Database { get; init; }
    
    public AiConfiguration? Ai { get; init; }
    
    public string AppName =>
        Application!.Name!;

    public Version AppVersion =>
        Application!.Version!;
}
