using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Wally.Database
{
	public interface IReadRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> GetAll<TProperty>(Func<IIncluder<TEntity>, IThenIncluder<TEntity, TProperty>> include);

		Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

		Task<TEntity> FindAsync<TProperty>(
			Expression<Func<TEntity, bool>> predicate,
			Func<IIncluder<TEntity>, IThenIncluder<TEntity, TProperty>> include);

		Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

		IQueryable<TEntity> SqlQuery(FormattableString sql);
	}
}
