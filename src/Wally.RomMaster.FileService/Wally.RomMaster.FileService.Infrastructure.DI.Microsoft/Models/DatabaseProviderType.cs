namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;

public enum DatabaseProviderType
{
	Unknown = 0,
	None,
	InMemory,
	MariaDb,
	MySql,
	PostgreSQL,
	SQLite,
	SqlServer,
}
