using System;
using System.Net.Http;
using System.Threading;

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
	
	public object[] GetBooksAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetBookAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetAudiobooksAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetAudiobookAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetDaisyAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetDaisyAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetAuthorsAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetAuthorAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetEpochsAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetEpochAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetGenresAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetGenreAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetKindsAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetKindAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetThemesAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetThemeAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object[] GetCollectionsAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public object GetCollectionAsync(string slug, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
