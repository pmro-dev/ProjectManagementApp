using App.Common.Interfaces;
using App.Features.Projects.Common.Models;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectFactory : IBaseEntityFactory<ProjectModel, ProjectDto>
{
	public ProjectModel CreateModel(string title, string description, string ownerId, DateTime deadline, Guid budgetId);
}
