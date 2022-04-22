using System;

using Wally.RomMaster.Persistence.Abstractions;

namespace Wally.RomMaster.Persistence.Exceptions;

public class ResourceNotFoundException : Exception, INotFound
{
}
