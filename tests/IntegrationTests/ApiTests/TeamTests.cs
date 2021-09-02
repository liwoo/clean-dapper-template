using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Domain.Enums;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests.ApiTests
{
    using static Testing;

    public class TeamTests
    {
        private readonly string _serializedBody;
        private ITeamRepository _teamRepository;

        public TeamTests()
        {
            _serializedBody = JsonSerializer.Serialize(new TeamDto()
            {
                Name = "Arsenal",
                Position = 8,
                City = Cities.London,
                Stadium = "Emirates",
                HomeKitColor = KnownColor.Firebrick
            });
        }
        
        [SetUp]
        public void Setup()
        {
            using var scope = ServiceScopeFactory.CreateScope();
            _teamRepository = scope.ServiceProvider.GetService<ITeamRepository>();
            ResetAndMigrateDatabase();
        }

        [Test]
        public async Task TestThatTeamCanBeCreated()
        {
            var httpResponse = await Client.PostAsync("/api/teams",
                new StringContent(_serializedBody, Encoding.UTF8, "application/json"));
            httpResponse.StatusCode.Should().Be(201);
        }

        [Test]
        public async Task TestThatTeamIsReturnedWithAResponseDto()
        {
            var httpResponse = await Client.PostAsync("/api/teams",
                new StringContent(_serializedBody, Encoding.UTF8, "application/json"));
            var content = httpResponse.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var deserializedContent = JsonSerializer.Deserialize<CreateTeamResponseDto>(content, options);
            deserializedContent.Should().NotBeNull();
        }

        [Test]
        public async Task TestThatTeamIsReflectedInDatabase()
        {
            var initialResult = await _teamRepository.GetAsync(1);
            initialResult.Should().BeNull();
            await Client.PostAsync("/api/teams",
                new StringContent(_serializedBody, Encoding.UTF8, "application/json"));
            var result = await _teamRepository.GetAsync(1);
            result.Should().NotBeNull();
        }
    }
}