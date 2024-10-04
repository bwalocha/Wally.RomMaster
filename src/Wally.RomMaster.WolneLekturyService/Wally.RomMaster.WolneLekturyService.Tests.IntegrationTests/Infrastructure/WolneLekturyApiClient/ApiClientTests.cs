using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient;
using Xunit;

namespace Wally.RomMaster.WolneLekturyService.Tests.IntegrationTests.Infrastructure.WolneLekturyApiClient;

public class ApiClientTests
{
	private readonly ApiClient _apiClient;

	public ApiClientTests()
	{
		_apiClient = new ApiClient(new HttpClient());
	}
	
	[Fact]
	public async Task GetAudiobooks_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetAudiobooksAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(1248);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}
	
	[Fact]
	public async Task GetAuthors_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetAuthorsAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(551);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}

	[Fact]
	public async Task GetBooks_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetBooksAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(7018);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}
	
	[Fact]
	public async Task GetCollections_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetCollectionsAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(54);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}
	
	[Fact]
	public async Task GetEpochs_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetEpochsAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(12);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}
	
	[Fact]
	public async Task GetGenres_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetGenresAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(93);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}
	
	[Fact]
	public async Task GetKinds_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetKindsAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(3);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}
	
	[Fact]
	public async Task GetThemes_ForValidArguments_ReturnsResponse()
	{
		// Arrange
		
		// Act
		var response = await _apiClient.GetThemesAsync();
		
		// Assert
		response.Should().NotBeNull();
		response!.Length.Should()
			.Be(681);
		foreach (var resource in response!)
		{
			resource.Should().NotBeNull();
		}
	}
}
