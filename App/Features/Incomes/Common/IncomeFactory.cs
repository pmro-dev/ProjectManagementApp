using App.Features.Incomes.Common.Interfaces;
using App.Features.Incomes.Common.Models;

namespace App.Features.Incomes.Common;

public class IncomeFactory : IIncomeFactory
{
	public IncomeDto CreateDto()
	{
		return new IncomeDto();
	}

	public IncomeModel CreateModel()
	{
		return new IncomeModel();
	}

	public IncomeModel CreateModel(string name, string description, long value, Guid budgetId, string executorId, DateTime? paymentDeadline = null, DateTime? paymentDate = null)
	{
		return new IncomeModel(name, description, value, budgetId, executorId, paymentDeadline, paymentDate);
	}
}
