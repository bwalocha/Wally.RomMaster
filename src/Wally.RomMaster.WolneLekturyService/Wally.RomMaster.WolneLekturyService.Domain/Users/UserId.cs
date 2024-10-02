using System;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Domain.Users;

public class UserId : GuidId<UserId>
{
	public UserId()
	{
	}

	public UserId(Guid value)
		: base(value)
	{
	}
}
