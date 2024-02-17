using App.Features.Billings.Common.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.App.Interfaces;

public interface IBillingsRepository : IGenericRepository<BillingModel>
{
}
