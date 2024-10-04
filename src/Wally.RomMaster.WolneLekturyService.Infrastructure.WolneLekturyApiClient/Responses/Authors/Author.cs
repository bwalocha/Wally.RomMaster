using System;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Authors;

public sealed record Author(string Name, string Slug, Uri Url, Uri Href)
{
}
