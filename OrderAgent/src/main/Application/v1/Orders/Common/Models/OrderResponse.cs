using JacksonVeroneze.OrderAgent.Domain.Enums;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Models;

public sealed record OrderResponse(
    Guid Id,
    string AssetTicker,
    OrderSide Side,
    OrderType OrderType,
    OrderStatus Status,
    decimal Quantity,
    decimal Price,
    decimal FilledQuantity,
    decimal RemainingQuantity,
    decimal TotalAmount,
    string? RejectionReason,
    DateTimeOffset CreatedAtUtc,
    DateTimeOffset? UpdatedAtUtc);
