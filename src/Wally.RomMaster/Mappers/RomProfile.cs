using AutoMapper;
using Wally.RomMaster.Domain.Models;
using Wally.RomMaster.Models;

namespace Wally.RomMaster.Mappers
{
    public class RomProfile : Profile
    {
        public RomProfile()
        {
            var map = CreateMap<Rom, RomViewModel>();

            map.ForMember(d => d.Id, opt => opt.MapFrom(a => a.Id))
               ;
        }
    }
}
