using AutoMapper;
using Wally.RomMaster.NotificationService.Application.Contracts.Users.Requests;
using Wally.RomMaster.NotificationService.Application.Contracts.Users.Responses;
using Wally.RomMaster.NotificationService.Domain.Users;

namespace Wally.RomMaster.NotificationService.Application.MapperProfiles;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, GetUsersRequest>();
		CreateMap<User, GetUsersResponse>();

		CreateMap<User, GetUserResponse>();
	}
}
