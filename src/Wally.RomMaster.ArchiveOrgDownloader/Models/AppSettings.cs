namespace Wally.RomMaster.ArchiveOrgDownloader.Models;

// TODO: Remove Setters,
// extract interfaces
// and add ConventionTests
public class AppSettings
{
	public Uri DatsRootPath { get; init; } = null!;

	public Uri DownloadPath { get; init; } = null!;

	public Uri ArchiveOrgUri { get; init; } = null!;
}
