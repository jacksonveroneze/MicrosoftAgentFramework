namespace JacksonVeroneze.OrderAgent.Agent.Models.Orders.CheckOrdersByTicker;

internal sealed record CheckOrdersByAssetToolResult(
    bool Success,
    string AssetTicker,
    bool HasOrders,
    int OrdersCount,
    string Message);
