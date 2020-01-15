using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wally.Database
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class// , IEntity
    {
        private readonly DbContext _context;

        private IQueryable<TEntity> DbSet { get; }

        public ReadRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = CreateSet(context);
        }

        protected virtual IQueryable<TEntity> CreateSet(DbContext context)
        {
            return context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public IQueryable<TEntity> GetAll<TProperty>(Func<IIncluder<TEntity>, IThenIncluder<TEntity, TProperty>> include)
        {
            if (include == null)
            {
                throw new ArgumentNullException(nameof(include));
            }

            return Include(include);
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> result = DbSet;
            return result.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> FindAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Func<IIncluder<TEntity>, IThenIncluder<TEntity, TProperty>> include)
        {
            if (include == null)
            {
                throw new ArgumentNullException(nameof(include));
            }

            return Include(include).FirstOrDefaultAsync(predicate);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        public IQueryable<TEntity> SqlQuery(FormattableString sql)
        {
            return _context.Set<TEntity>().FromSqlInterpolated(sql);
        }

        private IQueryable<TEntity> Include<TProperty>(Func<IIncluder<TEntity>, IThenIncluder<TEntity, TProperty>> include)
        {
            IQueryable<TEntity> result = DbSet;
            var includer = new Includer<TEntity>(result);
            var thenIncluder = include.Invoke(includer);
            return thenIncluder.Result;
        }
    }
}
