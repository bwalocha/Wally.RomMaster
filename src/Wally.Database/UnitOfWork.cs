using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wally.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public Task CommitAsync()
        {
            return context.SaveChangesAsync();
        }

        public IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class// , IEntity
        {
            var type = typeof(IReadRepository<TEntity>);

            if (repositories.ContainsKey(type))
            {
                return repositories[type] as IReadRepository<TEntity>;
            }

            var repository = new ReadRepository<TEntity>(context);
            repositories.Add(type, repository);
            return repository;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class// , IEntity
        {
            var type = typeof(IRepository<TEntity>);

            if (repositories.ContainsKey(type))
            {
                return repositories[type] as IRepository<TEntity>;
            }

            var repository = new Repository<TEntity>(context);
            repositories.Add(type, repository);
            return repository;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
