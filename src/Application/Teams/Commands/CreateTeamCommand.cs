using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands
{
    public record CreateTeamCommand(TeamDto TeamDto) : IRequest<CreateTeamResponseDto>;

    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamResponseDto>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<CreateTeamResponseDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var result = await _teamRepository.InsertAsync(CreateTeamFromDto(request));
            return result switch
            {
                { } i => CreateResponseFromEntity(i, request),
                null => throw new Exception("Could not insert data in the database at this time")
            };
        }

        private static CreateTeamResponseDto CreateResponseFromEntity(int id, CreateTeamCommand request) =>
             new CreateTeamResponseDto(
                id,
                request.TeamDto.Name,
                request.TeamDto.Position,
                request.TeamDto.HomeKitColor.ToString(),
                request.TeamDto.Stadium,
                request.TeamDto.City.ToString()
            );

        private static Team CreateTeamFromDto(CreateTeamCommand request)
        {
            return new Team()
            {
                Name = request.TeamDto.Name,
                Position = request.TeamDto.Position,
                Stadium = request.TeamDto.Stadium,
                HomeKitColor = request.TeamDto.HomeKitColor,
                City = request.TeamDto.City
            };
        }
    }
}