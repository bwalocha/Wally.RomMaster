﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.Persistence.Extensions;

public static class EntityTypeBuilderExtensions
{
	/// <summary>
	///     Configure the <see cref="EntityTypeBuilder" /> to use the
	///     <see cref="StronglyTypedIdConverter{TStronglyTypedId,TValue}" />.
	/// </summary>
	/// <param name="entityTypeBuilder">The EntityTypeBuilder.</param>
	public static void UseStronglyTypedId(this EntityTypeBuilder entityTypeBuilder)
	{
		var properties = entityTypeBuilder.Metadata.ClrType.GetProperties()
			.Where(a => (Nullable.GetUnderlyingType(a.PropertyType) ?? a.PropertyType)
				.IsStronglyTypedId());

		foreach (var property in properties)
		{
			var idType = property.PropertyType;
			var valueType = idType.GetStronglyTypedIdValueType() !;

			var converterTypeTemplate = typeof(StronglyTypedIdConverter<,>);
			var converterType = converterTypeTemplate.MakeGenericType(idType, valueType);

			entityTypeBuilder.Property(property.Name)
				.HasConversion(converterType);
		}
	}
}
