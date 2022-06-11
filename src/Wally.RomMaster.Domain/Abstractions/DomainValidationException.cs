using System;

namespace Wally.RomMaster.Domain.Abstractions;

public abstract class DomainValidationException : Exception
{
	protected DomainValidationException(string message)
		: base(message)
	{
	}
}
