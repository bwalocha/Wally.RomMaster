using System;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Collections;

public sealed record Collection(string Title, Uri Url, Uri Href)
{
}
