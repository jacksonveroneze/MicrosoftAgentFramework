using Mapster;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetById;

public class GetByIdOrderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<Domain.Entities.Order, GetByIdOrderResponse>()
            .Map(dest => dest.Data, src => src);
    }
}
