using System.Linq.Expressions;
using JacksonVeroneze.OrderAgent.Application.v1.Orders.Common.Filters;
using JacksonVeroneze.OrderAgent.Domain.Entities;
using JacksonVeroneze.OrderAgent.Infrastructure.Builders.Util;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Builders.Filters;

public class OrderFilterBuilder(
    OrderFilter filter)
{
    public static OrderFilterBuilder Create(
        OrderFilter filter)
    {
        return new OrderFilterBuilder(filter);
    }

    public Expression<Func<Order, bool>> Build()
    {
        FilterBuilder<Order> builder = new();

        builder.And(d => d.AccountId == filter.AccountId);
        builder.And(d => d.UserId == filter.UserId);
        
        if (!string.IsNullOrWhiteSpace(filter.Ticker))
        {
            builder.And(d => EF.Functions
                .ILike(d.Ticker, filter.Ticker));
        }

        if (filter.Side.HasValue)
        {
            builder.And(d => d.Side == filter.Side);
        }

        if (filter.OrderType.HasValue)
        {
            builder.And(d => d.OrderType == filter.OrderType);
        }

        if (filter.Status.HasValue)
        {
            builder.And(d => d.Status == filter.Status);
        }

        return builder.Build();
    }
}
