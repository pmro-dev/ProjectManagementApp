using App.Common.ViewModels;
using MediatR;

namespace App.Features.Budgets.Edit;

public class EditBudgetQuery : IRequest<EditBudgetQueryResponse>
{
	public Guid BudgetId { get; set; }

	public EditBudgetQuery(Guid budgetId)
	{
		BudgetId = budgetId;
	}
}

public record EditBudgetQueryResponse(
	WrapperViewModel<EditBudgetInputVM, EditBudgetOutputVM>? Data,
	string? ErrorMessage = null,
	int StatusCode = StatusCodes.Status200OK
){}
