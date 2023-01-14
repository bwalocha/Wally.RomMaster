using System;

using Wally.RomMaster.HashService.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Persistence.Exceptions;

public class ResourceNotFoundException : Exception, INotFound
{
	public ResourceNotFoundException(string message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
