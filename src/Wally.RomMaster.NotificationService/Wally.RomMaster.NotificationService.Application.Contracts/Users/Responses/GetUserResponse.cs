using System;
using Wally.RomMaster.NotificationService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Contracts.Users.Responses;

[ExcludeFromCodeCoverage]
public class GetUserResponse : IResponse
{
	public Guid Id { get; private set; }

	public string? Name { get; private set; }
}
