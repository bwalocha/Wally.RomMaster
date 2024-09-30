using Wally.RomMaster.NotificationService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.NotificationService.Application.Contracts.Users.Requests;

[ExcludeFromCodeCoverage]
public class CreateUserRequest : IRequest
{
	public CreateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
