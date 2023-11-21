using App.Features.Tags.Common;
using App.Features.Tasks.Common.Models;

namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagModel
{
	TagModel Tag { get; set; }
	int TagId { get; set; }
	TaskModel Task { get; set; }
	int TaskId { get; set; }
}