using App.Common.Interfaces;
using App.Features.Incomes.Common.Models;

namespace App.Features.Incomes.Common.Interfaces;

public interface IIncomeFactory : IBaseEntityFactory<IncomeModel, IncomeDto>
{
	IncomeModel CreateModel(string name, string description, long value, Guid budgetId, string executorId, DateTime? paymentDeadline = null, DateTime? paymentDate = null);
}
