using System;
using System.Collections.Generic;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Collections;

public sealed record Details(string Title, Uri Url, string Description)
{
}
