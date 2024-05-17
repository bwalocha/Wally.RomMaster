using AutoMapper;
using Wally.RomMaster.HashService.Application.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Application.Contracts.Responses.Users;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Application.MapperProfiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, GetUsersRequest>();
		CreateMap<User, GetUsersResponse>();
		
		CreateMap<User, GetUserResponse>();
	}
}
