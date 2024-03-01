using MediatR;

namespace App.Features.Billings.Create;

public class CreateBillingCommand : IRequest<CreateBillingCommandResponse>
{
	public Guid BudgetId { get; set; }
	public CreateBillingInputVM InputVM { get; set; }

	public CreateBillingCommand(Guid budgetId, CreateBillingInputVM inputVM)
	{
		BudgetId = budgetId;
		InputVM = inputVM;
	}
}

public record CreateBillingCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}
