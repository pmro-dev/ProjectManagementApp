using App.Features.Projects.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface IProjectRepository : IGenericRepository<ProjectModel>
{
}

