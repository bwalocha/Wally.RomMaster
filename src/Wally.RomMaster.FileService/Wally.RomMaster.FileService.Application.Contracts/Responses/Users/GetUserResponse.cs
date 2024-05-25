using System;

namespace Wally.RomMaster.FileService.Application.Contracts.Responses.Users;

[ExcludeFromCodeCoverage]
public class GetUserResponse : IResponse
{
	public Guid Id { get; private set; }
	
	public string? Name { get; private set; }
}
