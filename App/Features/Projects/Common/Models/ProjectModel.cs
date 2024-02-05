using App.Common.Helpers;
using App.Features.Budgets.Common.Models;
using App.Features.Clients.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Interfaces;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Projects.Common.Models;

public class ProjectModel : IProjectModel
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public int BudgetId {  get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	public virtual BudgetModel? Budget { get; set; }

	public ICollection<ClientModel> Clients { get; set; }

	[Required]
	public int OwnerId {  get; set; }

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
	public DateTime CreationDate { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
	public DateTime LastUpdated { get; set; }

	public ICollection<TodoListModel> TodoLists { get; set; } = new List<TodoListModel>();

	public ICollection<TeamModel> Teams { get; set; } = new List<TeamModel>();

	public ProjectModel(string title, string description, int ownerId, DateTime deadline, int budgetId)
	{
		Title = title;
		Description = description;
		OwnerId = ownerId;
		Deadline = deadline;

		CreationDate = DateTime.Now;
		LastUpdated = DateTime.Now;
		BudgetId = budgetId;

		Clients = new List<ClientModel>();
	}
}