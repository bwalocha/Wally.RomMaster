using Microsoft.EntityFrameworkCore;
using Wally.RomMaster.FileService.Infrastructure.Persistence.Extensions;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	private const string DefaultSchema = "FileService";
	
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
