using App.Features.Tags.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using App.Features.Budgets.Common.Models;

namespace App.Features.Budgets.Common.Interfaces;

public interface IBudgetTagModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; }

	[Required]
	public Guid BudgetId { get; set; }

	[ForeignKey(nameof(BudgetId))]
	public BudgetModel? Budget { get; set; }

	[Required]
	public Guid TagId { get; set; }

	[ForeignKey(nameof(TagId))]
	public TagModel? Tag { get; set; }
}
