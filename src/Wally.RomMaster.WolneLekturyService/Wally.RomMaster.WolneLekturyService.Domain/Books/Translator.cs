using System.Collections.Generic;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Books;

public class Translator : ValueObject<Translator>
{
	public string Name { get; }

	private Translator()
	{
	}
	
	public Translator(string name)
	{
		Name = name;
	}
	
	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Name;
	}
}
