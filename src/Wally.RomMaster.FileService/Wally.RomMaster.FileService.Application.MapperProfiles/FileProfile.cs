using System;
using AutoMapper;
using Wally.RomMaster.FileService.Application.Contracts.Files.Requests;
using Wally.RomMaster.FileService.Application.Contracts.Files.Responses;
using Wally.RomMaster.FileService.Domain.Files;

namespace Wally.RomMaster.FileService.Application.MapperProfiles;

public class FileProfile : Profile
{
	public FileProfile()
	{
		CreateMap<File, GetFilesRequest>();
		CreateMap<File, GetFilesResponse>();

		CreateMap<FileId, Guid>()
			.ConvertUsing(a => a.Value);

		CreateMap<Guid, FileId>()
			.ConvertUsing(a => new FileId(a));

		CreateMap<FileLocation, Uri>()
			.ConvertUsing(a => a.Value);
	}
}
