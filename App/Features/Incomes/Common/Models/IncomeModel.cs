using App.Features.Budgets.Common.Interfaces;
using App.Features.Budgets.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Incomes.Common.Models;

public class IncomeModel : IBillingBase
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
	public virtual BudgetModel? Budget { get; set; }

	public DateTime? PaymentDate { get; set; }

	public DateTime? PaymentDeadline { get; set; }

	public IncomeModel(string name, string description, long value, int budgetId)
	{
		Name = name;
		Description = description;
		Value = value;
		BudgetId = budgetId;
	}

	public IncomeModel(string name, string description, long value, int budgetId, DateTime paymentDeadline)
		: this(name, description, value, budgetId)
	{
		PaymentDeadline = paymentDeadline;
	}
}
