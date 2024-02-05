using App.Features.Billings.Common.Models;
using App.Features.Budgets.Common.Interfaces;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Budgets.Common.Models;

public class BudgetModel : IBudgetModel
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public int ProjectId { get; set; }

	[Required]
	[ForeignKey(nameof(ProjectId))]
	public virtual ProjectModel? Project { get; set; }

	[Required]
	public string OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public virtual UserModel? Owner { get; set; }

	public ICollection<BillingModel> Billings { get; set; }
	public ICollection<IncomeModel> Incomes { get; set; }

	public BudgetModel(string title, string description, int projectId, string ownerId)
	{
		Title = title;
		Description = description;
		ProjectId = projectId;
		OwnerId = ownerId;

		Billings = new List<BillingModel>();
		Incomes = new List<IncomeModel>();
	}
}
