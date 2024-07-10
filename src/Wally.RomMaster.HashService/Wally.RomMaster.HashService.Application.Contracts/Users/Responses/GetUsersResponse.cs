using System;
using Wally.RomMaster.HashService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.HashService.Application.Contracts.Users.Responses;

[ExcludeFromCodeCoverage]
public class GetUsersResponse : IResponse
{
	public Guid Id { get; private set; }

	public string? Name { get; private set; }
}
