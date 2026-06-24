using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Services;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.GetById;
using JacksonVeroneze.OrderAgent.Infrastructure.Repositories.Order;
using JacksonVeroneze.OrderAgent.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IGetByIdOrderUseCase, GetByIdOrderUseCase>();
        services.AddScoped<IGetOrdersByAssetUseCase, GetOrdersByAssetUseCase>();

        return services;
    }
}
