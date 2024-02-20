using App.Features.Projects.Create.Interfaces;

namespace App.Features.Projects.Create.Models;

public class ProjectCreateOutputVM : IProjectCreateOutputVM
{
	public string CreatorId { get; set; } = string.Empty;
}
