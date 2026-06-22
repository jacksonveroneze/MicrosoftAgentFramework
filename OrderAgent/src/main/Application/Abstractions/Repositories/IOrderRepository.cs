using JacksonVeroneze.OrderAgent.Domain.Entities;

namespace JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
