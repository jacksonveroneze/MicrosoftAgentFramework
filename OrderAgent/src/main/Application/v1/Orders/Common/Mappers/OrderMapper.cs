using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Models;
using Mapster;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Mappers;

public class OrderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Order, OrderResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.AssetTicker, src => src.AssetTicker)
            .Map(dest => dest.Side, src => src.Side)
            .Map(dest => dest.OrderType, src => src.OrderType)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.FilledQuantity, src => src.FilledQuantity)
            .Map(dest => dest.RemainingQuantity, src => src.RemainingQuantity)
            .Map(dest => dest.TotalAmount, src => src.TotalAmount)
            .Map(dest => dest.RejectionReason, src => src.RejectionReason)
            .Map(dest => dest.CreatedAtUtc, src => src.CreatedAtUtc)
            .Map(dest => dest.UpdatedAtUtc, src => src.UpdatedAtUtc);
    }
}
