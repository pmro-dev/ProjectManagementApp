using App.Features.Projects.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

public class ProjectRepository : GenericRepository<ProjectModel>, IProjectRepository
{
	public ProjectRepository(CustomAppDbContext dbContext, ILogger<ProjectRepository> logger) : base(dbContext, logger)
	{
	}
}
