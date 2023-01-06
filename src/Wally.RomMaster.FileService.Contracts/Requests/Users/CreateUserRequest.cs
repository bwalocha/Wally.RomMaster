using Wally.Lib.DDD.Abstractions.Requests;

namespace Wally.RomMaster.FileService.Contracts.Requests.Users;

[ExcludeFromCodeCoverage]
public class CreateUserRequest : IRequest
{
	public CreateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
