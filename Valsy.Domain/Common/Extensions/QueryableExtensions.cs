using System.Linq.Expressions;
using System.Reflection;

namespace Valsy.Domain.Common.Extensions
{
    public static partial class QueryableExtensions
    {
        public static IQueryable<T> OrderByMemberNullsDown<T>(this IQueryable<T> q, string SortField, bool asc = true)
        {
            q = q.OrderBy(GetExpression<T>(SortField));

            ParameterExpression param = Expression.Parameter(typeof(T), "p");
            MemberExpression prop = Expression.Property(param, SortField);
            LambdaExpression exp = Expression.Lambda(prop, param);
            string method = asc ? "ThenBy" : "ThenByDescending";
            Type[] types = { q.ElementType, exp.Body.Type };
            MethodCallExpression rs = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(rs);
        }

        private static Expression<Func<T, bool>> GetExpression<T>(string sortField)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "p");
            Expression prop = Expression.Property(param, sortField);

            PropertyInfo info = typeof(T).GetProperty(sortField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            Expression exp = Expression.Equal(prop, info.PropertyType.IsValueType
                ? Expression.Constant(Activator.CreateInstance(info.PropertyType))
                : Expression.Constant(null));

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

    }

}
