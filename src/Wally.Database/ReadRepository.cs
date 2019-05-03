using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wally.Database
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class// , IEntity
    {
        private readonly IQueryable<TEntity> dbSet;

        public ReadRepository(DbContext context)
        {
            dbSet = context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationPropertyPaths)
        {
            IQueryable<TEntity> result = dbSet;
            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                result = result.Include(navigationPropertyPath);
            }

            return result;
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.AnyAsync(predicate);
        }

        public IQueryable<TEntity> SqlQuery(FormattableString sql)
        {
            return dbSet.FromSqlInterpolated(sql);
        }
    }
}
