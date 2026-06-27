using JacksonVeroneze.OrderAgent.Domain.Entities;
using Mapster;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetOpen;

public class GetOrdersOpenMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<List<Order>, GetOrdersOpenResponse>()
            .Map(dest => dest.Orders, src => src);
    }
}
