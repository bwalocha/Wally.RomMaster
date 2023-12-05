using System;
using System.Runtime.Serialization;
using Wally.RomMaster.HashService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence.Exceptions;

[Serializable]
public class ResourceNotFoundException : Exception, INotFound
{
	public ResourceNotFoundException()
	{
	}

	public ResourceNotFoundException(string? message)
		: base(message)
	{
	}

	public ResourceNotFoundException(string message, Exception? innerException)
		: base(message, innerException)
	{
	}

	protected ResourceNotFoundException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
