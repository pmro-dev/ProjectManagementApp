using Web.TaskTags.Common.Interfaces;

namespace Web.Tags.Common.Interfaces
{
    public interface ITagDto
    {
        int Id { get; set; }
        ICollection<ITaskTagDto> TaskTags { get; set; }
        string Title { get; set; }
    }
}