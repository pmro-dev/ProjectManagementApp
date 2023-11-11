using Web.Tags.Common.Interfaces;
using Web.Tasks.Common.Interfaces;

namespace Web.TaskTags.Common.Interfaces
{
    public interface ITaskTagDto
    {
        ITagDto Tag { get; set; }

        int TagId { get; set; }

        ITaskDto Task { get; set; }

        int TaskId { get; set; }
    }
}