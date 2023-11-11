using Web.Tags.Common.Interfaces;
using Web.Tasks.Common.Interfaces;

namespace Web.TaskTags.Common.Interfaces
{
    public interface ITaskTagModel
    {
        ITagModel Tag { get; set; }
        int TagId { get; set; }
        ITaskModel Task { get; set; }
        int TaskId { get; set; }
    }
}