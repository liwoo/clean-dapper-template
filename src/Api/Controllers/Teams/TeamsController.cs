using System;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Teams.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Teams
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly  IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // POST: api/teams
        [HttpPost]
        public async Task<ActionResult<CreateTeamResponseDto>> PostTeam(TeamDto teamDto)
        {
            var command = new CreateTeamCommand(teamDto);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(PostTeam), new {Id = result.Id},
                result);
        }

        [HttpGet]
        public string GetHello()
        {
            return "Hello";
        }
    }
}