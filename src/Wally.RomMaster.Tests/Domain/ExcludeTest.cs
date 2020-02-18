using FluentAssertions;
using Wally.RomMaster.Domain.Models;
using Xunit;

namespace Wally.RomMaster.Tests.Domain
{
    public class ExcludeTest
    {
        //private /*readonly */Exclude model;

        public ExcludeTest()
        {
            //model = new Exclude();
        }

        [Theory]
        [InlineData("file.bat", true)]
        [InlineData("file.bat.exe", false)]
        [InlineData("file.bat.bat", true)]
        [InlineData("file.bat.", false)]
        [InlineData("bat", false)]
        [InlineData(".bat", true)]
        public void Match_ForExtensionPattern_ReturnsValidResult(string file, bool expectedResult)
        {
            var model = new Exclude();
            model.Pattern = "*.bat";

            var result = model.Match(file);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("file_test.bat", true)]
        [InlineData("test_file.bat.exe", false)]
        [InlineData("file.bat.test", false)]
        // [InlineData("test.bat.", false)] // 2.3
        // [InlineData("test", true)] // 1.2, 1.3
        [InlineData(".test", false)]
        [InlineData("test.test.test", true)] // 2.2
        // [InlineData("test.test1.test", false)] // 3.3
        public void Match_ForSufixNamePattern_ReturnsValidResult(string file, bool expectedResult)
        {
            var model = new Exclude();
            model.Pattern = "*test.*";

            var result = model.Match(file);

            result.Should().Be(expectedResult, model.Pattern);
        }
    }
}
