﻿using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;

namespace App.Features.Tags.Common.Models
{
    public class TagDto : ITagDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<TaskTagDto> TaskTags { get; set; } = new List<TaskTagDto>();
    }
}
