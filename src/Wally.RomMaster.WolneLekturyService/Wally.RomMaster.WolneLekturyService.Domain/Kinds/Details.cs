using System;
using System.Collections.Generic;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Kinds;

public class Details : ValueObject<Details>
{
	public string Name { get; }
	public Uri Url { get; }
	public string SortKey { get; }
	public string Description { get; }
	public string DescriptionPl { get; }
	public string Plural { get; }
	public bool GenreEpochSpecific { get; }
	public string AdjectiveFeminineSingular { get; }
	public string AdjectiveNonmasculinePlural { get; }
	public string Genitive { get; }
	public string CollectiveNoun { get; }

	private Details()
	{
	}
	
	public Details(string name, Uri url, string sortKey, string description, string descriptionPl, string plural, bool genreEpochSpecific, string adjectiveFeminineSingular, string adjectiveNonmasculinePlural, string genitive, string collectiveNoun)
	{
		Name = name;
		Url = url;
		SortKey = sortKey;
		Description = description;
		DescriptionPl = descriptionPl;
		Plural = plural;
		GenreEpochSpecific = genreEpochSpecific;
		AdjectiveFeminineSingular = adjectiveFeminineSingular;
		AdjectiveNonmasculinePlural = adjectiveNonmasculinePlural;
		Genitive = genitive;
		CollectiveNoun = collectiveNoun;
	}
	
	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Name;
		yield return Url;
		yield return SortKey;
		yield return Description;
		yield return DescriptionPl;
		yield return Plural;
		yield return GenreEpochSpecific;
		yield return AdjectiveFeminineSingular;
		yield return AdjectiveNonmasculinePlural;
		yield return Genitive;
		yield return CollectiveNoun;
	}
}
