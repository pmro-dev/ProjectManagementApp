using App.Features.Teams.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

public class TeamRepository : GenericRepository<TeamModel>, ITeamRepository
{
	///<inheritdoc />
	public TeamRepository(CustomAppDbContext dbContext, ILogger<TeamRepository> logger) : base(dbContext, logger)
	{
	}
}
