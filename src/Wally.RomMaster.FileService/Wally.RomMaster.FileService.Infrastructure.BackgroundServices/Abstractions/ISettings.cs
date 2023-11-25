using System.Collections.Generic;
using Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Models;

namespace Wally.RomMaster.FileService.Infrastructure.BackgroundServices.Abstractions;

public interface ISettings
{
	List<FolderSettings> FolderSettings { get; }
}
