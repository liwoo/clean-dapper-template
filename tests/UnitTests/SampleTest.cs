using System;
using FluentAssertions;
using Xunit;

namespace Unit
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
