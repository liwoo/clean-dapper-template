using System.Drawing;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IntegrationTests.ApiTests
{
    using static Testing;

    public class DapperBaseTests
    {
        private readonly Team _fakeTeam;
        private ITeamRepository _teamRepository;

        public DapperBaseTests()
        {
            _fakeTeam = new Team() {
                Name = "Arsenal",
                Position = 8,
                City = Cities.London,
                Stadium = "Emirates",
                HomeKitColor = KnownColor.Firebrick
            };
        }

        [SetUp]
        public void Setup()
        {
            using var scope = ServiceScopeFactory.CreateScope();
            _teamRepository = scope.ServiceProvider.GetService<ITeamRepository>();
            ResetAndMigrateDatabase();
        }

        [Test]
        public async Task TestThatCountWorks()
        {
            _ = await _teamRepository.InsertAsync(_fakeTeam);
            var count = await _teamRepository.RecordCountAsync();
            count.Should().Be(1);
        }

        [Test]
        public async Task TestThatGetAsyncWorks()
        {
            var createdFakeTeamId = await _teamRepository.InsertAsync(_fakeTeam);
            var expected = await _teamRepository.GetAsync(1);
            expected.Id.Should().Be(createdFakeTeamId);
            expected.Name.Should().Be(_fakeTeam.Name);
        }

        [Test]
        public async Task TestThatGetAllAsyncWorks()
        {
            _ = await _teamRepository.InsertAsync(_fakeTeam);

            const string condition = "WHERE name = @Name";
            const string orderby = "Name desc";
            var parameters = new { _fakeTeam.Name };

            var expected = await _teamRepository.GetAllAsync(
                1,
                10,
                condition,
                orderby,
                parameters
            );

            expected.Should()
                .NotBeNullOrEmpty().And
                .HaveCount(1).And
                .ContainItemsAssignableTo<Team>();
        }

        [Test]
        public async Task TestThatInsertAsyncWorks()
        {
            var expected = await _teamRepository.InsertAsync(_fakeTeam);
            expected.Should().Be(1);
        }

        [Test]
        public async Task TestThatUpdateAsyncWorks()
        {
            var createdFakeTeamId = (int) await _teamRepository.InsertAsync(_fakeTeam);

            var newFakeTeam = new Team() {
                Id = createdFakeTeamId,
                Name = "Chelsea",
                Position = 1,
                City = Cities.London,
                Stadium = "Emirates",
                HomeKitColor = KnownColor.Blue
            };

            _ = await _teamRepository.UpdateAsync(newFakeTeam);
            var expected = await _teamRepository.GetAsync(createdFakeTeamId);

            expected.Name.Should().Be(newFakeTeam.Name);
            expected.HomeKitColor.Should().Be(newFakeTeam.HomeKitColor);
        }

        [Test]
        public async Task TestThatDeleteByIdAsyncWorks()
        {
            _ = await _teamRepository.InsertAsync(_fakeTeam);
            await _teamRepository.DeleteByIdAsync(1);
            var count = await _teamRepository.RecordCountAsync();
            count.Should().Be(0);
        }
    }
}