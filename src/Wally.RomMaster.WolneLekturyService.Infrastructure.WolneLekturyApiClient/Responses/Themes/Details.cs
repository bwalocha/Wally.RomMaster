using System;
using System.Collections.Generic;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Themes;

public sealed record Details(string Name, Uri Url, string SortKey, string Description, string DescriptionPl, string Plural, bool GenreEpochSpecific, string AdjectiveFeminineSingular, string AdjectiveNonmasculinePlural, string Genitive, string CollectiveNoun)
{
}
