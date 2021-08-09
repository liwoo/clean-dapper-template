using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests.ApiTests
{
    using static Testing;

    public class HealthCheckTests
    {
        [Test]
        public async Task TestThatHealthEndpointWorks()
        {
            var response = await Client.GetAsync("/health");
            var expected = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(200);
            expected.Should().Contain("\"status\":\"Healthy");
        }
    }
}