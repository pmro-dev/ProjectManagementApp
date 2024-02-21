using App.Common.Helpers;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Projects.Edit.Interfaces;
using App.Features.Tags.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Edit.Models;

public class ProjectEditInputVM : IProjectEditInputVM
{
    public Guid Id { get; set; } = Guid.Empty;

	public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ICollection<ProjectUserModel> ProjectClients { get; set; } = new List<ProjectUserModel>();

    public string OwnerId { get; set; } = string.Empty;

    public ProjectStatusType Status { get; set; }

    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime Deadline { get; set; }

    public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
    public ICollection<ProjectTagModel> ProjectTags { get; set; } = new List<ProjectTagModel>();
}
