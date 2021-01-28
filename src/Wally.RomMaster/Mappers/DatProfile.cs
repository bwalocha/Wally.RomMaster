using AutoMapper;
using Wally.RomMaster.Domain.Models;
using Wally.RomMaster.Models;

namespace Wally.RomMaster.Mappers
{
	public class DatProfile : Profile
	{
		public DatProfile()
		{
			var map = CreateMap<Dat, DatViewModel>();

			map.ForMember(d => d.Id, opt => opt.MapFrom(a => a.Id));
		}
	}
}
