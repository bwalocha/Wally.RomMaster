using AutoMapper;

using Wally.RomMaster.Contracts.Requests.Users;
using Wally.RomMaster.Contracts.Responses.Users;
using Wally.RomMaster.Domain.Users;

namespace Wally.RomMaster.MapperProfiles;

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
