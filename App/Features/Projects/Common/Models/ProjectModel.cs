#region USINGS

using App.Common.Helpers;
using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Interfaces;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Projects.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace App.Features.Projects.Common.Models;

public class ProjectModel : IProjectModel
{
	[Required]
	[Key]
	public Guid Id { get; set; }

	[Required]
	public string DataVersion { get; set; }

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public Guid BudgetId {  get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	public virtual BudgetModel? Budget { get; set; }

	public ICollection<UserModel> Clients { get; set; }
	public ICollection<UserProjectModel> ProjectClients { get; set; }

	[Required]
	public string OwnerId {  get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public virtual UserModel? Owner { get; set; }

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

	public ProjectModel(string title, string description, string ownerId, DateTime deadline, Guid budgetId, ICollection<TeamModel>? teams = null)
	{
		Id = Guid.NewGuid();
		DataVersion = Guid.NewGuid().ToString();
		Title = title;
		Description = description;
		OwnerId = ownerId;
		Deadline = deadline;
		Created = DateTime.Now;
		LastUpdated = DateTime.Now;
		BudgetId = budgetId;

		Clients = new List<UserModel>();
		TodoLists = new List<TodoListModel>();
		Teams = teams ?? new List<TeamModel>();
	}
}