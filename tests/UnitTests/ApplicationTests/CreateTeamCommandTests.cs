using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Teams.Commands;
using Bogus;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace UnitTests.ApplicationTests
{
    public class CreateTeamCommandTests
    {
        [Fact]
        public async Task TestThatCreateCommandHandlerReturnsTeam()
        {
            const int key = 1;
            
            var teamRepo = Substitute.For<ITeamRepository>();
            teamRepo.InsertAsync(default).ReturnsForAnyArgs(key);
            var handler = new CreateTeamCommandHandler(teamRepo);
            var dto = new Faker<TeamDto>()
                .RuleFor(u => u.City, f => f.PickRandom<Cities>())
                .RuleFor(u => u.Name, f => f.Company.CompanyName())
                .RuleFor(u => u.Position, f => f.Random.Number(1, 20))
                .RuleFor(u => u.Stadium, f => f.Address.City())
                .RuleFor(u => u.HomeKitColor, f => f.PickRandom<KnownColor>())
                .Generate();
            
            var expected = new CreateTeamResponseDto(
                key,
                dto.Name,
                dto.Position,
                dto.HomeKitColor.ToString(),
                dto.Stadium,
                dto.City.ToString()
            );
            
            var request = new CreateTeamCommand(dto);
            var actual = await handler.Handle(request, CancellationToken.None);
            actual.Should().Be(expected);
        }
    }
}