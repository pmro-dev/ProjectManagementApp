using App.Features.Billings.Common.Models;
using App.Features.Budgets.Common.Interfaces;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Features.Budgets.Common.Models;

public class BudgetDto : IBudgetDto
{
	public Guid Id { get; set; } = Guid.Empty;

	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public Guid ProjectId { get; set; } = Guid.Empty;

	public virtual ProjectDto? Project { get; set; }

	public string OwnerId { get; set; } = string.Empty;

	public virtual UserDto? Owner { get; set; }

	public ICollection<BillingDto> Billings { get; set; } = new List<BillingDto>();
	public ICollection<IncomeDto> Incomes { get; set; } = new List<IncomeDto>();

	public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
	public ICollection<BudgetTagDto> BudgetTags { get; set; } = new List<BudgetTagDto>();
}
