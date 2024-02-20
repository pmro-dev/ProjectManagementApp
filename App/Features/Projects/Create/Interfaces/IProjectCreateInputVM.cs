namespace App.Features.Projects.Create.Interfaces;

public interface IProjectCreateInputVM
{
	string CreatorId { get; set; }
	string Title { get; set; }
	string Description { get; set; }
	DateTime Deadline { get; set; }
	string TagsAsSingleText { get; set; }
}
