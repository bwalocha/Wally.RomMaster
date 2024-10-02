using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.WolneLekturyService.WebApi;

namespace Wally.RomMaster.WolneLekturyService.Tests.ConventionTests.Helpers;

public static class TypeHelpers
{
	private static readonly List<string> _prefixes = new()
	{
		"Wally.RomMaster.WolneLekturyService",
	};

	public static IEnumerable<Assembly> GetAllInternalAssemblies()
	{
		var assemblies = typeof(IInfrastructureDIMicrosoftAssemblyMarker).Assembly.GetReferencedAssemblies()
			.Concat(
				typeof(Startup).Assembly.GetReferencedAssemblies())
			.Where(a => _prefixes.Exists(b => a.FullName.StartsWith(b)));

		foreach (var assembly in assemblies)
		{
			yield return Assembly.Load(assembly);
		}

		yield return typeof(Startup).Assembly;
	}
}
