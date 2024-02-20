using App.Features.Projects.Create.Interfaces;

namespace App.Features.Projects.Create.Models;

public class ProjectCreateInputVM : IProjectCreateInputVM
{
	public string CreatorId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public DateTime Deadline { get; set; } = DateTime.MinValue;
	public string TagsAsSingleText { get; set; } = string.Empty;
}
