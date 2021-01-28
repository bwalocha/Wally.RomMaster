using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Wally.RomMaster.Database
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options)
		{
			// dotnet tool install --global dotnet-ef --version 3.0.0-preview4.19216.3
			//
			// dotnet ef migrations add Initial
			// Add-Migration Initial -c DatabaseContext -o Migrations/DatabaseContext -v

			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var typesToRegister = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(
				type => type.GetInterfaces().Any(
					a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

			foreach (var type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.ApplyConfiguration(configurationInstance);
			}
		}
	}
}
