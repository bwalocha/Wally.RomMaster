using AutoMapper;

using Wally.RomMaster.HashService.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Contracts.Responses.Users;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.MapperProfiles;

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
