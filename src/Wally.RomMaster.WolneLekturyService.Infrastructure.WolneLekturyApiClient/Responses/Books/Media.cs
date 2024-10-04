using System;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Books;

public sealed record Media(string Name, string Type, string Director, string Artist, Uri Url)
{
}
