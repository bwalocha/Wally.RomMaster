using AutoMapper;
using Wally.RomMaster.Domain.Models;
using Wally.RomMaster.Models;

namespace Wally.RomMaster.Mappers
{
	public class GameProfile : Profile
	{
		public GameProfile()
		{
			var map = CreateMap<Game, GameViewModel>();

			map.ForMember(d => d.Id, opt => opt.MapFrom(a => a.Id));
		}
	}
}
