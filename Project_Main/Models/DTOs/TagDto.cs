﻿namespace Project_Main.Models.DTOs
{
    public class TagDto : ITagDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<ITaskTagDto> TaskTags { get; set; } = new List<ITaskTagDto>();
    }
}