using JacksonVeroneze.OrderAgent.Application.Abstractions.UseCases;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetByAsset;

public sealed record GetOrdersByAssetRequest(
    string AssetTicker,
    Guid UserId) : IBaseRequest;
