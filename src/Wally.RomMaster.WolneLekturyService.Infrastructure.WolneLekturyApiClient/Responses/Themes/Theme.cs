using System;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Themes;

public sealed record Theme(string Name, string Slug, Uri Url,  Uri Href)
{
}
