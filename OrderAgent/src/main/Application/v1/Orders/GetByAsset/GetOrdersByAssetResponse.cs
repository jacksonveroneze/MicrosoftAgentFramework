namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

public sealed record CheckOrdersByAssetResponse(
    string AssetTicker,
    bool HasOrders,
    int OrdersCount);
