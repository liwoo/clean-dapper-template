using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class DapperTeamRepository : DapperBase<Team>, ITeamRepository
    {
        public DapperTeamRepository(IConfiguration configuration): base(configuration)
        {
        }
    }
}