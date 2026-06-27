using JacksonVeroneze.OrderAgent.Agent.Models.Orders;

namespace JacksonVeroneze.OrderAgent.Agent.Agents.Orders;

public interface IOrdersAgent
{
    Task<OrdersAgentOutput> RunAsync(
        OrdersAgentInput input,
        CancellationToken cancellationToken);
}
