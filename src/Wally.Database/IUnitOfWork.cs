using System;
using System.Threading.Tasks;

namespace Wally.Database
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();

        IReadRepository<TEntity> GetReadRepository<TEntity>()
            where TEntity : class; // , IEntity;

        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class; // , IEntity;
    }
}
