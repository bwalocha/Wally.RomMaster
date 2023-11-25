using System;
using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Domain.Users;

public class UserId : GuidId<UserId>
{
	public UserId()
	{
	}

	public UserId(Guid value)
		: base(value)
	{
	}

	public static explicit operator Guid(UserId id)
	{
		return id.Value;
	}
}
