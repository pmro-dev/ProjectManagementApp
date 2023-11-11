using Web.TaskTags.Common.Interfaces;

namespace Web.Tags.Common.Interfaces
{
    public interface ITagModel
    {
        int Id { get; set; }
        ICollection<ITaskTagModel> TaskTags { get; set; }
        string Title { get; set; }
    }
}