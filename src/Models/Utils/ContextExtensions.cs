using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Models.Utils
{
    public static class ContextExtensions
    {
        public static IQueryable<T> When<T>(
            this IQueryable<T> query,
            bool condition,
            Expression<Func<T, bool>> whereExpression)
            where T : class
        {
            return condition ? query.Where(whereExpression) : query;
        }

        public static async Task<TResultEntity[]> AllAsync<TEntity, TResultEntity>(
            this IQueryable<TEntity> query, Func<TEntity, TResultEntity> transform, CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class
            where TResultEntity : class
        {
            return (await query
                .AllAsync(cancellationToken))
                .Select(transform)
                .ToArray();
        }

        public static Task<T[]> AllAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default(CancellationToken))
            where T : class
        {
            return query
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);
        }

    }
}
