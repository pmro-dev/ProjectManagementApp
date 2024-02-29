using MediatR;

namespace App.Features.Budgets.Delete;

public class DeleteBudgetQuery : IRequest<DeleteBudgetQueryResponse>
{
	public Guid BudgetId { get; set; }

	public DeleteBudgetQuery(Guid budgetId)
	{
		BudgetId = budgetId;
	}
}

public record DeleteBudgetQueryResponse(
	DeleteBudgetOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}