﻿using System;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence.Abstractions;

namespace Wally.RomMaster.NotificationService.Infrastructure.Persistence.Exceptions;

public class ResourceNotFoundException<TResource> : Exception, INotFound
{
	public ResourceNotFoundException()
		: base($"The '{typeof(TResource).Name}' could not be found")
	{
	}

	public ResourceNotFoundException(object id)
		: base($"The '{typeof(TResource).Name}' with Id='{id}' could not be found")
	{
	}
}
