using App.Common.Interfaces;
using App.Features.Budgets.Common.Models;

namespace App.Features.Budgets.Common.Interfaces;

public interface IBudgetFactory : IBaseEntityFactory<BudgetModel, BudgetDto>
{
}
