using App.Features.Incomes.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface IIncomeRepository : IGenericRepository<IncomeModel>
{
}
