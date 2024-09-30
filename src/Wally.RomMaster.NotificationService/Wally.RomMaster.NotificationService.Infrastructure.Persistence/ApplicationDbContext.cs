using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence.Extensions;

namespace Wally.RomMaster.NotificationService.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	public const string SchemaName = "NotificationService";

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
