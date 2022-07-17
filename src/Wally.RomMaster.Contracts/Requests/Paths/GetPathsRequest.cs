using System;

using Wally.Lib.DDD.Abstractions.Requests;

namespace Wally.RomMaster.Contracts.Requests.Paths;

[ExcludeFromCodeCoverage]
public class GetPathsRequest : IRequest
{
	public Guid Id { get; private set; }

	public string Name { get; private set; }
}
