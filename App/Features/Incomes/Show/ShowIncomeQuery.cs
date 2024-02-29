using MediatR;

namespace App.Features.Incomes.Show;

public class ShowIncomeQuery : IRequest<ShowIncomeQueryResponse>
{
	public Guid IncomeId { get; set; }

	public ShowIncomeQuery(Guid incomeId)
	{
		IncomeId = incomeId;
	}
}

public record ShowIncomeQueryResponse(
	ShowIncomeOutputVM? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}