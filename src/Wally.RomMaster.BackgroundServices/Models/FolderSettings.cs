using System;
using System.Collections.Generic;
using System.IO;

namespace Wally.RomMaster.BackgroundServices.Models;

public class FolderSettings
{
	public bool Enabled { get; set; } = true;

	public Uri Path { get; set; } = null;

	public SearchOption SearchOptions { get; set; } = SearchOption.TopDirectoryOnly;

	public bool WatcherEnabled { get; set; } = false;

	public List<ExcludeSettings> Excludes { get; } = new();
}
