using System;
using Wally.Lib.DDD.Abstractions.Requests;

namespace Wally.RomMaster.FileService.Application.Contracts.Requests.Users;

[ExcludeFromCodeCoverage]
public class GetUsersRequest : IRequest
{
	public Guid? Id { get; private set; }
	
	public string? Name { get; private set; }
}
