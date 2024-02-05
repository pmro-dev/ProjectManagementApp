using App.Features.Budgets.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Budgets.Common.Interfaces;

public interface IBillingBase
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public long Value { get; set; }

	[Required]
	public int BudgetId { get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	public BudgetModel? Budget {  get; set; }

	public DateTime? PaymentDate { get; set; }
	
	public DateTime? PaymentDeadline { get; set; }
}
