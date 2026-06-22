using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Infrastructure.Configurations;
using ModelContextProtocol.Protocol;

namespace JacksonVeroneze.OrderAgent.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class McpExtensions
{
    public static IServiceCollection AddMcp(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddMcpServer(configureOption =>
            {
                configureOption.ServerInfo = new Implementation
                {
                    Name = appConfiguration.AppName,
                    Version = appConfiguration.AppVersion.ToString(),
                };
            })
            .AddAuthorizationFilters()
            .WithHttpTransport(options => { options.Stateless = true; })
            .WithToolsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
