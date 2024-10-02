using System;
using AutoMapper;
using Wally.RomMaster.WolneLekturyService.Domain.Abstractions;

namespace Wally.RomMaster.WolneLekturyService.Application.MapperProfiles;

public class GuidIdProfile : Profile
{
	public GuidIdProfile()
	{
		CreateMap<IStronglyTypedId<Guid>, Guid>()
			.ConvertUsing(a => a.Value);

		CreateMap<IStronglyTypedId<Guid>?, Guid?>()
			.ConvertUsing(a => a == null ? null : a.Value);
	}
}
