using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Agent.Agents.Orders;
using JacksonVeroneze.OrderAgent.Agent.Middleware;
using JacksonVeroneze.OrderAgent.Agent.Tools.Orders;
using Microsoft.Agents.AI;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.OrderAgent.Agent.Extensions;

[ExcludeFromCodeCoverage]
public static class AgentExtensions
{
    public static IServiceCollection AddOrdersAgent(
        this IServiceCollection services)
    {
        services.AddScoped<CheckOrdersByTickerTool>();
        services.AddScoped<GetOpenOrdersTool>();
        
        services.AddScoped<OrdersAgentBuilder>();
        
        services.AddScoped<IOrdersAgent, OrdersAgent>();

        services.AddScoped<AIAgent>(sp => sp
            .GetRequiredService<OrdersAgentBuilder>()
            .Build());

        services.AddSingleton<AllowedToolsMiddleware>(_ =>
        {
            string[] allowedTools = [
                CheckOrdersByTickerTool.ToolName,
                GetOpenOrdersTool.ToolName,
            ];
            
            var readSet = new HashSet<string>(
                allowedTools, StringComparer.OrdinalIgnoreCase);

            return new AllowedToolsMiddleware(readSet);
        });
        
        return services;
    }
}
