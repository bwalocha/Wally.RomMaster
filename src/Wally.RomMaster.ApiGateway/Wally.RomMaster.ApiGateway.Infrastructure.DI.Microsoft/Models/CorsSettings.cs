using System;
using System.Collections.Generic;

namespace Wally.RomMaster.ApiGateway.Infrastructure.DI.Microsoft.Models;

public class CorsSettings
{
	public List<Uri> Origins { get; } = new();
}
