using System;
using System.Collections.Generic;
using System.Linq;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Authors;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Epochs;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Genres;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Kinds;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Books;

public sealed record BookDetail(
	string Title,
	string Slug,
	string FullSortKey,
	Uri Url,
	Uri Href,
	string Language,
	IReadOnlyCollection<Kind> Kinds,
	IReadOnlyCollection<Epoch> Epochs,
	IReadOnlyCollection<Genre> Genres,
	IReadOnlyCollection<Author> Authors,
	IReadOnlyCollection<Translator> Translators,
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
	TimeSpan AudioLength,
	string CoverColor,
	Uri SimpleCover,
	Uri CoverThumb,
	Uri Cover,
	Uri SimpleThumb,
	string IsbnPdf,
	string IsbnEpub,
	string IsbnMobi)
{
}
