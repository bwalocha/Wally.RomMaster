using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Wally.RomMaster.WolneLekturyService.Infrastructure.WolneLekturyApiClient;
using Xunit;

namespace Wally.RomMaster.WolneLekturyService.Tests.IntegrationTests.Infrastructure.WolneLekturyApiClient;

public class ApiClientBooksTests
{
	private const string WatchFolder = "d:/WatchFolder";
	private readonly ApiClient _apiClient;

	public ApiClientBooksTests()
	{
		_apiClient = new ApiClient(new HttpClient());
	}
	
	[Fact(Skip = "Disabled")]
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

			var details = await _apiClient.GetBookAsync(resource.Slug);

			details.Should().NotBeNull();

			if (File.Exists(Path.Combine(WatchFolder, $"{resource.Slug}.crawljob")))
			{
				continue;
			}

			await Task.Delay(TimeSpan.FromSeconds(1));
			
			await using var outputFile = new StreamWriter(Path.Combine(WatchFolder, $"{resource.Slug}.crawljob"));

			async Task write(string slug, Uri url, bool last = false)
			{
				if (!url.IsAbsoluteUri)
				{
					return;
				}
				
				await outputFile.WriteLineAsync("  {");
				await outputFile.WriteLineAsync($"""
												"packageName": "WolneLekturyPl-{slug}",
												"comment": "{slug}",
												"enabled": "TRUE",
												"autoStart": "TRUE",
												"forcedStart": "FALSE",
												"autoConfirm": "TRUE",
												"text": "{url}",
												"filename": "{Path.GetFileName(url.AbsolutePath)}"
												""");
				if (last)
				{
					await outputFile.WriteLineAsync("  }");
				} 
				else
				{
					await outputFile.WriteLineAsync("  },");
				}
			}
				
			await outputFile.WriteLineAsync("[");
				
			await write(resource.Slug, details!.Epub);
			await write(resource.Slug, details.Mobi);
			await write(resource.Slug, details.Pdf);
			await write(resource.Slug, details.Html);
			await write(resource.Slug, details.Txt);
			await write(resource.Slug, details.Fb2);
			await write(resource.Slug, details.Xml);

			foreach (var media in details.Media)
			{
				await write(resource.Slug, media.Url);
			}
				
			await write(resource.Slug, details.SimpleCover);
			await write(resource.Slug, details.CoverThumb);
			await write(resource.Slug, details.Cover);
			await write(resource.Slug, details.SimpleThumb, true);
				
			await outputFile.WriteLineAsync($"]");
		}
	}
}
