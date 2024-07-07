using System;
using Wally.RomMaster.HashService.Domain.Abstractions;

namespace Wally.RomMaster.HashService.Domain.Files;

public class FileId : GuidId<FileId>
{
	public FileId()
	{
	}

	public FileId(Guid value)
		: base(value)
	{
	}

	public static explicit operator Guid(FileId id)
	{
		return id.Value;
	}
}
