using System;

namespace Wally.RomMaster.FileService.Application.Contracts.Responses.Files;

[ExcludeFromCodeCoverage]
public class GetFilesResponse : IResponse
{
	public Guid Id { get; private set; }

	public Uri Location { get; private set; } = null!;

	// public string Name { get; private set; }

	// public bool HasChildren { get; private set; }
}
