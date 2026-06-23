using JacksonVeroneze.NET.Result;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;
using JacksonVeroneze.OrderAgent.Domain.Errors;
using MapsterMapper;

namespace JacksonVeroneze.OrderAgent.Application.v1.Orders.GetById;

public class GetByIdOrderUseCase(
    IMapper mapper,
    IOrderRepository repository) : IGetByIdOrderUseCase
{
    public async Task<Result<GetByIdOrderResponse>> ExecuteAsync(
        GetByIdOrderRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = await repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<GetByIdOrderResponse>
                .FromNotFound(OrderErrors.OrderError.NotFound);
        }

        var response = mapper.Map<Domain.Entities.Order,
            GetByIdOrderResponse>(entity);

        return Result<GetByIdOrderResponse>
            .WithSuccess(response);
    }
}
