using FluentAssertions;

using Xunit;

namespace Wally.RomMaster.ApiGateway.UnitTests;

public class SampleTests
{
	[Fact]
	public void SampleTest()
	{
		true.Should()
			.BeTrue();
	}
}
