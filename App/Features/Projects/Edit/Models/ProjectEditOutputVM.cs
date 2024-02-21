using App.Common.Helpers;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Projects.Edit.Interfaces;
using App.Features.Tags.Common.Models;
using App.Features.Users.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Edit.Models;

public class ProjectEditOutputVM : IProjectEditOutputVM
{
    public Guid Id { get; set; } = Guid.Empty;

	public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid BudgetId { get; set; }

    public ICollection<UserModel> ActualClients { get; set; } = new List<UserModel>();
	public ICollection<ProjectUserModel> ProjectClients { get; set; } = new List<ProjectUserModel>();
	public ICollection<UserModel> OtherClients { get; set; } = new List<UserModel>();

	public string OwnerId { get; set; } = string.Empty;

    public ProjectStatusType Status { get; set; }

	public SelectList? StatusSelector { get; set; }


	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
    public DateTime Deadline { get; set; }

    public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
}
