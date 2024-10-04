using System.Threading;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient;

public interface IApiClient
{
	object[] GetBooksAsync(CancellationToken cancellationToken = default);
	
	object GetBookAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetAudiobooksAsync(CancellationToken cancellationToken = default);
	
	object GetAudiobookAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetDaisyAsync(CancellationToken cancellationToken = default);
	
	object GetDaisyAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetAuthorsAsync(CancellationToken cancellationToken = default);
	
	object GetAuthorAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetEpochsAsync(CancellationToken cancellationToken = default);
	
	object GetEpochAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetGenresAsync(CancellationToken cancellationToken = default);
	
	object GetGenreAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetKindsAsync(CancellationToken cancellationToken = default);
	
	object GetKindAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetThemesAsync(CancellationToken cancellationToken = default);
	
	object GetThemeAsync(string slug, CancellationToken cancellationToken = default);
	
	object[] GetCollectionsAsync(CancellationToken cancellationToken = default);
	
	object GetCollectionAsync(string slug, CancellationToken cancellationToken = default);
}
