#region USINGS
using App.Features.Billings.Common.Models;
using App.Features.Budgets.Common.Interfaces;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.Budgets.Common.Models;

public class BudgetModel : IBudgetModel
{
	[Key]
	[Required]
	public Guid Id { get; set; }

	[Timestamp]
	public byte[] RowVersion { get; set; }

	[Required]
	public string Title { get; set; }

	[Required]
	public string Description { get; set; }

	[Required]
	public Guid ProjectId { get; set; }

	[Required]
	[ForeignKey(nameof(ProjectId))]
	public virtual ProjectModel? Project { get; set; }

	public string? OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	public virtual UserModel? Owner { get; set; }

	public ICollection<BillingModel> Billings { get; set; }
	public ICollection<IncomeModel> Incomes { get; set; }

	public ICollection<TagModel> Tags { get; set; }
	public ICollection<BudgetTagModel> BudgetTags { get; set; }

	public BudgetModel(string title, string description, Guid projectId, string ownerId)
	{
		Id = Guid.NewGuid();
		RowVersion = new byte[] { 1, 1, 1 };
		Title = title;
		Description = description;
		ProjectId = projectId;
		OwnerId = ownerId;

		Billings = new List<BillingModel>();
		Incomes = new List<IncomeModel>();

		Tags = new List<TagModel>();
		BudgetTags = new List<BudgetTagModel>();
	}
}
