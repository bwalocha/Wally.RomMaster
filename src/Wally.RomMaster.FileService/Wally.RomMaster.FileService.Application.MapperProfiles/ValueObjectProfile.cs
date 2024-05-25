using AutoMapper;
using Wally.RomMaster.FileService.Domain.Abstractions;

namespace Wally.RomMaster.FileService.Application.MapperProfiles;

public class ValueObjectProfile : Profile
{
	public ValueObjectProfile()
	{
		CreateMap<ValueObject<string>, string>()
			.ConvertUsing(a => a.Value);
		
		CreateMap<ValueObject<int>, int>()
			.ConvertUsing(a => a.Value);
	}
}
