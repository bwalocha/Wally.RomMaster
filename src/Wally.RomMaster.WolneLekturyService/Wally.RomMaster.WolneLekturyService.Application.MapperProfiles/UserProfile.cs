using AutoMapper;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Responses;
using Wally.RomMaster.WolneLekturyService.Domain.Users;

namespace Wally.RomMaster.WolneLekturyService.Application.MapperProfiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, GetUsersRequest>();
		CreateMap<User, GetUsersResponse>();

		CreateMap<User, GetUserResponse>();
	}
}
