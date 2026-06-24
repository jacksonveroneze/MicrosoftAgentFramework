using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.OrderAgent.Application.Abstractions.Repositories;
using JacksonVeroneze.OrderAgent.Infrastructure.Contexts;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Repositories.Order;

[ExcludeFromCodeCoverage]
public class OrderRepository(
    IEfCoreRepository<Domain.Entities.Order, DefaultDbContext> efRepository)
    : IOrderRepository
{
    public async Task<Domain.Entities.Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await efRepository.GetByIdAsync(
            order => order.Id == id,
            cancellationToken);

        return result;
    }

    public async Task<int> CountByTickerAsync(
        string ticker,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await efRepository.CountAsync(
            order => order.AssetTicker == ticker
                     && order.UserId == userId,
            cancellationToken);

        return (int)result;
    }
}
