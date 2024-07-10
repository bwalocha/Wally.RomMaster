using System;
using Wally.RomMaster.FileService.Application.Contracts.Abstractions;

namespace Wally.RomMaster.FileService.Application.Contracts.Files.Requests;

[ExcludeFromCodeCoverage]
public class GetFilesRequest : IRequest
{
	public Guid Id { get; private set; }
}
