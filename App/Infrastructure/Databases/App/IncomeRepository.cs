using App.Features.Incomes.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

public class IncomeRepository : GenericRepository<IncomeModel>, IIncomeRepository
{
	public IncomeRepository(CustomAppDbContext dbContext, ILogger<IncomeRepository> logger) : base(dbContext, logger)
	{
	}
}
