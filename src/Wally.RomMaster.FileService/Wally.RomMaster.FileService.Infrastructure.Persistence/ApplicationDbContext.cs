using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	private const string RowVersion = nameof(RowVersion);

	private readonly ILogger<ApplicationDbContext> _logger;

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
		: base(options)
	{
		_logger = logger;
		ChangeTracker.LazyLoadingEnabled = false;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// modelBuilder.HasDefaultSchema("users");
		ConfigureProperties(modelBuilder);
		ConfigureStronglyTypedId(modelBuilder);
		ConfigureIdentityProperties(modelBuilder);

		// ConfigureConcurrencyTokens(modelBuilder); // TODO: Fix
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
			var idPropertyName = "Id"; // nameof(AggregateRoot<,>.Id); // TODO: "Id"
			var idProperty = entity.FindProperty(idPropertyName);
			if (idProperty != null)
			{
				idProperty.ValueGenerated = ValueGenerated.Never;
			}
		}
	}

	private static void ConfigureConcurrencyTokens(ModelBuilder modelBuilder)
	{
		var allEntities = modelBuilder.Model.GetEntityTypes();
		foreach (var entity in allEntities.Where(a => a.ClrType.IsSubclassOf(typeof(AggregateRoot<,>)))
					.Where(a => string.IsNullOrEmpty(a.GetViewName())))
		{
			var property = entity.AddProperty(RowVersion, typeof(DateTime));
			property.IsConcurrencyToken = true;
			property.ValueGenerated = ValueGenerated.OnAddOrUpdate;
		}
	}

	/// <summary>
	///     Configure the <see cref="EntityTypeBuilder" /> to use the
	///     <see cref="StronglyTypedIdConverter{TStronglyTypedId,TValue}" />.
	/// </summary>
	/// <param name="entityTypeBuilder"></param>
	public static void ConfigureStronglyTypedId(ModelBuilder modelBuilder)
	{
		var allEntities = modelBuilder.Model.GetEntityTypes();
		foreach (var entity in allEntities.Where(a => InheritsGenericClass(a.ClrType, typeof(Entity<,>)))
					.Where(a => string.IsNullOrEmpty(a.GetViewName()))
					.ToArray())
		{
			var entityBuilder = modelBuilder.Entity(entity.ClrType);
			entityBuilder.UseStronglyTypedId();
		}
	}

	public static bool InheritsGenericClass(Type type, Type classType)
	{
		if (!classType.IsClass)
		{
			throw new ArgumentException($"Parameter '{nameof(classType)}' is not a Class");
		}

		while (type != null && type != typeof(object))
		{
			var current = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
			if (classType == current)
			{
				return true;
			}

			if (type.BaseType == null)
			{
				break;
			}

			type = type.BaseType;
		}

		return false;
	}
}