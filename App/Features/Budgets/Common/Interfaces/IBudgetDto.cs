using App.Features.Billings.Common.Models;
using App.Features.Budgets.Common.Models;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Features.Budgets.Common.Interfaces;

public interface IBudgetDto
{
	Guid Id { get; set; }

	byte[] RowVersion { get; set; }

	string Title { get; set; }

	string Description { get; set; }

	Guid ProjectId { get; set; }

	ProjectModel? Project { get; set; }

	string? OwnerId { get; set; }

	UserModel? Owner { get; set; }

	ICollection<BillingModel> Billings { get; set; }
	ICollection<IncomeModel> Incomes { get; set; }

	ICollection<TagModel> Tags { get; set; }
	ICollection<BudgetTagModel> BudgetTags { get; set; }
}
