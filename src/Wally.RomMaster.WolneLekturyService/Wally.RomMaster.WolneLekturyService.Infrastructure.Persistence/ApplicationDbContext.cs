using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence.Extensions;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	public const string SchemaName = "WolneLekturyService";

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
		ChangeTracker.LazyLoadingEnabled = false;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.HasDefaultSchema(SchemaName)
			.ApplyMappings<IInfrastructurePersistenceAssemblyMarker>()
			.ApplyStronglyTypedId()
			.ApplySoftDelete();
	}
}
