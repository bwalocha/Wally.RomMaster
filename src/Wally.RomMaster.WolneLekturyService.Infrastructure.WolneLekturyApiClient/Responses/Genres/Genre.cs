using System;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Genres;

public sealed record Genre(string Name, string Slug, Uri Url,  Uri Href)
{
}
