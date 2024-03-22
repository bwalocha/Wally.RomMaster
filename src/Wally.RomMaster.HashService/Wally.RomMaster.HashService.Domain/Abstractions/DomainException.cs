using System;

namespace Wally.RomMaster.HashService.Domain.Abstractions;

[Serializable]
public class DomainException : Exception
{
	public DomainException()
	{
	}

	public DomainException(string? message)
		: base(message)
	{
	}

	public DomainException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
