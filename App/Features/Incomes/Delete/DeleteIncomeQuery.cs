using MediatR;

namespace App.Features.Incomes.Delete;

public class DeleteIncomeQuery : IRequest<DeleteIncomeQueryResponse>
{
	public Guid IncomeId { get; set; }

	public DeleteIncomeQuery(Guid incomeId)
	{
		IncomeId = incomeId;
	}
}

public record DeleteIncomeQueryResponse(
	DeleteBudgetOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}