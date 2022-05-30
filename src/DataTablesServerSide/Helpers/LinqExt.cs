using System.Linq.Expressions;
using System.Reflection;

namespace DataTablesServerSide.Helpers
{
    public static class LinqExt
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderByProperty, bool isOrderDesc)
        {
            string command = isOrderDesc ? "OrderByDescending" : "OrderBy";
            var type = typeof(T);
            var property = type.GetProperty(orderByProperty, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new Type[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
