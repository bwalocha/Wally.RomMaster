using System;
using AutoMapper;
using Wally.RomMaster.FileService.Application.Contracts.Requests.Paths;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Paths;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.MapperProfiles;

public class PathProfile : Profile
{
	public PathProfile()
	{
		CreateMap<Path, GetPathsRequest>();
		CreateMap<Path, GetPathsResponse>();

		CreateMap<PathId, Guid>()
			.ConvertUsing(a => a.Value);

		CreateMap<Guid, PathId>()
			.ConvertUsing(a => new PathId(a));
	}
}
