using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Models;

namespace JacksonVeroneze.OrderAgent.Agent.Models.Orders.GetOpenOrders;

internal sealed record GetOpenOrdersToolResult(
    bool Success,
    int OrdersCount,
    OrderResponse[] Orders,
    string Message);
