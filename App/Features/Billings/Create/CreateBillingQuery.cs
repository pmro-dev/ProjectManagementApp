using App.Common.ViewModels;
using MediatR;

namespace App.Features.Billings.Create;

public class CreateBillingQuery : IRequest<CreateBillingQueryResponse>
{
	public Guid BudgetId { get; set; }

	public CreateBillingQuery(Guid budgetId)
	{
		BudgetId = budgetId;
	}
}

public record CreateBillingQueryResponse(
	WrapperViewModel<CreateBillingInputVM, CreateBillingOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}