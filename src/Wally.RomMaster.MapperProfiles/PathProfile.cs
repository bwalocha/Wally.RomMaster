using AutoMapper;

using Wally.RomMaster.Contracts.Requests.Paths;
using Wally.RomMaster.Contracts.Responses.Paths;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.MapperProfiles;

public class PathProfile : Profile
{
	public PathProfile()
	{
		CreateMap<Path, GetPathsRequest>();
		CreateMap<Path, GetPathsResponse>();

		// OData
		CreateMap<GetPathsRequest, GetPathsResponse>();
	}
}
