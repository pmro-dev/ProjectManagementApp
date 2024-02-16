using App.Features.Billings.Common.Interfaces;
using App.Features.Budgets.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Features.Billings.Common.Models;

public class BillingDto : IBillingDto
{
	public Guid Id { get; set; }

	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public string Name { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public long Value { get; set; }

	public string ExecutorId { get; set; } = string.Empty;

	public UserModel? Executor { get; set; }

	public Guid BudgetId { get; set; } = Guid.Empty;

	public BudgetModel? Budget { get; set; }

	public DateTime? PaymentDate { get; set; }

	public DateTime? PaymentDeadline { get; set; }
}
