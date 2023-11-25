using System;
using Wally.RomMaster.HashService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence.Exceptions;

public class ResourceNotFoundException : Exception, INotFound
{
	public ResourceNotFoundException(string message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
