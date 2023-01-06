using System;
using System.Collections.Generic;
using System.IO;

namespace Wally.RomMaster.FileService.BackgroundServices.Models;

public class FolderSettings
{
	public bool Enabled { get; init; } = true;

	public Uri Path { get; init; } = null!;

	public SearchOption SearchOptions { get; init; } = SearchOption.TopDirectoryOnly;

	public bool WatcherEnabled { get; init; } = false;

	public List<ExcludeSettings> Excludes { get; } = new();
}
