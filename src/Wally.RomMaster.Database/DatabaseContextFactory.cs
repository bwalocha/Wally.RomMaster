using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Wally.RomMaster.Database
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            var connectionString = @"DataSource=rommaster_designTime.sqlite";

            optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
