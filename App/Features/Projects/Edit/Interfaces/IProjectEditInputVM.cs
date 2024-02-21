using App.Common.Helpers;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Edit.Interfaces;

public interface IProjectEditInputVM
{
	Guid Id { get; set; }

	string Title { get; set; }

    string Description { get; set; }

    ICollection<ProjectUserModel> ProjectClients { get; set; }

    string OwnerId { get; set; }

    ProjectStatusType Status { get; set; }

    [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    DateTime Deadline { get; set; }

    ICollection<TagModel> Tags { get; set; }
    ICollection<ProjectTagModel> ProjectTags { get; set; }
}
