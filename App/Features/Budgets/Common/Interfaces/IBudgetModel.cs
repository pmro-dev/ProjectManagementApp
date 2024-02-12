﻿using App.Features.Billings.Common.Models;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Budgets.Common.Interfaces;

public interface IBudgetModel
{
	[Required]
	[Key]
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

	[Required]
	string OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	UserModel? Owner { get; set; }

	ICollection<BillingModel> Billings { get; set; }
	ICollection<IncomeModel> Incomes { get; set; }
}