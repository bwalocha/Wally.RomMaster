using System.Collections.Generic;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class FragmentData : ValueObject<FragmentData>
{
	public string Title { get; }
	public string Html { get; }

	private FragmentData()
	{
	}
	
	public FragmentData(string title, string html)
	{
		Title = title;
		Html = html;
	}
	
	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Title;
		yield return Html;
	}
}
