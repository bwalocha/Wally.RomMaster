using AutoMapper;

using Wally.RomMaster.FileService.Application.Contracts.Requests.Users;
using Wally.RomMaster.FileService.Application.Contracts.Responses.Users;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Application.MapperProfiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, GetUsersRequest>();
		CreateMap<User, GetUsersResponse>();

		CreateMap<User, GetUserResponse>();

		// OData
		CreateMap<GetUsersRequest, GetUsersResponse>();
	}
}
