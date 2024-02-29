using App.Common.ViewModels;
using MediatR;

namespace App.Features.Incomes.Edit;

public class EditIncomeQuery : IRequest<EditIncomeQueryResponse>
{
	public Guid IncomeId { get; set; }

	public EditIncomeQuery(Guid incomeId)
	{
		IncomeId = incomeId;
	}
}

public record EditIncomeQueryResponse(
	WrapperViewModel<EditIncomeInputVM, EditIncomeOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
