using Wally.RomMaster.FileService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.FileService.Application.Contracts.Users.Requests;

[ExcludeFromCodeCoverage]
public class CreateUserRequest : IRequest
{
	public CreateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
