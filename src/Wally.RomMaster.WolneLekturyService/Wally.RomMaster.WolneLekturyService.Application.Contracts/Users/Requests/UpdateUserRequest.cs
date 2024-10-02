using Wally.RomMaster.WolneLekturyService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;

[ExcludeFromCodeCoverage]
public class UpdateUserRequest : IRequest
{
	public UpdateUserRequest(string name)
	{
		Name = name;
	}

	public string Name { get; }
}
