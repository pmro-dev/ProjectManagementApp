using App.Features.Billings.Common.Interfaces;
using App.Features.Billings.Common.Models;

namespace App.Features.Billings.Common;

public class BillingFactory : IBillingFactory
{
	public BillingDto CreateDto()
	{
		return new BillingDto();
	}

	public BillingModel CreateModel()
	{
		return new BillingModel();
	}

	public BillingModel CreateModel(string name, string description, long value, Guid budgetId, string executorId, DateTime? paymentDeadline = null, DateTime? paymentDate = null)
	{
		return new BillingModel( name, description, value, budgetId, executorId, paymentDeadline, paymentDate);
	}
}
