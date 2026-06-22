using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JacksonVeroneze.OrderAgent.Application;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services, Assembly assembly)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.RequireExplicitMapping = true;
        config.RequireDestinationMemberSource = true;

        config.Scan(typeof(AssemblyReference).Assembly);
        config.Scan(assembly);

        services.AddSingleton(config);

        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
