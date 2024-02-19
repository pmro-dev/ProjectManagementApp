using App.Common.Interfaces;
using App.Features.Billings.Common.Models;

namespace App.Features.Billings.Common.Interfaces;

public interface IBillingFactory : IBaseEntityFactory<BillingModel, BillingDto>
{
	BillingModel CreateModel(string name, string description, long value, Guid budgetId, string executorId, DateTime? paymentDeadline = null, DateTime? paymentDate = null);
}
