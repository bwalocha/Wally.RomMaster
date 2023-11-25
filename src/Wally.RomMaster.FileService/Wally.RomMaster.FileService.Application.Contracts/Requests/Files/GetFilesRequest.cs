using System;
using Wally.Lib.DDD.Abstractions.Requests;

namespace Wally.RomMaster.FileService.Application.Contracts.Requests.Files;

[ExcludeFromCodeCoverage]
public class GetFilesRequest : IRequest
{
	public Guid Id { get; private set; }
}
