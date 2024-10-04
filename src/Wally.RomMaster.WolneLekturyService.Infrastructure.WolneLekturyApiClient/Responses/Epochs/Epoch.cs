using System;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Epochs;

public sealed record Epoch(string Name, string Slug, Uri Url,  Uri Href)
{
}
