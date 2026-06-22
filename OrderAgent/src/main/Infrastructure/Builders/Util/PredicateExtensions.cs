using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Builders.Util;

[ExcludeFromCodeCoverage]
public static class PredicateExtensions
{
    public static Expression<Func<TType, bool>> And<TType>(
        this Expression<Func<TType, bool>> a,
        Expression<Func<TType, bool>> b)
    {
        ArgumentNullException.ThrowIfNull(a);
        ArgumentNullException.ThrowIfNull(b);

        var parameter = a.Parameters[0];

        SubstExpressionVisitor visitor =
            new() { Subst = { [b.Parameters[0]] = parameter } };

        var body = Expression
            .AndAlso(a.Body, visitor.Visit(b.Body));

        return Expression
            .Lambda<Func<TType, bool>>(body, parameter);
    }

    public static Expression<Func<TType, bool>> Or<TType>(
        this Expression<Func<TType, bool>> a,
        Expression<Func<TType, bool>> b)
    {
        ArgumentNullException.ThrowIfNull(a);
        ArgumentNullException.ThrowIfNull(b);

        var parameter = a.Parameters[0];

        SubstExpressionVisitor visitor =
            new() { Subst = { [b.Parameters[0]] = parameter } };

        var body = Expression
            .Or(a.Body, visitor.Visit(b.Body));

        return Expression
            .Lambda<Func<TType, bool>>(body, parameter);
    }
}
