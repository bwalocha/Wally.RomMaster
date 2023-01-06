using System;

using Wally.RomMaster.FileService.Persistence.Abstractions;

namespace Wally.RomMaster.FileService.Persistence.Exceptions;

public class ResourceNotFoundException : Exception, INotFound
{
	public ResourceNotFoundException(string message, Exception? innerException)
		: base(message, innerException)
	{
	}
}
