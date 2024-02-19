using App.Features.Budgets.Common.Interfaces;
using App.Features.Tags.Common.Models;

namespace App.Features.Budgets.Common.Models;

public class BudgetTagDto : IBudgetTagDto
{
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public Guid BudgetId { get; set; } = Guid.Empty;

	public BudgetDto? Budget { get; set; }

	public Guid TagId { get; set; } = Guid.Empty;

	public TagDto? Tag { get; set; }
}
