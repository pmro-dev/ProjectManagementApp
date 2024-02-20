using App.Common.Interfaces;
using App.Features.Teams.Common.Models;

namespace App.Features.Teams.Common.Interfaces;

public interface ITeamFactory : IBaseEntityFactory<TeamModel, TeamDto>
{
	TeamModel CreateModel(string name, string description);
	TeamDto CreateDto(string name, string description);
}
