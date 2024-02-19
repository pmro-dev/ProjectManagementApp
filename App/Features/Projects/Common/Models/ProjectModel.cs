#region USINGS
using App.Common.Helpers;
using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Interfaces;
using App.Features.Tags.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using BenchmarkDotNet.Running;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.Projects.Common.Models;

public class ProjectModel : IProjectModel
{
	[Key]
	[Required]
	public Guid Id { get; set; } = Guid.NewGuid();

	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public Guid BudgetId {  get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	public virtual BudgetModel? Budget { get; set; }

	public ICollection<UserProjectModel> Clients { get; set; }

	public string? OwnerId {  get; set; }

	[Required]
	public ProjectStatusType Status { get; set; } = ProjectStatusType.Planning;

	[Required]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime Deadline { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime Created { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime LastUpdated { get; set; }

	public ICollection<TodoListModel> TodoLists { get; set; }
	public ICollection<TeamModel> Teams { get; set; }
	public ICollection<ProjectTeamModel> ProjectTeams { get; set; }

	public ICollection<TagModel> Tags { get; set; }
	public ICollection<ProjectTagModel> ProjectTags { get; set; }

    public ProjectModel()
    {
		Title = string.Empty;
		Description = string.Empty;
		OwnerId = string.Empty;
		Deadline = DateTime.Today;
		Created = DateTime.Now;
		LastUpdated = DateTime.Now;
		BudgetId = Guid.Empty;

		Clients = new List<UserProjectModel>();
		TodoLists = new List<TodoListModel>();
		Teams = new List<TeamModel>();
		ProjectTeams = new List<ProjectTeamModel>();
		Tags = new List<TagModel>();
		ProjectTags = new List<ProjectTagModel>();
	}

    public ProjectModel(string title, string description, string ownerId, DateTime deadline, Guid budgetId)
	{
		Title = title;
		Description = description;
		OwnerId = ownerId;
		Deadline = deadline;
		Created = DateTime.Now;
		LastUpdated = DateTime.Now;
		BudgetId = budgetId;

		Clients = new List<UserProjectModel>();
		TodoLists = new List<TodoListModel>();
		Teams = new List<TeamModel>();
		ProjectTeams = new List<ProjectTeamModel>();
		Tags = new List<TagModel>();
		ProjectTags = new List<ProjectTagModel>();
	}
}