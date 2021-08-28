using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class SampleTest
    {
        [Fact]
        public void TestThatUnitTestWorks()
        {
            true.Should().BeTrue();
        }
    }
}
