using App.Features.Budgets.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface IBudgetRepository : IGenericRepository<BudgetModel>
{
}
