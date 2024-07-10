using AutoMapper;
using Wally.RomMaster.HashService.Application.Contracts.Users.Requests;
using Wally.RomMaster.HashService.Application.Contracts.Users.Responses;
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
