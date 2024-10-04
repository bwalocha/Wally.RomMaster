using System;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Authors;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Epochs;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Genres;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Kinds;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Books;

public sealed record Book(
	string Title,
	string Slug,
	string FullSortKey,
	Uri Url,
	Uri Href,
	Kind KindId,
	Epoch EpochId,
	Genre GenreId,
	Author AuthorId,
	Uri Cover,
	Uri CoverThumb,
	string CoverColor,
	Uri SimpleThumb,
	bool HasAudio,
	bool Liked)
{
}
