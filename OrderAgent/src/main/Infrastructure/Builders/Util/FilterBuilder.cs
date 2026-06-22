using System.Linq.Expressions;

namespace JacksonVeroneze.OrderAgent.Infrastructure.Builders.Util;

public class FilterBuilder<T>
{
    private Expression<Func<T, bool>> _expression = x => true;

    public FilterBuilder<T> And(
        Expression<Func<T, bool>> newExpression)
    {
        _expression = _expression.And(newExpression);

        return this;
    }

    public FilterBuilder<T> Or(
        Expression<Func<T, bool>> newExpression)
    {
        _expression = _expression.Or(newExpression);

        return this;
    }

    public Expression<Func<T, bool>> Build()
    {
        return _expression;
    }
}
