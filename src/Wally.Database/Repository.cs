using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wally.Database
{
    public class Repository<TEntity> : ReadRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> DbSet { get; }

        public Repository(DbContext context)
            : base(context)
        {
        }

        protected override IQueryable<TEntity> CreateSet(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Set<TEntity>();
        }

        public TEntity Find(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await DbSet.AddAsync(entity);
            return result.Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
