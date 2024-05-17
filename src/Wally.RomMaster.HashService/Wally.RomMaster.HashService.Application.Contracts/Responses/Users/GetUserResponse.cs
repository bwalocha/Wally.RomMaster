using System;

namespace Wally.RomMaster.HashService.Application.Contracts.Responses.Users;

[ExcludeFromCodeCoverage]
public class GetUserResponse : IResponse
{
	public Guid Id { get; private set; }
	
	public string? Name { get; private set; }
}
