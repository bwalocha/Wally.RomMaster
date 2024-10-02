using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Themes;

public class ThemeId : GuidId<ThemeId>
{
	public ThemeId()
	{
	}

	public ThemeId(Guid value)
		: base(value)
	{
	}
}
