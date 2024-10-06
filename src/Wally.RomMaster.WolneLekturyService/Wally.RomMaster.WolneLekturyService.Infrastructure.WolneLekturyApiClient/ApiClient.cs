using System;
using System.Net.Http;
using System.Net.Http.Json;
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

public class ApiClient : IApiClient
{
	private const string BaseAddress = "https://wolnelektury.pl/api/";
	private readonly HttpClient _httpClient;

	public ApiClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
		
		_httpClient.BaseAddress = new Uri(BaseAddress);
	}

	public Task<Book[]?> GetBooksAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Book[]>(new Uri("books", UriKind.Relative), cancellationToken);
	}

	public Task<BookDetail?> GetBookAsync(string slug, CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<BookDetail>(new Uri($"books/{slug}", UriKind.Relative), cancellationToken);
	}

	public Task<Book[]?> GetAudiobooksAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Book[]>(new Uri("audiobooks", UriKind.Relative), cancellationToken);
	}

	public Task<object[]?> GetDaisyAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<object> GetDaisyAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Author[]?> GetAuthorsAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Author[]>(new Uri("authors", UriKind.Relative), cancellationToken);
	}

	public Task<Author> GetAuthorAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Epoch[]?> GetEpochsAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Epoch[]>(new Uri("epochs", UriKind.Relative), cancellationToken);
	}

	public Task<Epoch> GetEpochAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Genre[]?> GetGenresAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Genre[]>(new Uri("genres", UriKind.Relative), cancellationToken);
	}

	public Task<Genre> GetGenreAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Kind[]?> GetKindsAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Kind[]>(new Uri("kinds", UriKind.Relative), cancellationToken);
	}

	public Task<Kind> GetKindAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Theme[]?> GetThemesAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Theme[]>(new Uri("themes", UriKind.Relative), cancellationToken);
	}

	public Task<Theme> GetThemeAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Collection[]?> GetCollectionsAsync(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync<Collection[]>(new Uri("collections", UriKind.Relative), cancellationToken);
	}

	public Task<Collection> GetCollectionAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
