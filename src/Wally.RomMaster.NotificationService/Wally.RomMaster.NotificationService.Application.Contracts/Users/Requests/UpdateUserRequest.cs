using Wally.RomMaster.NotificationService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Contracts.Users.Requests;

[ExcludeFromCodeCoverage]
public class UpdateUserRequest : IRequest
{
	public UpdateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
