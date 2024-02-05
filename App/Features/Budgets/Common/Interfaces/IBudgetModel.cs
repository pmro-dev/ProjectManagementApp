using App.Features.Billings.Common.Models;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Budgets.Common.Interfaces;

public interface IBudgetModel
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
	public ProjectModel? Project { get; set; }

	[Required]
	public string OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	public ICollection<BillingModel> Billings { get; set; }
	public ICollection<IncomeModel> Incomes { get; set; }
}