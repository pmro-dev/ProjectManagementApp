using App.Features.Budgets.Common.Models;
using App.Features.Clients.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectModel
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public int BudgetId { get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	public BudgetModel? Budget { get; set; }

	public ICollection<ClientModel> Clients { get; set; }

	[Required]
	public int OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	[Required]
	public ProjectStatusType Status { get; set; }

	[Required]
	public DateTime Deadline { get; set; }

	[Required]
	public DateTime CreationDate { get; set; }

	[Required]
	public DateTime LastUpdated { get; set; }

	public ICollection<TodoListModel> TodoLists { get; set; }

	public ICollection<TeamModel> Teams { get; set; }
}