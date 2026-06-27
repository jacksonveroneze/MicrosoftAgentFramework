using JacksonVeroneze.OrderAgent.Domain.Entities;

namespace JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<int> CountByTickerAsync(
        Guid accountId,
        Guid userId,
        string ticker,
        CancellationToken cancellationToken);
}
