using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Models;
using Mapster;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Mappers;

public class OrderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Order, OrderResponse>()
            .Map(dest => dest.Id, src => src.Id);
    }
}
