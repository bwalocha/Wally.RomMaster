using System;
using Wally.RomMaster.NotificationService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Contracts.Users.Requests;

[ExcludeFromCodeCoverage]
public class GetUsersRequest : IRequest
{
	public Guid? Id { get; private set; }

	public string? Name { get; private set; }
}
