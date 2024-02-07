#region USINGS
using App.Common.Interfaces;
using App.Features.Billings.Common.Interfaces;
using App.Features.Budgets.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion 

namespace App.Features.Billings.Common.Models;

public class BillingModel : IBillingModel
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

	public BillingModel(string name, string description, long value, Guid budgetId)
	{
		Id = Guid.NewGuid();
		Name = name;
		Description = description;
		Value = value;
		BudgetId = budgetId;
	}
	
	public BillingModel(string name, string description, long value, Guid budgetId, DateTime paymentDeadline)
		: this(name, description, value, budgetId) { PaymentDeadline = paymentDeadline; }
}
