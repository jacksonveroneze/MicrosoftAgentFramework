using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Infrastructure.Configurations;
using JacksonVeroneze.NET.Logging.Configuration;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;

namespace JacksonVeroneze.OrderAgent.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtensions
{
    public static WebApplicationBuilder AddLogger(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        LoggingConfiguration conf = new()
        {
            ApplicationName = appConfiguration
                .Application!.Name,

            ApplicationVersion = appConfiguration
                .Application!.Version!.ToString()
        };

        builder.Host.UseSerilog((hostingContext,
            services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .ReadFrom.Services(services)
                .ConfigureEnrich(conf);
        });

        return builder;
    }

    private static void ConfigureEnrich(
        this LoggerConfiguration loggerConfiguration,
        LoggingConfiguration optionsConfig)
    {
        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithExceptionDetails()
            .Enrich.WithEnvironmentName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.WithSpan()
            .Enrich.WithProperty("ApplicationName", optionsConfig.ApplicationName!)
            .Enrich.WithProperty("ApplicationVersion", optionsConfig.ApplicationVersion!);
    }
}
