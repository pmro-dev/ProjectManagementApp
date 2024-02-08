using App.Features.Budgets.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Budgets.Interfaces;

public interface IUserBudgetModel
{
	[Key]
	[Required]
	public string OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	[Key]
	[Required]
	public Guid BudgetId { get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	public BudgetModel? Budget { get; set; }
}
