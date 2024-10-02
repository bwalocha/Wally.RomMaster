using System;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;

[ExcludeFromCodeCoverage]
public class GetUsersRequest : IRequest
{
	public Guid? Id { get; private set; }

	public string? Name { get; private set; }
}
