namespace Wally.RomMaster.WolneLekturyService.Infrastructure.DI.Microsoft.Models;

public class DbContextSettings
{
	public DatabaseProviderType ProviderType { get; init; }

	public bool IsMigrationEnabled { get; init; } = true;
}
