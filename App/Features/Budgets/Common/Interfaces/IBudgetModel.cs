#region USINGS
using App.Features.Billings.Common.Models;
using App.Features.Budgets.Common.Models;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.Budgets.Common.Interfaces;

public interface IBudgetModel
{
	[Key]
	[Required]
	Guid Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	string Title { get; set; }

	[Required]
	string Description { get; set; }

	[Required]
	Guid ProjectId { get; set; }

	[Required]
	[ForeignKey(nameof(ProjectId))]
	ProjectModel? Project { get; set; }

	string? OwnerId { get; set; }

	//[ForeignKey(nameof(OwnerId))]
	//UserModel? Owner { get; set; }

	ICollection<BillingModel> Billings { get; set; }
	ICollection<IncomeModel> Incomes { get; set; }

	ICollection<TagModel> Tags { get; set; }
	ICollection<BudgetTagModel> BudgetTags { get; set; }
}