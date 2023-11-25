using System;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Domain.Files;

public class PathId : GuidId<PathId>
{
	public PathId()
	{
	}

	public PathId(Guid value)
		: base(value)
	{
	}

	public static explicit operator Guid(PathId id)
	{
		return id.Value;
	}
}
