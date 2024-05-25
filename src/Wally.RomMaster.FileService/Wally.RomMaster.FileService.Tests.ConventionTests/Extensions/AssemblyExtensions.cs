using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Types;

namespace Wally.RomMaster.FileService.Tests.ConventionTests.Extensions;

public static class AssemblyExtensions
{
	public static TypeSelector GetAllTypes(this IEnumerable<Assembly> assemblies)
	{
		return assemblies
			.SelectMany(a => a.GetTypes())
			.Types();
	}
	
	public static TypeSelector GetAllExportedTypes(this IEnumerable<Assembly> assemblies)
	{
		return assemblies
			.SelectMany(a => a.GetExportedTypes())
			.Types();
	}
}
