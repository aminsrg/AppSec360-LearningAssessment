using System.Linq.Expressions;

namespace AppSec360_LearningAssessment.Application.Common.Extensions;

/// <summary>
/// Extension methods for IQueryable.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Conditionally applies a where clause if the condition is true.
    /// </summary>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    /// <summary>
    /// Orders by a property name dynamically.
    /// </summary>
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool descending = false)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = descending ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}
