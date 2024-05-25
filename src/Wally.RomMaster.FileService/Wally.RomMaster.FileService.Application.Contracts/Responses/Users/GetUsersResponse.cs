using System;

namespace Wally.RomMaster.FileService.Application.Contracts.Responses.Users;

[ExcludeFromCodeCoverage]
public class GetUsersResponse : IResponse
{
	public Guid Id { get; private set; }
	
	public string? Name { get; private set; }
}
