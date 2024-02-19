using App.Common.Interfaces;
using App.Features.Teams.Common.Models;

namespace App.Features.Teams.Common.Interfaces;

public interface ITeamFactory : IBaseEntityFactory<TeamModel, TeamDto>
{
}
