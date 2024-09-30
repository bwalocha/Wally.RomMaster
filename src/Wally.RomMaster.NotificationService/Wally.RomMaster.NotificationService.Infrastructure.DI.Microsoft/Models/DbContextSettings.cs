namespace Wally.RomMaster.NotificationService.Infrastructure.DI.Microsoft.Models;

public class DbContextSettings
{
	public DatabaseProviderType ProviderType { get; init; }

	public bool IsMigrationEnabled { get; init; } = true;
}
