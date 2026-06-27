using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Domain.Enums;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Filters;

[ExcludeFromCodeCoverage]
public sealed record OrderFilter
{
    public Guid AccountId { get; init; }
    
    public Guid UserId { get; init; }

    public string? Ticker { get; init; }

    public OrderSide? Side { get; init; }

    public OrderType? OrderType { get; init; }

    public OrderStatus? Status { get; init; }
}
