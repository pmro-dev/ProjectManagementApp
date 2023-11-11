using Web.Tags.Common;
using Web.Tags.Common.Interfaces;
using Web.Tasks.Common;
using Web.Tasks.Common.Interfaces;
using Web.TaskTags.Common.Interfaces;

namespace Web.TaskTags.Common;

public class TaskTagModel : ITaskTagModel
{
    public int TaskId { get; set; }
    public ITaskModel Task { get; set; } = new TaskModel();

    public int TagId { get; set; }
    public ITagModel Tag { get; set; } = new TagModel();
}
