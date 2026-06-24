using JacksonVeroneze.OrderAgent.Agent.Models;

namespace JacksonVeroneze.OrderAgent.Agent.Agents;

public interface IOrdersAgent
{
    Task<OrdersAgentResponse> RunAsync(
        OrdersAgentRequest request,
        CancellationToken cancellationToken);
}
