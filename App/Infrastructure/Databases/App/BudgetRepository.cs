using App.Features.Budgets.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

public class BudgetRepository : GenericRepository<BudgetModel>, IBudgetRepository
{
	public BudgetRepository(CustomAppDbContext dbContext, ILogger<BudgetRepository> logger) : base(dbContext, logger)
	{
	}
}
