using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using Wally.Lib.DDD.Abstractions.DomainModels;

namespace Wally.RomMaster.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	private const string RowVersion = nameof(RowVersion);

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
		ChangeTracker.LazyLoadingEnabled = false;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// modelBuilder.HasDefaultSchema("users");
		ConfigureProperties(modelBuilder);
		ConfigureIdentityProperties(modelBuilder);

		// ConfigureConcurrencyTokens(modelBuilder);
	}

	private void ConfigureProperties(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			GetType()
				.Assembly,
			type => type.Namespace!.StartsWith(
				GetType()
					.Namespace!));
	}

	private static void ConfigureIdentityProperties(ModelBuilder modelBuilder)
	{
		var allEntities = modelBuilder.Model.GetEntityTypes();
		foreach (var entity in allEntities)
		{
			var idProperty = entity.FindProperty(nameof(Entity.Id));
			if (idProperty != null)
			{
				idProperty.ValueGenerated = ValueGenerated.Never;
			}
		}
	}

	private static void ConfigureConcurrencyTokens(ModelBuilder modelBuilder)
	{
		var allEntities = modelBuilder.Model.GetEntityTypes();
		foreach (var entity in allEntities.Where(a => a.ClrType.IsSubclassOf(typeof(AggregateRoot)))
					.Where(a => string.IsNullOrEmpty(a.GetViewName())))
		{
			var property = entity.AddProperty(RowVersion, typeof(DateTime));
			property.IsConcurrencyToken = true;
			property.ValueGenerated = ValueGenerated.OnAddOrUpdate;
		}
	}
}
