using System;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public interface ISoftDeletable
{
	bool IsDeleted { get; }

	DateTimeOffset? DeletedAt { get; }

	UserId? DeletedById { get; }
}
