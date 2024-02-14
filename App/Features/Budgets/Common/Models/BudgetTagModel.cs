using App.Features.Tags.Common.Models;
using App.Features.Budgets.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Budgets.Common.Models;

public class BudgetTagModel : IBudgetTagModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	[Required]
	public Guid BudgetId { get; set; }

	[ForeignKey(nameof(BudgetId))]
	public BudgetModel? Budget { get; set; }

	[Required]
	public Guid TagId { get; set; }

	[ForeignKey(nameof(TagId))]
	public TagModel? Tag { get; set; }

	//public BudgetTagModel(Guid budgetId, Guid tagId)
	//{
	//	BudgetId = budgetId;
	//	TagId = tagId;
	//	RowVersion = new byte[] { 1, 1, 1 };
	//}
}
