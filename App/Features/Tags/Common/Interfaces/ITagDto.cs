﻿using App.Features.Tasks.Common.TaskTags.Common;

namespace App.Features.Tags.Common.Interfaces
{
	public interface ITagDto
	{
		Guid Id { get; set; }
		ICollection<TaskTagDto> TaskTags { get; set; }
		string Title { get; set; }
	}
}