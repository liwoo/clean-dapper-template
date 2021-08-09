using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands
{
    public record CreateTeamCommand(TeamDto TeamDto) : IRequest<Team>;

    public  class CreateTeamCommandHandler: IRequestHandler<CreateTeamCommand, Team>
    {
        public Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}