using MediatR;

namespace App.Features.Incomes.Create;

public class CreateIncomeCommand : IRequest<CreateIncomeCommandResponse>
{
	public Guid BudgetId { get; set; }
	public CreateIncomeInputVM InputVM { get; set; }

	public CreateIncomeCommand(Guid budgetId, CreateIncomeInputVM inputVM)
	{
		BudgetId = budgetId;
		InputVM = inputVM;
	}
}

public record CreateIncomeCommandResponse(
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status201Created
){}
