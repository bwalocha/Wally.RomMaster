using AutoMapper;

using Wally.RomMaster.DatFileParser.Models;

namespace Wally.RomMaster.DatFileParser.MapperProfiles;

public class DataFileProfile : Profile
{
	public DataFileProfile()
	{
		CreateMap<DataFile, Domain.DataFiles.DataFile>()
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Author), a => a.MapFrom(b => b.Header.Author))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Category), a => a.MapFrom(b => b.Header.Category))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Date), a => a.MapFrom(b => b.Header.Date))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Description), a => a.MapFrom(b => b.Header.Description))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Email), a => a.MapFrom(b => b.Header.Email))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.File), a => a.Ignore())
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Name), a => a.MapFrom(b => b.Header.Name))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Url), a => a.MapFrom(b => b.Header.Url))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Version), a => a.MapFrom(b => b.Header.Version))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.FileId), a => a.Ignore())
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.HomePage), a => a.MapFrom(b => b.Header.HomePage))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.Id), a => a.Ignore())

			//.ForMember(a => a._games, a => a.MapFrom(b => b.Games));
			// .AfterMap((prop, reprop) => reprop.AddGames(prop.Games));
			;

		CreateMap<Game, Domain.DataFiles.Game>();
		CreateMap<Rom, Domain.DataFiles.Rom>();
	}
}
