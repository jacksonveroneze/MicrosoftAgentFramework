namespace JacksonVeroneze.OrderAgent.Agent.Models;

internal sealed record ToolBlockedResult(
    bool Success,
    string Code,
    string Message);
