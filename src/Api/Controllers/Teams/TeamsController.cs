using System;
using System.Threading.Tasks;
using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Teams
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        // POST: api/teams
        [HttpPost]
        public ActionResult<CreateTeamResponseDto> PostTeam(TeamDto teamDto)
        {
            // await Task.Delay(100);
            return CreatedAtAction(nameof(PostTeam), new {Message = "Successfully Created Team"},
                teamDto);
        }
    }
}