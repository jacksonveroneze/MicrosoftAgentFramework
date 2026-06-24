using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Agent.Agents;
using JacksonVeroneze.OrderAgent.Agent.Tools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.OrderAgent.Agent.Configuration;

[ExcludeFromCodeCoverage]
public static class AgentExtensions
{
    public static IServiceCollection AddOrdersAgent(this IServiceCollection services)
    {
        services.AddScoped<CheckOrdersByAssetTool>();
        services.AddScoped<OrdersAgentBuilder>();
        
        services.AddScoped<IOrdersAgent, OrdersAgent>();

        services.AddScoped<AIAgent>(provider => provider
            .GetRequiredService<OrdersAgentBuilder>()
            .Build());
        
        return services;
    }
}
