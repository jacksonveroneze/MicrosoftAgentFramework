using JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetOpen;

public sealed record GetOrdersOpenRequest(
    Guid AccountId,
    Guid UserId) : IBaseRequest;
