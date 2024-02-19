using App.Features.Projects.Common.Interfaces;
using App.Features.Projects.Common.Models;

namespace App.Features.Projects.Common;

public class ProjectFactory : IProjectFactory
{
	public ProjectDto CreateDto()
	{
		return new ProjectDto();
	}

	public ProjectModel CreateModel()
	{
		return new ProjectModel();
	}

	public ProjectModel CreateModel(string title, string description, string ownerId, DateTime deadline, Guid budgetId)
	{
		return new ProjectModel(title, description, ownerId, deadline, budgetId);
	}
}
