using App.Features.Billings.Common.Models;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Features.Budgets.Common.Models;

public class BudgetDto
{
	public Guid Id { get; set; } = Guid.Empty;

	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public Guid ProjectId { get; set; } = Guid.Empty;

	public virtual ProjectModel? Project { get; set; }

	public string OwnerId { get; set; } = string.Empty;

	public virtual UserModel? Owner { get; set; }

	public ICollection<BillingModel> Billings { get; set; } = new List<BillingModel>();
	public ICollection<IncomeModel> Incomes { get; set; } = new List<IncomeModel>();

	public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
	public ICollection<BudgetTagModel> BudgetTags { get; set; } = new List<BudgetTagModel>();
}
