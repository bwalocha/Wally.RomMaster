using Wally.Database;

namespace Wally.RomMaster.Database
{
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		private readonly DatabaseContext _databaseContext;

		// private IUnitOfWork unitOfWork;

		public UnitOfWorkFactory(DatabaseContext databaseContext)
		{
			this._databaseContext = databaseContext;
		}

		public IUnitOfWork Create()
		{
			return new UnitOfWork(_databaseContext);

			// return unitOfWork ?? (unitOfWork = new UnitOfWork(databaseContext));
		}
	}
}
