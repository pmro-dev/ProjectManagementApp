using MediatR;

namespace App.Features.Budgets.Show;

public class ShowBudgetQuery : IRequest<ShowBudgetQueryResponse>
{
	public Guid BudgetId { get; set; }

	public ShowBudgetQuery(Guid budgetId)
	{
		BudgetId = budgetId;
	}
}

public record ShowBudgetQueryResponse(
	ShowBudgetOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}