using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Authors;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Books;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Collections;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Epochs;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Genres;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Kinds;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient.Responses.Themes;

namespace Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient;

public interface IApiClient
{
	Task<Book[]?> GetBooksAsync(CancellationToken cancellationToken = default);
	
	Task<Book> GetBookAsync(string slug, CancellationToken cancellationToken = default);
	
	Task<Book[]?> GetAudiobooksAsync(CancellationToken cancellationToken = default);
	
	Task<object[]?> GetDaisyAsync(CancellationToken cancellationToken = default);
	
	Task<object> GetDaisyAsync(string slug, CancellationToken cancellationToken = default);
	
	Task<Author[]?> GetAuthorsAsync(CancellationToken cancellationToken = default);
	
	Task<Author> GetAuthorAsync(string slug, CancellationToken cancellationToken = default);
	
	Task<Epoch[]?> GetEpochsAsync(CancellationToken cancellationToken = default);
	
	Task<Epoch> GetEpochAsync(string slug, CancellationToken cancellationToken = default);
	
	Task<Genre[]?> GetGenresAsync(CancellationToken cancellationToken = default);
	
	Task<Genre> GetGenreAsync(string slug, CancellationToken cancellationToken = default);
	
	Task<Kind[]?> GetKindsAsync(CancellationToken cancellationToken = default);
	
	Task<Kind> GetKindAsync(string slug, CancellationToken cancellationToken = default);
	
	Task<Theme[]?> GetThemesAsync(CancellationToken cancellationToken = default);
	
	Task<Theme> GetThemeAsync(string slug, CancellationToken cancellationToken = default);
	
	Task<Collection[]?> GetCollectionsAsync(CancellationToken cancellationToken = default);
	
	Task<Collection> GetCollectionAsync(string slug, CancellationToken cancellationToken = default);
}
