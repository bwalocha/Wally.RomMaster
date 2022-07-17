using System;
using System.Globalization;
using System.Linq;

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

			// .ForCtorParam(nameof(Domain.DataFiles.DataFile.Date), a => a.MapFrom(b =>
			// DateTime.TryParse(b.Header.Date, out var value) ? value : (DateTime?)null
			// ))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Date), a => a.MapFrom(b => (DateTime?)null))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Description), a => a.MapFrom(b => b.Header.Description))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Email), a => a.MapFrom(b => b.Header.Email))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.File), a => a.Ignore())
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Name), a => a.MapFrom(b => b.Header.Name))

			// .ForCtorParam(nameof(Domain.DataFiles.DataFile.Url), a => a.MapFrom(b => b.Header.Url))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Url), a => a.MapFrom(b => new Uri("https://wally.best")))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Version), a => a.MapFrom(b => b.Header.Version))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.FileId), a => a.Ignore())
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.HomePage), a => a.MapFrom(b => b.Header.HomePage))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.Id), a => a.Ignore())
			.ForCtorParam(
				nameof(Domain.DataFiles.DataFile.Games),
				a => a.MapFrom(
					b => b.Games.Cast<object>()
						.Concat(b.Machines)))

			//.ForMember(a => a._games, a => a.MapFrom(b => b.Games));
			// .AfterMap((prop, reprop) => reprop.AddGames(prop.Games));
			;

		CreateMap<Game, Domain.DataFiles.Game>();
		CreateMap<Machine, Domain.DataFiles.Game>()
			.ForCtorParam(nameof(Domain.DataFiles.Game.Name), a => a.MapFrom(b => b.Name))
			.ForCtorParam(nameof(Domain.DataFiles.Game.Description), a => a.MapFrom(b => b.Description))
			.ForCtorParam(nameof(Domain.DataFiles.Game.Year), a => a.MapFrom(b => string.Empty))

			// .ForCtorParam(nameof(Domain.DataFiles.Game.Name), a => a.MapFrom(b => b.Name))
			;
		CreateMap<Rom, Domain.DataFiles.Rom>()
			.ForMember(
				a => a.Size,
				a => a.MapFrom(
					b => string.IsNullOrEmpty(b.Size) ? 0 :
						b.Size == "-" ? 0 : ulong.Parse(b.Size, NumberStyles.None, CultureInfo.InvariantCulture)));

		CreateMap<RetroBytesFile, Domain.DataFiles.DataFile>()
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Author), a => a.MapFrom(b => b.Header.Author))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Category), a => a.MapFrom(b => b.Header.Category))

			// .ForCtorParam(nameof(Domain.DataFiles.DataFile.Date), a => a.MapFrom(b =>
			// DateTime.TryParse(b.Header.Date, out var value) ? value : (DateTime?)null
			// ))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Date), a => a.MapFrom(b => (DateTime?)null))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Description), a => a.MapFrom(b => b.Header.Description))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Email), a => a.MapFrom(b => b.Header.Email))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.File), a => a.Ignore())
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Name), a => a.MapFrom(b => b.Header.Name))

			// .ForCtorParam(nameof(Domain.DataFiles.DataFile.Url), a => a.MapFrom(b => b.Header.Url))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Url), a => a.MapFrom(b => new Uri("https://wally.best")))
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Version), a => a.MapFrom(b => b.Header.Version))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.FileId), a => a.Ignore())
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.HomePage), a => a.MapFrom(b => b.Header.HomePage))

			// .ForCtorParam(nameof(Wally.RomMaster.Domain.DataFiles.DataFile.Id), a => a.Ignore())
			.ForCtorParam(nameof(Domain.DataFiles.DataFile.Games), a => a.MapFrom(b => b.Games))

			//.ForMember(a => a._games, a => a.MapFrom(b => b.Games));
			// .AfterMap((prop, reprop) => reprop.AddGames(prop.Games));
			;
	}
}
