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
	[property: JsonProperty("full_sort_key")]
	[property: JsonPropertyName("full_sort_key")]
	string FullSortKey,
	Uri Url,
	Uri Href,
	string Kind,
	string Epoch,
	string Genre,
	string Author,
	Uri Cover,
	[property: JsonProperty("cover_thumb")]
	[property: JsonPropertyName("cover_thumb")]
	Uri CoverThumb,
	[property: JsonProperty("cover_color")]
	[property: JsonPropertyName("cover_color")]
	string CoverColor,
	[property: JsonProperty("simple_thumb")]
	[property: JsonPropertyName("simple_thumb")]
	Uri SimpleThumb,
	[property: JsonProperty("has_audio")]
	[property: JsonPropertyName("has_audio")]
	bool HasAudio,
	bool? Liked)
{
}
