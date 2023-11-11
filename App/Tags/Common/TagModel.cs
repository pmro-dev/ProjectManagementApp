using System.ComponentModel.DataAnnotations;
using Web.Tags.Common.Interfaces;
using Web.TaskTags.Common.Interfaces;

namespace Web.Tags.Common;

public class TagModel : ITagModel
{
    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public ICollection<ITaskTagModel> TaskTags { get; set; } = new List<ITaskTagModel>();
}
