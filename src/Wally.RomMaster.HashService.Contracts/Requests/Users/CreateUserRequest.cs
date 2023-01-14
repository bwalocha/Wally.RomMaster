using Wally.Lib.DDD.Abstractions.Requests;

namespace Wally.RomMaster.HashService.Contracts.Requests.Users;

[ExcludeFromCodeCoverage]
public class CreateUserRequest : IRequest
{
	public CreateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
