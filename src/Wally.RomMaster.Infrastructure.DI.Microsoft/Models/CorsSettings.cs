using System.Collections.Generic;

namespace Wally.RomMaster.Infrastructure.DI.Microsoft.Models;

public class CorsSettings
{
	public List<string> Origins { get; } = new();
}
