namespace Wally.RomMaster.ArchiveOrgDownloader.Models;

// TODO: Remove Setters,
// extract interfaces
// and add ConventionTests
public class AppSettings
{
	public Uri DatsRootPath { get; set; } = null;

	public Uri DownloadPath { get; set; } = null;

	public Uri ArchiveOrgUri { get; set; } = null;
}
