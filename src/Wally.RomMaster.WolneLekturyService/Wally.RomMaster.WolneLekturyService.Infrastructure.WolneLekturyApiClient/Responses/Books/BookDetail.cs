using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Authors;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Epochs;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Genres;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Kinds;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Books;

public sealed record BookDetail(
	string Title,
	// string Slug,
	// string FullSortKey,
	Uri Url,
	// Uri Href,
	string Language,
	IReadOnlyCollection<Kind> Kinds,
	IReadOnlyCollection<Epoch> Epochs,
	IReadOnlyCollection<Genre> Genres,
	IReadOnlyCollection<Author> Authors,
	IReadOnlyCollection<Translator> Translators,
	[property: JsonPropertyName("fragment_data")]
	FragmentData FragmentData,
	// children
	// parent?
	bool Preview,
	Uri Epub,
	Uri Mobi,
	Uri Pdf,
	Uri Html,
	Uri Txt,
	Uri Fb2,
	Uri Xml,
	IReadOnlyCollection<Media> Media,
	[property: JsonPropertyName("audio_length")]
	string AudioLength,
	[property: JsonPropertyName("cover_color")]
	string CoverColor,
	[property: JsonPropertyName("simple_cover")]
	Uri SimpleCover,
	[property: JsonPropertyName("cover_thumb")]
	Uri CoverThumb,
	Uri Cover,
	[property: JsonPropertyName("simple_thumb")]
	Uri SimpleThumb,
	[property: JsonPropertyName("isbn_pdf")]
	string IsbnPdf,
	[property: JsonPropertyName("isbn_epub")]
	string IsbnEpub,
	[property: JsonPropertyName("isbn_mobi")]
	string IsbnMobi)
{
}
