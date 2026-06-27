namespace JacksonVeroneze.OrderAgent.Agent.Models.Orders;

internal sealed record CheckOrdersByAssetToolResult(
    bool Success,
    string AssetTicker,
    bool HasOrders,
    int OrdersCount,
    string Message);
