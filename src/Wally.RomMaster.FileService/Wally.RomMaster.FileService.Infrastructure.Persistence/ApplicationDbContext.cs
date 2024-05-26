using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.FileService.Infrastructure.Persistence.Extensions;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	public const string SchemaName = "FileService";
	
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
