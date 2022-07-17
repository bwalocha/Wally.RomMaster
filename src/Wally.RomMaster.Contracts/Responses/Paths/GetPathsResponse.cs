using System;

namespace Wally.RomMaster.Contracts.Responses.Paths;

[ExcludeFromCodeCoverage]
public class GetPathsResponse : IResponse
{
	public Guid Id { get; private set; }

	public string Name { get; private set; }

	public bool HasChildren { get; private set; }
}
