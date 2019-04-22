﻿namespace Wally.RomMaster.Database
{
    using Wally.Database;

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DatabaseContext databaseContext;
        // private IUnitOfWork unitOfWork;

        public UnitOfWorkFactory(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(databaseContext);
            // return unitOfWork ?? (unitOfWork = new UnitOfWork(databaseContext));
        }
    }
}
