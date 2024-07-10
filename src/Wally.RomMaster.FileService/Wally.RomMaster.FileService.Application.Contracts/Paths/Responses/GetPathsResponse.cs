using System;
using Wally.RomMaster.FileService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.FileService.Application.Contracts.Paths.Responses;

[ExcludeFromCodeCoverage]
public class GetPathsResponse : IResponse
{
	public Guid Id { get; private set; }

	public string Name { get; private set; }

	// public bool HasChildren { get; private set; }
}
