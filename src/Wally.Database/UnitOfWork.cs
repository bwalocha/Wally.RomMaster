using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wally.Database
{
	public class UnitOfWork : IUnitOfWork
	{
		private bool _disposed = false;
		private readonly DbContext _context;
		private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

		public UnitOfWork(DbContext context)
		{
			this._context = context;
		}

		public Task CommitAsync()
		{
			return _context.SaveChangesAsync();
		}

		public IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class // , IEntity
		{
			var type = typeof(IReadRepository<TEntity>);

			if (_repositories.ContainsKey(type))
			{
				return _repositories[type] as IReadRepository<TEntity>;
			}

			var repository = new ReadRepository<TEntity>(_context);
			_repositories.Add(type, repository);
			return repository;
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class // , IEntity
		{
			var type = typeof(IRepository<TEntity>);

			if (_repositories.ContainsKey(type))
			{
				return _repositories[type] as IRepository<TEntity>;
			}

			var repository = new Repository<TEntity>(_context);
			_repositories.Add(type, repository);
			return repository;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// context.Dispose();
				}
			}

			_disposed = true;
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
