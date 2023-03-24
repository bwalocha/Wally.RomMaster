﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Types;

using Wally.RomMaster.HashService.Domain.Abstractions;
using Wally.RomMaster.HashService.Tests.ConventionTests.Helpers;

using Xunit;

namespace Wally.RomMaster.HashService.Tests.ConventionTests;

public class RepositoryTests
{
	[Fact]
	public void Repository_ReturnedCollection_ShouldBeMaterialized()
	{
		var assemblies = Configuration.Assemblies.GetAllAssemblies();

		using (new AssertionScope(new AssertionStrategy()))
		{
			foreach (var assembly in assemblies)
			{
				var types = AllTypes.From(assembly)
					.ThatSatisfy(
						a => a.GetInterfaces()
							.Any(i => i.GetTypeDefinitionIfGeneric() == typeof(IRepository<>)));

				var notAllowedTypes = new List<Type>
				{
					typeof(IEnumerable), typeof(IEnumerable<>), typeof(IQueryable), typeof(IQueryable<>),
				};

				foreach (var type in types)
				{
					type.Methods()
						.ThatArePublicOrInternal.ReturnTypes()
						.ThatSatisfy(
							a => notAllowedTypes.Contains(a) || notAllowedTypes.Any(
								n => a.GenericTypeArguments.Any(g => g.GetTypeDefinitionIfGeneric() == n)))
						.ToArray()
						.Should()
						.BeEmpty("do not return not materialized collections from Repository '{0}'", type);
				}
			}
		}
	}
}