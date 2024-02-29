using MediatR;

namespace App.Features.Incomes.Create;

public class CreateIncomeQuery : IRequest<CreateIncomeQueryResponse>
{
	public Guid BudgetId { get; set; }

	public CreateIncomeQuery(Guid budgetId)
	{
		BudgetId = budgetId;
	}
}

public record CreateIncomeQueryResponse(
	WrapperViewModel<CreateIncomeInputVM, CreateIncomeOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}