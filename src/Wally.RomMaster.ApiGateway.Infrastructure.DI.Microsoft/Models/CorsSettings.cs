using System.Collections.Generic;

namespace Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Models;

public class CorsSettings
{
	public List<string> Origins { get; } = new();
}
