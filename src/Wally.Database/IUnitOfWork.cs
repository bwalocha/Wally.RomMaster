using System;
using System.Threading.Tasks;

namespace Wally.Database
{
	public interface IUnitOfWork : IDisposable
	{
		Task CommitAsync();

		IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class;

		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
	}
}
