using App.Features.Teams.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface ITeamRepository : IGenericRepository<TeamModel>
{
}
