using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Models;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetOpen;

public sealed record GetOrdersOpenResponse(
    OrderResponse[] Orders);
