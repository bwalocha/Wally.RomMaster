using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Authors;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Epochs;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Genres;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Kinds;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Books;

public sealed record Book(
	string Title,
	string Slug,
	[property: JsonPropertyName("full_sort_key")]
	string FullSortKey,
	Uri Url,
	Uri Href,
	string Kind,
	string Epoch,
	string Genre,
	string Author,
	Uri Cover,
	[property: JsonPropertyName("cover_thumb")]
	Uri CoverThumb,
	[property: JsonPropertyName("cover_color")]
	string CoverColor,
	[property: JsonPropertyName("simple_thumb")]
	Uri SimpleThumb,
	[property: JsonPropertyName("has_audio")]
	bool HasAudio,
	bool? Liked)
{
}
