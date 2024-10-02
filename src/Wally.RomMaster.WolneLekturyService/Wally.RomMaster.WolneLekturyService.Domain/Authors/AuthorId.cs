using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Authors;

public class AuthorId : GuidId<AuthorId>
{
	public AuthorId()
	{
	}

	public AuthorId(Guid value)
		: base(value)
	{
	}
}
