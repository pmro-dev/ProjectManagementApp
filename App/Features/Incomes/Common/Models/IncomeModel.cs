using App.Features.Budgets.Common.Models;
using App.Features.Incomes.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Incomes.Common.Models;

public class IncomeModel : IIncomeModel
{
	[Required]
	[Key]
	public Guid Id { get; set; }

	[Required]
	public string Name { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public long Value { get; set; }

	[Required]
	public Guid BudgetId { get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	public virtual BudgetModel? Budget { get; set; }

	public DateTime? PaymentDate { get; set; }

	public DateTime? PaymentDeadline { get; set; }

	public IncomeModel(string name, string description, long value, Guid budgetId)
	{
		Id = Guid.NewGuid();
		Name = name;
		Description = description;
		Value = value;
		BudgetId = budgetId;
	}

	public IncomeModel(string name, string description, long value, Guid budgetId, DateTime paymentDeadline)
		: this(name, description, value, budgetId)
	{
		PaymentDeadline = paymentDeadline;
	}
}
