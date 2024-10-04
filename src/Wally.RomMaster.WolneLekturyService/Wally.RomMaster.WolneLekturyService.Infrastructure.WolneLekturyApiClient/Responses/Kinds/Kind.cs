using System;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Kinds;

public sealed record Kind(string Name, string Slug, Uri Url,  Uri Href)
{
}
