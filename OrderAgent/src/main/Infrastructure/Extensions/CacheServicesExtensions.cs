using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class CacheServicesExtensions
{
    public static IServiceCollection AddCached(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName =
                    $"{appConfiguration.AppName}-" +
                    $"{appConfiguration.AppVersion}";

                options.ConfigurationOptions = new ConfigurationOptions
                {
                    Ssl = false,
                    AbortOnConnectFail = false,
                    EndPoints = { appConfiguration.Cache!.Endpoint! },
                    ClientName = $"{appConfiguration.AppName}-" +
                                 $"{appConfiguration.AppVersion}" +
                                 $"{Guid.NewGuid()}",
                };
            });

            return services;
    }

}
