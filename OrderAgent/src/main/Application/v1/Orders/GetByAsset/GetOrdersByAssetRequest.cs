using JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

public sealed record GetOrdersByAssetRequest(
    Guid AccountId,
    Guid UserId,
    string Ticker) : IBaseRequest;
