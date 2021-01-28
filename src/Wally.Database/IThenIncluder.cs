using System.Linq;

namespace Wally.Database
{
	public interface IThenIncluder<TEntity, TPreviousProperty> : IIncluder<TEntity> where TEntity : class
	{
	}
}
