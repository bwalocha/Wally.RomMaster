using System.Collections.Generic;

using Wally.RomMaster.BackgroundServices.Abstractions;
using Wally.RomMaster.BackgroundServices.Models;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

// TODO: Remove Setters,
// extract interfaces
// and add ConventionTests
public class AppSettings : ISettings
{
	public CorsSettings Cors { get; } = new();

	public AuthenticationSettings Authentication { get; } = new();

	public AuthenticationSettings SwaggerAuthentication { get; } = new();

	public DbContextSettings Database { get; } = new();
	
	public ConnectionStrings ConnectionStrings { get; } = new();

	public List<FolderSettings> DatRoots { get; } = new();

	public List<FolderSettings> RomRoots { get; } = new();

	public List<FolderSettings> ToSortRoots { get; } = new();
}
