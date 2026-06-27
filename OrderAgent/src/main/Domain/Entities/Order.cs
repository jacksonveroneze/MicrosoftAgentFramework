using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.OrderAgent.Domain.Enums;

namespace JacksonVeroneze.OrderAgent.Domain.Entities;

public sealed class Order : Entity
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid AccountId { get; init; }

    public string Ticker { get; init; } = string.Empty;

    public OrderSide Side { get; init; }

    public OrderType OrderType { get; init; }

    public OrderStatus Status { get; init; }

    public decimal Quantity { get; init; }

    public decimal Price { get; init; }

    public decimal FilledQuantity { get; init; }

    public decimal RemainingQuantity { get; init; }

    public decimal AveragePrice { get; init; }

    public decimal TotalAmount { get; init; }

    public string? RejectionReason { get; init; }

    public DateTimeOffset CreatedAtUtc { get; init; }

    public DateTimeOffset? UpdatedAtUtc { get; init; }

    public DateTimeOffset? ExecutedAtUtc { get; init; }

    public DateTimeOffset? CancelledAtUtc { get; init; }

    #region Ctor

    public Order()
    {
    }

    #endregion
}
