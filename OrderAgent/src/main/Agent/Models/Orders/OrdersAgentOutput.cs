namespace JacksonVeroneze.OrderAgent.Agent.Models.Orders;

public sealed record OrdersAgentOutput(
    string Message,
    int MessageCount,
    IReadOnlyCollection<string> Messages,
    string? RawText);
