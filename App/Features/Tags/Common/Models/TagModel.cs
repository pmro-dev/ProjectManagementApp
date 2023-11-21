using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tags.Common.Models;

public class TagModel : ITagModel
{
    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public ICollection<TaskTagModel> TaskTags { get; set; } = new List<TaskTagModel>();
}
