using System;
using System.Threading.Tasks;

namespace Wally.Database
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();

        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class; // , IEntity;
    }
}
