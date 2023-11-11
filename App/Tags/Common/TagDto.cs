using Web.Tags.Common.Interfaces;
using Web.TaskTags.Common.Interfaces;

namespace Web.Tags.Common
{
    public class TagDto : ITagDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public ICollection<ITaskTagDto> TaskTags { get; set; } = new List<ITaskTagDto>();
    }
}
