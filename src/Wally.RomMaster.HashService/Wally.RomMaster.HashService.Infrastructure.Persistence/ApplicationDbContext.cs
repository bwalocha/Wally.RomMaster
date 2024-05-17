using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.HashService.Infrastructure.Persistence.Extensions;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	private const string DefaultSchema = "HashService";
	
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
		ChangeTracker.LazyLoadingEnabled = false;
	}
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.HasDefaultSchema(DefaultSchema)
			.ApplyMappings<IInfrastructurePersistenceAssemblyMarker>()
			.ApplyStronglyTypedId()
			.ApplySoftDelete();
	}
}
