﻿using App.Features.Budgets.Common.Models;
using App.Features.Tags.Common.Models;

namespace App.Features.Budgets.Common.Interfaces;

public interface IBudgetTagDto
{
	byte[] RowVersion { get; set; }

	Guid BudgetId { get; set; }

	BudgetModel? Budget { get; set; }

	Guid TagId { get; set; }

	TagModel? Tag { get; set; }
}
