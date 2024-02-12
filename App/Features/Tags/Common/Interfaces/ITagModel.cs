using App.Features.Projects.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagModel
{
	[Key]
	[Required]
	Guid Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	string Title { get; set; }

	ICollection<TaskModel> Tasks { get; set; }
	ICollection<TaskTagModel> TaskTags { get; set; }

	ICollection<ProjectModel> Projects { get; set; }
	ICollection<ProjectTagModel> ProjectTags { get; set; }
}