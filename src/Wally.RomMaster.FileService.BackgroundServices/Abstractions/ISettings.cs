using System.Collections.Generic;

using Wally.RomMaster.FileService.BackgroundServices.Models;

namespace Wally.RomMaster.FileService.BackgroundServices.Abstractions;

public interface ISettings
{
	List<FolderSettings> FolderSettings { get; }
}
