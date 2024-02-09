#region USINGS
using App.Features.Billings.Common.Interfaces;
using App.Features.Budgets.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion 

namespace App.Features.Billings.Common.Models;

public class BillingModel : IBillingModel
{
	[Required]
	[Key]
	public Guid Id { get; set; }

	public string DataVersion { get; set; } = Guid.NewGuid().ToString();

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

	[Required]
	public string ExecutorId { get; set; }

	[Required]
	[ForeignKey(nameof(ExecutorId))]
	public virtual UserModel? Executor { get; set; }

	public DateTime? PaymentDate { get; set; }

	public DateTime? PaymentDeadline { get; set; }

	public BillingModel(string name, string description, long value, Guid budgetId, DateTime? paymentDeadline = null, DateTime? paymentDate = null, string executorId)
	{
		Id = Guid.NewGuid();
		Name = name;
		Description = description;
		Value = value;
		BudgetId = budgetId;
		PaymentDeadline = paymentDeadline;
		PaymentDate = paymentDate;
		ExecutorId = executorId;
	}
}
