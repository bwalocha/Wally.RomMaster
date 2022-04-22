using System.Collections.Generic;

using Wally.RomMaster.BackgroundServices.Models;

namespace Wally.RomMaster.BackgroundServices.Abstractions;

public interface ISettings
{
	List<FolderSettings> DatRoots { get; }

	List<FolderSettings> RomRoots { get; }

	List<FolderSettings> ToSortRoots { get; }
}
