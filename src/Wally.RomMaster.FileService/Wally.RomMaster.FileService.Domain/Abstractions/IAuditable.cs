using System;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public interface IAuditable
{
	DateTimeOffset CreatedAt { get; }
	
	UserId CreatedById { get; }
	
	DateTimeOffset? ModifiedAt { get; }
	
	UserId? ModifiedById { get; }
}
