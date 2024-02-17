using App.Features.Billings.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.Common;

namespace App.Infrastructure.Databases.App;

public class BillingsRepository : GenericRepository<BillingModel>, IBillingsRepository
{
	public BillingsRepository(CustomAppDbContext dbContext, ILogger<BillingsRepository> logger) : base(dbContext, logger)
	{
	}
}
