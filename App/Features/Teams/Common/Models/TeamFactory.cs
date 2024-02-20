using App.Features.Teams.Common.Interfaces;

namespace App.Features.Teams.Common.Models;

public class TeamFactory : ITeamFactory
{
	public TeamDto CreateDto()
	{
		return new TeamDto();
	}

	public TeamDto CreateDto(string name, string description)
	{
		return new TeamDto(name, description);
	}

	public TeamModel CreateModel()
	{
		return new TeamModel();
	}
	public TeamModel CreateModel(string name, string description)
	{
		return new TeamModel(name, description);
	}
}
