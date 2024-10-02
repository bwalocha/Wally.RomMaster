using System;
using System.Collections.Generic;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Collections;

public class Details : ValueObject<Details>
{
	public string Title { get; }
	
	public Uri Url { get; }
	
	// books[]

	// authors[]

	public string Description { get; }

	private Details()
	{
	}
	
	public Details(string title, Uri url, string description)
	{
		Title = title;
		Url = url;
		Description = description;
	}
	
	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Title;
		yield return Url;
		yield return Description;
	}
}
