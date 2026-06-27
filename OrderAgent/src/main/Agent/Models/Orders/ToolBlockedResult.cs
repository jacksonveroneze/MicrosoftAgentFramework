namespace JacksonVeroneze.OrderAgent.Agent.Models.Orders;

internal sealed record ToolBlockedResult(
    bool Success,
    string Code,
    string Message);
