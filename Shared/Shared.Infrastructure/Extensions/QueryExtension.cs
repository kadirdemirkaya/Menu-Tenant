using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> FilterByTenant<T>(this IQueryable<T> query, string tenantId) where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, "TenantId");
            var constant = Expression.Constant(tenantId);
            var condition = Expression.Equal(property, constant);

            var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            return query.Where(lambda);
        }
    }
}
