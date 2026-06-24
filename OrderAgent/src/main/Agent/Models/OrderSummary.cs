namespace JacksonVeroneze.OrderAgent.Agent.Models;

public sealed record CheckOrdersByAssetToolResult(
    bool Success,
    string AssetTicker,
    bool HasOrders,
    int OrdersCount,
    string Message);
