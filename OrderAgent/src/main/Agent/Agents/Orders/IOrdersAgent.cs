using JacksonVeroneze.OrderAgent.Agent.Models.Orders.Agent;

namespace JacksonVeroneze.OrderAgent.Agent.Agents.Orders;

public interface IOrdersAgent
{
    Task<OrdersAgentOutput> RunAsync(
        OrdersAgentInput input,
        CancellationToken cancellationToken);
}
