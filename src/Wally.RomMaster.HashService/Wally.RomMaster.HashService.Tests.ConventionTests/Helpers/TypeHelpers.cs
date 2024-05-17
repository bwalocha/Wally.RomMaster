using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wally.RomMaster.HashService.Infrastructure.DI.Microsoft;
using Wally.RomMaster.HashService.WebApi;

namespace Wally.RomMaster.HashService.Tests.ConventionTests.Helpers;

public static class TypeHelpers
{
	private static readonly List<string> _prefixes = new()
	{
		"Wally.RomMaster.HashService",
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
