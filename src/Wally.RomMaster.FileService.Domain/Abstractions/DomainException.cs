using System;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public abstract class DomainException : Exception
{
	public DomainException(string message)
		: base(message)
	{
	}
}
