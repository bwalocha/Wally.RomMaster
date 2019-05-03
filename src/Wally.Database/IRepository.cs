using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wally.Database
{
    public interface IRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class// , IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
